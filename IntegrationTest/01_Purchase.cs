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
        public async Task Purchase_AutoLogin_Visa_Tap()
        {
            string transactionId = DateTime.Now.ToString("yyMMddHHmmssfff");
            decimal requestedAmount = 1.00M;
            PaymentRequest request = new DataMeshGroup.Fusion.Model.PaymentRequest(transactionId, requestedAmount);
            PaymentResponse r = await Client.SendRecvAsync<PaymentResponse>(request);


            Assert.NotNull(r);
            // Message type
            Assert.True(r.MessageCategory == MessageCategory.Payment);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);

            // POIData
            Assert.NotNull(r.POIData.POIReconciliationID);
            Assert.NotNull(r.POIData.POITransactionID.TransactionID);


            // PaymentResult
            Assert.NotNull(r.PaymentResult.AmountsResp);
            //Assert.Equal(requestedAmount, r.PaymentResult.AmountsResp.AuthorizedAmount);
            Assert.Equal(CurrencySymbol.AUD, r.PaymentResult.AmountsResp.Currency);
            Assert.True(r.PaymentResult.OnlineFlag);
            Assert.Equal(PaymentType.Normal, r.PaymentResult.PaymentType);
            // PaymentResult.PaymentAcquirerData
            Assert.Equal("343455", r.PaymentResult.PaymentAcquirerData.AcquirerID); // Validate NAB acquirer code
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.AcquirerPOIID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.AcquirerTransactionID.TransactionID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.ApprovalCode);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.HostReconciliationID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.MerchantID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.RRN);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.ResponseCode);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.STAN);
            // PaymentResult.PaymentInstrumentData.CardData
            Assert.Equal(PaymentInstrumentType.Card, r.PaymentResult.PaymentInstrumentData.PaymentInstrumentType);
            Assert.Equal(EntryMode.Tapped, r.PaymentResult.PaymentInstrumentData.CardData.EntryMode);
            Assert.Equal("VISA", r.PaymentResult.PaymentInstrumentData.CardData.PaymentBrand);
            Assert.Equal(16, r.PaymentResult.PaymentInstrumentData.CardData.MaskedPAN.Length);
            Assert.Equal("XXXXXX", r.PaymentResult.PaymentInstrumentData.CardData.MaskedPAN.Substring(6, 6)); // Ensure PAN is masked
            Assert.True(int.TryParse(r.PaymentResult.PaymentInstrumentData.CardData.MaskedPAN.Substring(0, 6), out int panPrefix));
            Assert.True(int.TryParse(r.PaymentResult.PaymentInstrumentData.CardData.MaskedPAN.Substring(12, 4), out int panSuffix));
            // Sale Data
            Assert.NotNull(r.SaleData?.SaleTransactionID?.TransactionID);
            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }

        [Fact]
        public async Task Purchase_AutoLogin_Visa_Chip()
        {
            string transactionId = DateTime.Now.ToString("yyMMddHHmmssfff");
            decimal requestedAmount = 2.00M;
            PaymentRequest request = new DataMeshGroup.Fusion.Model.PaymentRequest(transactionId, requestedAmount);
            PaymentResponse r = await Client.SendRecvAsync<PaymentResponse>(request);


            Assert.NotNull(r);
            // Message type
            Assert.True(r.MessageCategory == MessageCategory.Payment);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);

            // POIData
            Assert.NotNull(r.POIData.POIReconciliationID);
            Assert.NotNull(r.POIData.POITransactionID.TransactionID);


            // PaymentResult
            Assert.NotNull(r.PaymentResult.AmountsResp);
            //Assert.Equal(requestedAmount, r.PaymentResult.AmountsResp.AuthorizedAmount);
            Assert.Equal(CurrencySymbol.AUD, r.PaymentResult.AmountsResp.Currency);
            Assert.True(r.PaymentResult.OnlineFlag);
            Assert.Equal(PaymentType.Normal, r.PaymentResult.PaymentType);
            // PaymentResult.PaymentAcquirerData
            Assert.Equal("343455", r.PaymentResult.PaymentAcquirerData.AcquirerID); // Validate NAB acquirer code
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.AcquirerPOIID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.AcquirerTransactionID.TransactionID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.ApprovalCode);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.HostReconciliationID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.MerchantID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.RRN);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.ResponseCode);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.STAN);
            // PaymentResult.PaymentInstrumentData.CardData
            Assert.Equal(PaymentInstrumentType.Card, r.PaymentResult.PaymentInstrumentData.PaymentInstrumentType);
            Assert.Equal(EntryMode.ICC, r.PaymentResult.PaymentInstrumentData.CardData.EntryMode);
            Assert.Equal("VISA", r.PaymentResult.PaymentInstrumentData.CardData.PaymentBrand);
            Assert.Equal(16, r.PaymentResult.PaymentInstrumentData.CardData.MaskedPAN.Length);
            Assert.Equal("XXXXXX", r.PaymentResult.PaymentInstrumentData.CardData.MaskedPAN.Substring(6, 6)); // Ensure PAN is masked
            Assert.True(int.TryParse(r.PaymentResult.PaymentInstrumentData.CardData.MaskedPAN.Substring(0, 6), out int panPrefix));
            Assert.True(int.TryParse(r.PaymentResult.PaymentInstrumentData.CardData.MaskedPAN.Substring(12, 4), out int panSuffix));
            // Sale Data
            Assert.NotNull(r.SaleData?.SaleTransactionID?.TransactionID);
            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }
    }
}
