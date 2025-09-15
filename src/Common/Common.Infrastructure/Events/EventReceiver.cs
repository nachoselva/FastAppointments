namespace Common.Infrastructure.Events
{
    using Microsoft.Extensions.Hosting;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class EventReceiver<TEvent> : IHostedService, IDisposable
    {
        private readonly SemaphoreSlim _channelSemaphore = new(1, 1);
        private readonly ConnectionFactory _connectionFactory;
        private IConnection? _connection;
        private IChannel? _channel;
        private AsyncEventingBasicConsumer? _consumer;

        protected abstract string ExchangeName { get; }
        protected abstract string QueueName { get; }
        protected abstract Func<TEvent, Task> ProcessEvent { get; }

        protected EventReceiver(ConnectionFactory factory)
        {
            _connectionFactory = factory;
        }

        public async Task StartAsync(CancellationToken _)
        {
            await InitConnectionAndChannel();
            _connection!.ConnectionShutdownAsync += OnConnectionShutdown;
            _channel!.ChannelShutdownAsync += OnChannelShutdown;
        }

        public async Task StopAsync(CancellationToken _)
        {
            await DisposeAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            DisposeAsync().GetAwaiter().GetResult();
        }

        private async Task InitConnectionAndChannel()
        {
            await _channelSemaphore.WaitAsync();
            try
            {
                if (_connection == null || !_connection.IsOpen)
                {
                    _connection = await _connectionFactory.CreateConnectionAsync();
                }

                if (_channel == null || !_channel.IsOpen)
                {
                    _channel = await _connection.CreateChannelAsync();
                    await _channel.ExchangeDeclareAsync(exchange: ExchangeName, type: "fanout");
                    await _channel.QueueDeclareAsync(queue: QueueName, durable: false, exclusive: false, autoDelete: false);
                    await _channel.QueueBindAsync(queue: QueueName, exchange: ExchangeName, routingKey: string.Empty);

                    _consumer = new AsyncEventingBasicConsumer(_channel);
                    _consumer.ReceivedAsync += OnMessageReceived;
                    await _channel.BasicConsumeAsync(QueueName, autoAck: true, _consumer);
                }
            }
            finally
            {
                _channelSemaphore.Release();
            }
        }

        private async Task OnConnectionShutdown(object? sender, ShutdownEventArgs args)
        {
            if (args.Initiator == ShutdownInitiator.Peer)
            {
                await InitConnectionAndChannel();
            }
        }

        private async Task OnChannelShutdown(object? sender, ShutdownEventArgs args)
        {
            if (args.Initiator == ShutdownInitiator.Peer)
            {
                await InitConnectionAndChannel();
            }
        }

        private async Task OnMessageReceived(object? sender, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var eventBody = JsonSerializer.Deserialize<TEvent>(message);

            if (eventBody != null)
            {
                await ProcessEvent(eventBody);
            }
        }

        private async ValueTask DisposeAsync()
        {
            if (_channel != null && _channel.IsOpen)
            {
                await _channel.CloseAsync();
                _channel.Dispose();
            }

            if (_connection != null && _connection.IsOpen)
            {
                await _connection.CloseAsync();
                _connection.Dispose();
            }
        }
    }
}