using DataMeshGroup.Fusion;
using System;
using System.Threading.Tasks;
using DataMeshGroup.Fusion.Model;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace IntegrationTest
{
    [Collection(nameof(FusionClientFixtureCollection))]
    public class TransactionStatusIntegrationTest
    {
        readonly FusionClientFixture fusionClientFixture;

        FusionClient Client => fusionClientFixture.Client;

        public TransactionStatusIntegrationTest(FusionClientFixture fusionClientFixture)
        {
            this.fusionClientFixture = fusionClientFixture;
        }

        [Fact]
        public async Task Purchase_AutoLogin_Visa_Tap()
        {
            // Ensure SaleToPOIRequestHistory has 2 payments
            while (fusionClientFixture.SaleToPOIRequestHistory.Count < 2)
            {
                string transactionId = DateTime.Now.ToString("yyMMddHHmmssfff");
                decimal requestedAmount = 1.00M;
                var saleToPoiRequest = await Client.SendAsync(new PaymentRequest(transactionId, requestedAmount));
                PaymentResponse r = await Client.RecvAsync<PaymentResponse>(new CancellationTokenSource(TimeSpan.FromSeconds(60)).Token);
                Assert.NotNull(r);
                Assert.True(r.Response.Success);
                Assert.Equal(Result.Success, r.Response.Result);

                fusionClientFixture.SaleToPOIRequestHistory.Add(saleToPoiRequest);
            }
        }

        [Fact]
        public async Task TransactionStatus_NoServiceId()
        {
            TransactionStatusRequest request = new TransactionStatusRequest();
            _ = await Client.SendAsync(request);

            List<MessagePayload> responses = new List<MessagePayload>();
            MessagePayload messagePayload  = await Client.RecvAsync();
            
            Assert.IsType<TransactionStatusResponse>(messagePayload);

            TransactionStatusResponse r = messagePayload as TransactionStatusResponse;

            Assert.NotNull(r);
            // Message type
            Assert.True(r.MessageCategory == MessageCategory.TransactionStatus);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);

            // TODO: Validate that we have the correct response (should be last payment in fusionClientFixture.SaleToPOIRequestHistory)

            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }

        [Fact]
        public async Task TransactionStatus_IncludeServiceId()
        {
            TransactionStatusRequest request = new TransactionStatusRequest()
            {
                MessageReference = new MessageReference()
                {
                    MessageCategory = fusionClientFixture.SaleToPOIRequestHistory[0].MessageHeader.MessageCategory,
                    DeviceID = fusionClientFixture.SaleToPOIRequestHistory[0].MessageHeader.DeviceID,
                    POIID = fusionClientFixture.SaleToPOIRequestHistory[0].MessageHeader.POIID,
                    SaleID = fusionClientFixture.SaleToPOIRequestHistory[0].MessageHeader.SaleID,
                    ServiceID = fusionClientFixture.SaleToPOIRequestHistory[0].MessageHeader.ServiceID
                }
            };
            _ = await Client.SendAsync(request);

            List<MessagePayload> responses = new List<MessagePayload>();
            MessagePayload messagePayload = await Client.RecvAsync();

            Assert.IsType<TransactionStatusResponse>(messagePayload);

            TransactionStatusResponse r = messagePayload as TransactionStatusResponse;

            Assert.NotNull(r);
            // Message type
            Assert.True(r.MessageCategory == MessageCategory.TransactionStatus);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);


            // TODO: Validate that we have the correct response (should be fusionClientFixture.SaleToPOIRequestHistory[0])

            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }
    }
}
