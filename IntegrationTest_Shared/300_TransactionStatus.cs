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
            // Ensure SaleToPOIRequestHistory has 3 payments
            while (fusionClientFixture.SaleToPOIRequestHistory.Count < 3)
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
            MessagePayload messagePayload = await Client.SendRecvAsync<TransactionStatusResponse>(request);

            Assert.IsType<TransactionStatusResponse>(messagePayload);

            TransactionStatusResponse r = messagePayload as TransactionStatusResponse;

            Assert.NotNull(r);
            // Message type
            Assert.True(r.MessageCategory == MessageCategory.TransactionStatus);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);

            // Validate that we have the correct response (should be last payment in fusionClientFixture.SaleToPOIRequestHistory)
            MessageHeader expectedHeader = fusionClientFixture.SaleToPOIRequestHistory.Last().MessageHeader;
            MessageHeader actualHeader = r.RepeatedMessageResponse.MessageHeader;
            Assert.Equal(expectedHeader.ServiceID, actualHeader.ServiceID);
            Assert.Equal(expectedHeader.SaleID, actualHeader.SaleID);
            Assert.Equal(expectedHeader.POIID, actualHeader.POIID);

            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }

        [Fact]
        public async Task TransactionStatus_IncludeServiceId_FirstTxn()
        {
            await TransactionStatus_IncludeServiceId_Internal(fusionClientFixture.SaleToPOIRequestHistory.First().MessageHeader);
        }

        [Fact]
        public async Task TransactionStatus_IncludeServiceId_LastTxn()
        {
            await TransactionStatus_IncludeServiceId_Internal(fusionClientFixture.SaleToPOIRequestHistory.Last().MessageHeader);
        }

        /// <summary>
        /// Simulate TransactionStatus for a transaction never received by DataMesh
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TransactionStatus_IncludeServiceId_UnknownServiceID()
        {
            MessageHeader mh = fusionClientFixture.SaleToPOIRequestHistory.Last().MessageHeader;
            
            TransactionStatusRequest request = new TransactionStatusRequest()
            {
                MessageReference = new MessageReference()
                {
                    MessageCategory = mh.MessageCategory,
                    DeviceID = mh.DeviceID,
                    POIID = mh.POIID,
                    SaleID = mh.SaleID,
                    ServiceID = Guid.NewGuid().ToString()
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

            // RepeatedMessageResponse                        
            Assert.Null(r.RepeatedMessageResponse);

            // Response
            Assert.False(r.Response.Success);
            Assert.Equal(Result.Failure, r.Response.Result);
            Assert.Equal(ErrorCondition.NotFound, r.Response.ErrorCondition);
            Assert.NotNull(r.Response.AdditionalResponse);
        }


        /// <summary>
        /// Simulate TransactionStatus for an InProgress payment
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TransactionStatus_IncludeServiceId_InProgress_NoTap()
        {
            // Send payment request... DONT TAP
            string transactionId = DateTime.Now.ToString("yyMMddHHmmssfff");
            decimal requestedAmount = 1.00M;
            var saleToPoiRequest = await Client.SendAsync(new PaymentRequest(transactionId, requestedAmount));

            MessagePayload mp;
            do
            {
                mp = await Client.RecvAsync();
            } while (!(mp is DisplayRequest));

            fusionClientFixture.SaleToPOIRequestHistory.Add(saleToPoiRequest);
            await Client.DisconnectAsync();

            // Send transaction status request
            MessageHeader mh = fusionClientFixture.SaleToPOIRequestHistory.Last().MessageHeader;
            TransactionStatusRequest request = new TransactionStatusRequest()
            {
                MessageReference = new MessageReference()
                {
                    MessageCategory = mh.MessageCategory,
                    DeviceID = mh.DeviceID,
                    POIID = mh.POIID,
                    SaleID = mh.SaleID,
                    ServiceID = mh.ServiceID
                }
            };
            MessagePayload messagePayload = await Client.SendRecvAsync<TransactionStatusResponse>(request);
            Assert.IsType<TransactionStatusResponse>(messagePayload);

            TransactionStatusResponse r = messagePayload as TransactionStatusResponse;

            Assert.NotNull(r);
            // Message type
            Assert.True(r.MessageCategory == MessageCategory.TransactionStatus);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);

            // RepeatedMessageResponse                        
            Assert.Null(r.RepeatedMessageResponse);

            // Response
            Assert.False(r.Response.Success);
            Assert.Equal(Result.Failure, r.Response.Result);
            Assert.Equal(ErrorCondition.InProgress, r.Response.ErrorCondition);
            Assert.NotNull(r.Response.AdditionalResponse);

            // TODO: should be able to remove this
            await Task.Delay(5000);

            // Send abort request
            var abortRequest = new AbortRequest()
            {
                AbortReason = "User Cancel",
                MessageReference = new MessageReference()
                {
                    DeviceID = mh.DeviceID,
                    MessageCategory = mh.MessageCategory,
                    POIID = mh.POIID,
                    SaleID = mh.SaleID,
                    ServiceID = mh.ServiceID
                }
            };
            _ = await Client.SendAsync(abortRequest);
            await Client.DisconnectAsync();
            await Task.Delay(5000);

            // Send transaction status. Should work this time!
            await TransactionStatus_IncludeServiceId_Internal(mh);
        }


        private async Task TransactionStatus_IncludeServiceId_Internal(MessageHeader expectedHeader)
        {
            TransactionStatusRequest request = new TransactionStatusRequest()
            {
                MessageReference = new MessageReference()
                {
                    MessageCategory = expectedHeader.MessageCategory,
                    DeviceID = expectedHeader.DeviceID,
                    POIID = expectedHeader.POIID,
                    SaleID = expectedHeader.SaleID,
                    ServiceID = expectedHeader.ServiceID
                }
            };
            MessagePayload messagePayload = await Client.SendRecvAsync<TransactionStatusResponse>(request);
            Assert.IsType<TransactionStatusResponse>(messagePayload);

            TransactionStatusResponse r = messagePayload as TransactionStatusResponse;

            Assert.NotNull(r);
            // Message type
            Assert.True(r.MessageCategory == MessageCategory.TransactionStatus);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);

            // Validate that we have the correct response (should be fusionClientFixture.SaleToPOIRequestHistory.First())
            MessageHeader actualHeader = r.RepeatedMessageResponse.MessageHeader;
            Assert.Equal(expectedHeader.ServiceID, actualHeader.ServiceID);
            Assert.Equal(expectedHeader.SaleID, actualHeader.SaleID);
            Assert.Equal(expectedHeader.POIID, actualHeader.POIID);

            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }
    }
}
