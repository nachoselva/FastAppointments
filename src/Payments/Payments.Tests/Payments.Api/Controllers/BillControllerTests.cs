using Common.Application.CQRS;
using NSubstitute;
using Payments.Api.Controllers;
using Payments.Application.Implementations.Bill.Create;
using Payments.Application.Implementations.PaymentEntity.Common;
using System.Linq;
using Xunit;

namespace Payments.Tests.Payments.Api.Controllers
{
    public class BillControllerTests
    {
        private readonly ICommandDispatcher _commandDispatcherSub;
        private readonly BillsController _controller;

        public BillControllerTests()
        {
            _commandDispatcherSub = Substitute.For<ICommandDispatcher>();
            _controller = new BillsController(_commandDispatcherSub);
        }

        [Fact]
        public async Task CreateBill_ValidRequest_DispatchesCommandAndReturnsGuid()
        {
            // Arrange
            var expectedGuid = Guid.NewGuid();

            var senderClientId = Guid.NewGuid();
            var senderProviderId = Guid.NewGuid();
            var senderCompanyId = Guid.NewGuid();
            var receiverClientId = Guid.NewGuid();
            var receiverProviderId = Guid.NewGuid();
            var receiverCompanyId = Guid.NewGuid();

            var billItemSourceId1 = Guid.NewGuid();
            var billItemSourceId2 = Guid.NewGuid();
            var billSourceId1 = Guid.NewGuid();
            var billSourceId2 = Guid.NewGuid();

            var request = new CreateBillRequest
            (
                Sender: new PaymentEntityRequest(senderClientId, senderProviderId, senderCompanyId),
                Receiver: new PaymentEntityRequest(receiverClientId, receiverProviderId, receiverCompanyId),
                BillItems:
                [
                    new CreateBillItemRequest(
                        Description: "Item1",
                        PricePerUnit: 10.5m,
                        UnitsCount: 2,
                        UnitsName: "pcs",
                        BillItemSources:
                        [
                            new CreateBillItemSourceRequest("TypeA", billItemSourceId1),
                            new CreateBillItemSourceRequest("TypeB", billItemSourceId2)
                        ]
                    ),
                    new CreateBillItemRequest(
                        Description: "Item2",
                        PricePerUnit: 20.0m,
                        UnitsCount: 1,
                        UnitsName: "kg",
                        BillItemSources:
                        [
                            new CreateBillItemSourceRequest("TypeC", billItemSourceId1)
                        ]
                    )
                ],
                BillSources:
                [
                    new CreateBillSourceRequest("TypeB", billSourceId1),
                    new CreateBillSourceRequest("TypeC", billSourceId2)
                ]
            );

            _commandDispatcherSub
                .DispatchAsync<CreateBillCommand, Guid>(Arg.Any<CreateBillCommand>(), Arg.Any<CancellationToken>())
                .Returns(expectedGuid);

            // Act
            var result = await _controller.CreateBill(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedGuid, result.Value);
            await _commandDispatcherSub.Received(1)
                .DispatchAsync<CreateBillCommand, Guid>(
                    Arg.Is<CreateBillCommand>(cmd =>
                        cmd.Sender.ClientId == senderClientId &&
                        cmd.Sender.ProviderId == senderProviderId &&
                        cmd.Sender.CompanyId == senderCompanyId &&
                        cmd.Receiver.ClientId == receiverClientId &&
                        cmd.Receiver.ProviderId == receiverProviderId &&
                        cmd.Receiver.CompanyId == receiverCompanyId &&
                        cmd.BillItems.Count() == 2 &&
                        cmd.BillItems.ElementAt(0).Description == "Item1" &&
                        cmd.BillItems.ElementAt(0).PricePerUnit == 10.5m &&
                        cmd.BillItems.ElementAt(0).UnitsCount == 2 &&
                        cmd.BillItems.ElementAt(0).UnitsName == "pcs" &&
                        cmd.BillItems.ElementAt(0).BillItemSources.Count() == 2 &&
                        cmd.BillItems.ElementAt(0).BillItemSources.ElementAt(0).SourceType == "TypeA" &&
                        cmd.BillItems.ElementAt(0).BillItemSources.ElementAt(0).SourceId == billItemSourceId1 &&
                        cmd.BillItems.ElementAt(0).BillItemSources.ElementAt(1).SourceType == "TypeB" &&
                        cmd.BillItems.ElementAt(0).BillItemSources.ElementAt(1).SourceId == billItemSourceId2 &&
                        cmd.BillItems.ElementAt(1).Description == "Item2" &&
                        cmd.BillItems.ElementAt(1).PricePerUnit == 20.0m &&
                        cmd.BillItems.ElementAt(1).UnitsCount == 1 &&
                        cmd.BillItems.ElementAt(1).UnitsName == "kg" &&
                        cmd.BillItems.ElementAt(1).BillItemSources.Count() == 1 &&
                        cmd.BillItems.ElementAt(1).BillItemSources.ElementAt(0).SourceType == "TypeC" &&
                        cmd.BillItems.ElementAt(1).BillItemSources.ElementAt(0).SourceId == billItemSourceId1 &&
                        cmd.BillSources.Count() == 2 &&
                        cmd.BillSources.ElementAt(0).SourceType == "TypeB" &&
                        cmd.BillSources.ElementAt(0).SourceId == billSourceId1 &&
                        cmd.BillSources.ElementAt(1).SourceType == "TypeC" &&
                        cmd.BillSources.ElementAt(1).SourceId == billSourceId2
                    ),
                    Arg.Any<CancellationToken>());
        }
    }
}
