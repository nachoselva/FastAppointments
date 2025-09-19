namespace Common.Infrastructure.Events
{
    using Common.Application;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System.Text;
    using System.Text.Json;
    using System.Threading;

    public abstract class EventPublisher<T> : IEventPublisher<T>, IDisposable
    {
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly ConnectionFactory _connectionFactory;
        private IConnection? _connection;
        private IChannel? _channel;

        protected abstract string ExchangeName { get; }

        public EventPublisher(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            InitConnectionAndChannel().GetAwaiter().GetResult();
        }

        public async Task PublishAsync(T eventToBePublished, CancellationToken cancellationToken)
        {
            if (_connection == null || !_connection.IsOpen)
                await InitConnectionAndChannel();

            if (_channel == null || _channel.IsClosed)
                await InitConnectionAndChannel();

            await PublishMessageWithOpenedConnection(eventToBePublished, cancellationToken);
        }

        public async Task PublishAsync(IEnumerable<T> eventToBePublished, CancellationToken cancellationToken)
        {
            if (_connection == null || !_connection.IsOpen)
                await InitConnectionAndChannel();

            if (_channel == null || _channel.IsClosed)
                await InitConnectionAndChannel();

            foreach (var item in eventToBePublished)
                await PublishMessageWithOpenedConnection(item, cancellationToken);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            DisposeAsync().AsTask().GetAwaiter().GetResult();
        }

        private async Task PublishMessageWithOpenedConnection(T eventToBePublished, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(eventToBePublished);
            var body = Encoding.UTF8.GetBytes(json);

            await _channel!.BasicPublishAsync(exchange: ExchangeName, routingKey: string.Empty, body: body, cancellationToken: cancellationToken);
        }

        private async Task InitConnectionAndChannel()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_connection == null || !_connection.IsOpen)
                {
                    _connection = await _connectionFactory.CreateConnectionAsync();
                }

                if (_channel == null || _channel.IsClosed)
                {
                    _channel = await _connection.CreateChannelAsync();
                    _channel.ChannelShutdownAsync += OnChannelShutdown;
                    await _channel.ExchangeDeclareAsync(exchange: ExchangeName, type: ExchangeType.Fanout);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task OnChannelShutdown(object sender, ShutdownEventArgs args)
        {
            if (args.Initiator == ShutdownInitiator.Peer)
            {
                await InitConnectionAndChannel();
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