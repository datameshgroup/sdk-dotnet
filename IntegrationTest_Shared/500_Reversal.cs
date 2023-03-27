using DataMeshGroup.Fusion;
using System;
using System.Threading.Tasks;
using DataMeshGroup.Fusion.Model;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataMeshGroup.Fusion.IntegrationTest
{
#if (!NETFRAMEWORK)
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest")]
#else
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest_Net48")]
#endif

    [Collection(nameof(FusionClientFixtureCollection))]
    public class ReversalIntegrationTest
    {
        readonly FusionClientFixture fusionClientFixture;

        FusionClient Client => fusionClientFixture.Client;

        // TODO → add to fusionClientFixture
        private static PaymentRequest paymentRequest = null;
        private static PaymentResponse paymentResponse = null;

        public ReversalIntegrationTest(FusionClientFixture fusionClientFixture)
        {
            this.fusionClientFixture = fusionClientFixture;
        }

        [Fact]
        public async Task Reversal_CreatePayment()
        {
            // Ensure SaleToPOIRequestHistory has 1 payments
            while (fusionClientFixture.SaleToPOIRequestHistory.Count < 1)
            {
                string transactionId = DateTime.Now.ToString("yyMMddHHmmssfff");
                decimal requestedAmount = 1.00M;
                paymentRequest = new PaymentRequest(transactionId, requestedAmount);
                var saleToPoiRequest = await Client.SendAsync(paymentRequest);
                paymentResponse = await Client.RecvAsync<PaymentResponse>(new CancellationTokenSource(TimeSpan.FromSeconds(60)).Token);
                Assert.NotNull(paymentResponse);
                Assert.True(paymentResponse.Response.Success);
                Assert.Equal(Result.Success, paymentResponse.Response.Result);

                fusionClientFixture.SaleToPOIRequestHistory.Add(saleToPoiRequest);
            }
        }


        [Fact]
        public async Task Reversal_KnownTransaction()
        {
            ReversalRequest request = new ReversalRequest()
            {
                OriginalPOITransaction = new OriginalPOITransaction()
                {
                    POIID = Client.POIID,
                    SaleID = Client.SaleID,
                    POITransactionID = paymentResponse.POIData.POITransactionID
                },
                ReversalReason = ReversalReason.SignatureDeclined
            };

            MessagePayload messagePayload = await Client.SendRecvAsync<ReversalResponse>(request);

            Assert.IsType<ReversalResponse>(messagePayload);

            ReversalResponse r = messagePayload as ReversalResponse;

            Assert.NotNull(r);
            // Message type
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageCategory == MessageCategory.Reversal);
            Assert.True(r.MessageType == MessageType.Response);

            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }

        [Fact]
        public async Task Reversal_UnknownTransaction()
        {
            ReversalRequest request = new ReversalRequest()
            {
                OriginalPOITransaction = new OriginalPOITransaction()
                {
                    POIID = Client.POIID,
                    SaleID = Client.SaleID,
                    POITransactionID = new TransactionIdentification(null)
                    {
                        TimeStamp = paymentResponse.POIData.POITransactionID.TimeStamp,
                        TransactionID = paymentResponse.POIData.POITransactionID.TransactionID + "_"
                    }
                },
                ReversalReason = ReversalReason.SignatureDeclined
            };

            MessagePayload messagePayload = await Client.SendRecvAsync<ReversalResponse>(request);

            Assert.IsType<ReversalResponse>(messagePayload);

            ReversalResponse r = messagePayload as ReversalResponse;

            Assert.NotNull(r);
            // Message type
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageCategory == MessageCategory.Reversal);
            Assert.True(r.MessageType == MessageType.Response);

            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }

    }
}
