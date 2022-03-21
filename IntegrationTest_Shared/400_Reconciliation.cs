using DataMeshGroup.Fusion;
using DataMeshGroup.Fusion.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DataMeshGroup.Fusion.IntegrationTest
{
#if (!NETFRAMEWORK)
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest")]
#else
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest_Net48")]
#endif


    [Collection(nameof(FusionClientFixtureCollection))]
    public class ReconciliationIntegrationTest
    {
        readonly FusionClientFixture fusionClientFixture;

        FusionClient Client => fusionClientFixture.Client;

        public ReconciliationIntegrationTest(FusionClientFixture fusionClientFixture)
        {
            this.fusionClientFixture = fusionClientFixture;
        }

        [Fact]
        public async Task Reconciliation()
        {
            ReconciliationRequest request = new ReconciliationRequest(reconciliationType: ReconciliationType.SaleReconciliation);
            _ = await Client.SendAsync(request);

            List<MessagePayload> responses = new List<MessagePayload>();
            ReconciliationResponse r = await Client.RecvAsync<ReconciliationResponse>(new CancellationTokenSource(TimeSpan.FromSeconds(60)).Token);
            
            //
            Assert.NotNull(r);
            Assert.IsType<ReconciliationResponse>(r);

            // Message type
            Assert.True(r.MessageCategory == MessageCategory.Reconciliation);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);

            // TODO: Validate totals
            Assert.NotNull(r.GetReceiptAsPlainText());

            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }
    }
}
