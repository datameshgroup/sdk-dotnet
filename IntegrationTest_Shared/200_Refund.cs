using DataMeshGroup.Fusion;
using DataMeshGroup.Fusion.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RefundIntegrationTest
    {
        readonly FusionClientFixture fusionClientFixture;

        FusionClient Client => fusionClientFixture.Client;

        public RefundIntegrationTest(FusionClientFixture fusionClientFixture)
        {
            this.fusionClientFixture = fusionClientFixture;
        }

        [Fact]
        public async Task Refund_AutoLogin_Visa_Tap()
        {
            string transactionId = DateTime.Now.ToString("yyMMddHHmmssfff");
            decimal requestedAmount = 1.00M;
            PaymentRequest request = new DataMeshGroup.Fusion.Model.PaymentRequest(transactionId, requestedAmount, paymentType: PaymentType.Refund);
            SaleToPOIMessage saleToPOIRequest = await Client.SendAsync(request);

            List<MessagePayload> responses = new List<MessagePayload>();
            MessagePayload messagePayload;
            PaymentResponse r;
            do
            {
                messagePayload = await Client.RecvAsync();
                responses.Add(messagePayload);
            }
            while (!(messagePayload is PaymentResponse));
            r = messagePayload as PaymentResponse;

            Assert.True(responses.Count > 1);
            Assert.NotNull(responses.FirstOrDefault(mp => mp is DisplayRequest));

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
            Assert.Equal(PaymentType.Refund, r.PaymentResult.PaymentType);
            // Receipt
            Assert.NotNull(r.GetReceiptAsPlainText());
            Assert.NotNull(r.PaymentReceipt);
            Assert.True(r.PaymentReceipt.Count >= 1);
            Assert.True(r.PaymentReceipt[0].DocumentQualifier == DocumentQualifier.SaleReceipt || r.PaymentReceipt[0].DocumentQualifier == DocumentQualifier.CustomerReceipt);
            Assert.True(r.PaymentReceipt[0].IntegratedPrintFlag);
            Assert.False(r.PaymentReceipt[0].RequiredSignatureFlag);
            Assert.NotNull(r.PaymentReceipt[0].OutputContent);
            Assert.True(r.PaymentReceipt[0].OutputContent.OutputFormat == OutputFormat.XHTML);
            Assert.True(r.PaymentReceipt[0].OutputContent.OutputXHTML?.Length > 0);
            // PaymentResult.PaymentAcquirerData
            Assert.Equal("343455", r.PaymentResult.PaymentAcquirerData.AcquirerID); // Validate NAB acquirer code
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.AcquirerPOIID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.AcquirerTransactionID.TransactionID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.ApprovalCode);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.HostReconciliationID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.MerchantID);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.RRN);
            Assert.NotNull(r.PaymentResult.PaymentAcquirerData.ResponseCode);
            //Assert.NotNull(r.PaymentResult.PaymentAcquirerData.STAN); // is returned as NULL from dummy processor - need to fix this
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

            fusionClientFixture.SaleToPOIRequestHistory.Add(saleToPOIRequest);
        }
    }
}
