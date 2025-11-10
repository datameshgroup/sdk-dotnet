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


    /// <summary>
    /// Test cases to cover model building and serialization/deserialization
    /// </summary>
    [Collection(nameof(FusionClientFixtureCollection))]
    public class ModelIntegrationTest
    {
        readonly FusionClientFixture fusionClientFixture;

        FusionClient Client => fusionClientFixture.Client;

        public ModelIntegrationTest(FusionClientFixture fusionClientFixture)
        {
            this.fusionClientFixture = fusionClientFixture;
        }

        [Fact]
        public async Task Model_CreateUnpack()
        {
            // Construct default NexoMessageParser
            NexoMessageParser parser = new NexoMessageParser();
            parser.EnableMACValidation = false;
            parser.EnableSecurityTrailer = false;
            // Default 
            string saleId = "SaleID";
            string poiId = "POIID";
            string serviceId = "ServiceID";
            string kek = "KEK";
            string msgStr = null;

            SaleToPOIMessage saleToPOIMessage = parser.BuildSaleToPOIMessage(serviceId, saleId, poiId, kek, SampleAbortRequest);
            Assert.NotNull(saleToPOIMessage);
            msgStr = parser.SaleToPOIMessageToString(saleToPOIMessage);
            Assert.NotNull(msgStr);
            saleToPOIMessage = parser.ParseSaleToPOIMessage(msgStr);
            Assert.NotNull(msgStr);

        }

        private static AbortRequest SampleAbortRequest => new AbortRequest()
        {
            AbortReason = "MerchantAbort",
            MessageReference = new MessageReference
            {
                MessageCategory = MessageCategory.Abort,
                ServiceID = "ServiceID",
                SaleID = "SaleID",
                POIID = "POIID"
            }
        };

        //[Fact]
        //public async Task Reversal_KnownTransaction()
        //{
        //    ReversalRequest request = new ReversalRequest()
        //    {
        //        OriginalPOITransaction = new OriginalPOITransaction()
        //        {
        //            POIID = Client.POIID,
        //            SaleID = Client.SaleID,
        //            POITransactionID = paymentResponse.POIData.POITransactionID
        //        },
        //        ReversalReason = ReversalReason.SignatureDeclined
        //    };

        //    MessagePayload messagePayload = await Client.SendRecvAsync<ReversalResponse>(request);

        //    Assert.IsType<ReversalResponse>(messagePayload);

        //    ReversalResponse r = messagePayload as ReversalResponse;

        //    Assert.NotNull(r);
        //    // Message type
        //    Assert.True(r.MessageClass == MessageClass.Service);
        //    Assert.True(r.MessageCategory == MessageCategory.Reversal);
        //    Assert.True(r.MessageType == MessageType.Response);

        //    // Response
        //    Assert.True(r.Response.Success);
        //    Assert.Equal(Result.Success, r.Response.Result);
        //}

        //[Fact]
        //public async Task Reversal_UnknownTransaction()
        //{
        //    ReversalRequest request = new ReversalRequest()
        //    {
        //        OriginalPOITransaction = new OriginalPOITransaction()
        //        {
        //            POIID = Client.POIID,
        //            SaleID = Client.SaleID,
        //            POITransactionID = new TransactionIdentification(null)
        //            {
        //                TimeStamp = paymentResponse.POIData.POITransactionID.TimeStamp,
        //                TransactionID = paymentResponse.POIData.POITransactionID.TransactionID + "_"
        //            }
        //        },
        //        ReversalReason = ReversalReason.SignatureDeclined
        //    };

        //    MessagePayload messagePayload = await Client.SendRecvAsync<ReversalResponse>(request);

        //    Assert.IsType<ReversalResponse>(messagePayload);

        //    ReversalResponse r = messagePayload as ReversalResponse;

        //    Assert.NotNull(r);
        //    // Message type
        //    Assert.True(r.MessageClass == MessageClass.Service);
        //    Assert.True(r.MessageCategory == MessageCategory.Reversal);
        //    Assert.True(r.MessageType == MessageType.Response);

        //    // Response
        //    Assert.True(r.Response.Success);
        //    Assert.Equal(Result.Success, r.Response.Result);
        //}

    }
}
