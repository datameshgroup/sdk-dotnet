using DataMeshGroup.Fusion;
using System;
using System.Threading.Tasks;
using DataMeshGroup.Fusion.Model;
using Xunit;

namespace IntegrationTest
{
    [Collection(nameof(FusionClientFixtureCollection))]
    public class PurchaseUnitTest 
    {
        readonly FusionClientFixture fusionClientFixture;

        FusionClient Client => fusionClientFixture.Client;

        public PurchaseUnitTest(FusionClientFixture fusionClientFixture)
        {
            this.fusionClientFixture = fusionClientFixture;
        }

        [Fact]
        public async Task Purchase_AutoLogin()
        {
            var request = new DataMeshGroup.Fusion.Model.PaymentRequest(DateTime.Now.ToString("yyMMddHHmmssfff"), 1.00M);
            var response = await Client.SendRecvAsync<PaymentResponse>(request);

            Assert.NotNull(response);
            Assert.NotNull(response.POIData.POIReconciliationID);
            Assert.NotNull(response.POIData.POITransactionID.TransactionID);
            Assert.NotNull(response.PaymentResult.AmountsResp);
            Assert.Equal(1M, response.PaymentResult.AmountsResp.AuthorizedAmount);
            Assert.Equal(CurrencySymbol.AUD, response.PaymentResult.AmountsResp.Currency);
            Assert.True(response.Response.Success);
        }
    }
}
