using DataMeshGroup.Fusion.Model;
using Newtonsoft.Json;
using System;
using System.Globalization;
using Xunit;

namespace DataMeshGroup.Fusion.IntegrationTest
{
#if (!NETFRAMEWORK)
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest")]
#else
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest_Net48")]
#endif

    public class MessageWrapper
    {
        public PaymentRequest PaymentRequest { get; set; }
        public PaymentResponse PaymentResponse { get; set; }
    }

    public class JSONParsing
    {
        public JSONParsing()
        {
        }


        [Fact]
        public void MessageParser_PaymentRequest()
        {
            string json = "{\"PaymentRequest\":{\"SaleData\":{\"SaleTransactionID\":{\"TransactionID\":\"8913a236-e70f-4e96-9f83-2e4724835370\",\"TimeStamp\":\"2023-05-18T14:33:37+10:00\"}},\"PaymentTransaction\":{\"AmountsReq\":{\"Currency\":\"AUD\",\"RequestedAmount\":10.00},\"SaleItem\":[{\"ItemID\":\"0\",\"ProductCode\":\"_DMGTC44851\",\"UnitOfMeasure\":\"Other\",\"Quantity\":\"1\",\"UnitPrice\":\"10.42\",\"ItemAmount\":\"10.42\",\"ProductLabel\":\"Big Kahuna Burger\"}]},\"PaymentData\":{\"PaymentType\":\"Normal\"}}}";
            MessageWrapper o = JsonConvert.DeserializeObject<MessageWrapper>(json);
            Assert.NotNull(o);
            Assert.NotNull(o.PaymentRequest);

            PaymentRequest p = o.PaymentRequest;
            Assert.Equal("8913a236-e70f-4e96-9f83-2e4724835370", p.SaleData.SaleTransactionID.TransactionID);
            Assert.Equal(DateTime.Parse("2023-05-18T14:33:37+10:00"), p.SaleData.SaleTransactionID.TimeStamp);
            Assert.Equal(CurrencySymbol.AUD, p.PaymentTransaction.AmountsReq.Currency);
            Assert.Equal(10.00M, p.PaymentTransaction.AmountsReq.RequestedAmount);
            Assert.Equal(PaymentType.Normal, p.PaymentData.PaymentType);

            SaleItem s = p.PaymentTransaction.SaleItem[0];
            Assert.Equal(0, s.ItemID);
            Assert.Equal("_DMGTC44851", s.ProductCode);
            Assert.Equal(UnitOfMeasure.Other, s.UnitOfMeasure);
            Assert.Equal(1, s.Quantity);
            Assert.Equal(10.42M, s.UnitPrice);
            Assert.Equal(10.42M, s.ItemAmount);
            Assert.Equal("Big Kahuna Burger", s.ProductLabel);
        }

        [Fact]
        public void MessageParser_PaymentResponse()
        {
            string json = "{\"SaleToPOIResponse\":{\"MessageHeader\":{\"MessageClass\":\"Service\",\"MessageCategory\":\"Payment\",\"MessageType\":\"Response\",\"ServiceID\":\"B1C96AD\",\"SaleID\":\"M00001088 004\",\"POIID\":\"50654236\"},\"PaymentResponse\":{\"Response\":{\"Result\":\"Success\"},\"SaleData\":{\"OperatorID\":\"85\",\"OperatorLanguage\":\"en\",\"ShiftNumber\":\"0\",\"SaleTransactionID\":{\"TransactionID\":\"0447087509\",\"TimeStamp\":\"2024-01-03T03:47:02.9566573Z\"},\"SaleReferenceID\":\"0641ab6f-827f-4650-b877-41e60742815c\",\"TokenRequestedType\":\"Transaction\",\"CustomerOrderID\":\"\"},\"POIData\":{\"POITransactionID\":{\"TransactionID\":\"6594d8b8cde34fa05ad7f966\",\"TimeStamp\":\"2024-01-03T14:47:10.493+11:00\"},\"POIReconciliationID\":\"648f9e0585be9b5dbfc3399a\"},\"PaymentResult\":{\"PaymentType\":\"Normal\",\"PaymentInstrumentData\":{\"PaymentInstrumentType\":\"Card\",\"CardData\":{\"EntryMode\":\"Tapped\",\"PaymentBrand\":\"American Express\",\"Expiry\":\"2411\",\"MaskedPAN\":\"379949XXXXX2395\",\"PaymentToken\":{\"TokenRequestedType\":\"Transaction\",\"TokenValue\":\"DCC19DEEF2C0DD0C669A095B2AD47D3A403BAFF5C6F14E\",\"ExpiryDateTime\":\"2024-12-01T00:00:00.00+11:00\"}}},\"AmountsResp\":{\"Currency\":\"AUD\",\"AuthorizedAmount\":30.44,\"TotalFeesAmount\":0,\"CashBackAmount\":0,\"SurchargeAmount\":0.44,\"TipAmount\":0},\"OnlineFlag\":true,\"PaymentAcquirerData\":{\"AcquirerID\":\"308400\",\"MerchantID\":\"42298585621399\",\"AcquirerPOIID\":\"50654236\",\"AcquirerTransactionID\":{\"TransactionID\":\"{\\\"tranType\\\":\\\"200\\\",\\\"stan\\\":\\\"007266\\\",\\\"origTimestamp\\\":\\\"0103134710\\\",\\\"authCode\\\":\\\"839939\\\",\\\"DE47\\\":\\\"TCC07\\\\\\\\\\\"}\",\"TimeStamp\":\"2024-01-03T03:47:11.696Z\"},\"ApprovalCode\":\"839939\",\"ResponseCode\":\"00\",\"HostReconciliationID\":\"20240104\",\"STAN\":\"007266\",\"RRN\":\"007266144710\"}},\"PaymentReceipt\":[{\"DocumentQualifier\":\"SaleReceipt\",\"IntegratedPrintFlag\":true,\"RequiredSignatureFlag\":false,\"OutputContent\":{\"OutputFormat\":\"XHTML\",\"OutputXHTML\":\"PHAgaWQ9InJlY2VpcHQtaW5mbyI+MDMvMDEvMjAyNCAxMzo0NzoxMDxici8+TWVyY2hhbnQgSUQ6IE0wMDAwMTA4ODxici8+VGVybWluYWwgSUQ6IDUwNjU0MjM2PC9wPjxwIGlkPSJyZWNlaXB0LWRldGFpbHMiPjxiPlB1cmNoYXNlIFRyYW5zYWN0aW9uPC9iPjxici8+QW1vdW50OiAkMzAuMDA8YnIvPlN1cmNoYXJnZTogJDAuNDQ8YnIvPlRvdGFsOiAkMzAuNDQ8YnIvPkFtZXJpY2FuIEV4cHJlc3M6IDM3OTk0OVhYWFhYMjM5NSAoVCk8YnIvPkNyZWRpdCBBY2NvdW50PC9wPjxwIGlkPSJyZWNlaXB0LXJlc3VsdCI+PGI+QXBwcm92ZWQ8L2I+PGJyLz5SZWZlcmVuY2U6IDAwMDAgNjg2MyA2Nzg0PGJyLz5BdXRoIENvZGU6IDgzOTkzOTxici8+QUlEOiBBMDAwMDAwMDI1MDEwOTAxPGJyLz5BVEM6IDAwNUY8YnIvPlRWUjogMDAwMDAwODAwMDxici8+QVJRQzogQzQ5MTkzNkJBMzgzNjlENjwvcD4=\"}}]},\"SecurityTrailer\":{\"ContentType\":\"id-ct-authData\",\"AuthenticatedData\":{\"Version\":\"v0\",\"Recipient\":{\"KEK\":{\"Version\":\"v4\",\"KEKIdentifier\":{\"KeyIdentifier\":\"SpecV2ProdMACKey\",\"KeyVersion\":\"20191122164326.594\"},\"KeyEncryptionAlgorithm\":{\"Algorithm\":\"des-ede3-cbc\"},\"EncryptedKey\":\"2198198FAFBEB30E547A768680EFC41C\"},\"MACAlgorithm\":{\"Algorithm\":\"id-retail-cbc-mac-sha-256\"},\"EncapsulatedContent\":{\"ContentType\":\"id-data\"},\"MAC\":\"23ED4D628B5E3CA2\"}}}}}";
            IMessageParser messageParser = new NexoMessageParser() { EnableMACValidation = false };
            var saleToPOIMessage = messageParser.ParseSaleToPOIMessage(json, null);

            Assert.NotNull(saleToPOIMessage);
            Assert.NotNull(saleToPOIMessage.MessagePayload);
            Assert.IsType<PaymentResponse>(saleToPOIMessage.MessagePayload);

            PaymentResponse p = saleToPOIMessage.MessagePayload as PaymentResponse;
            // Assert the PaymentResponse object
            Assert.NotNull(p.Response);
            // Assert the Response object
            Assert.Equal(Result.Success, p.Response.Result);
            Assert.Equal(MessageCategory.Payment, p.MessageCategory);
            Assert.Equal(MessageClass.Service, p.MessageClass);
            Assert.Equal(MessageClass.Service, p.MessageClass);
            // Assert the SaleData object
            Assert.NotNull(p.SaleData);
            Assert.Equal("85", p.SaleData.OperatorID);
            Assert.Equal("en", p.SaleData.OperatorLanguage);
            Assert.Equal("0", p.SaleData.ShiftNumber);
            Assert.NotNull(p.SaleData.SaleTransactionID);
            Assert.Equal("0447087509", p.SaleData.SaleTransactionID.TransactionID);
            
            Assert.Equal(DateTime.Parse("2024-01-03T03:47:02.9566573Z").ToUniversalTime(), p.SaleData.SaleTransactionID.TimeStamp);
            Assert.Equal("0641ab6f-827f-4650-b877-41e60742815c", p.SaleData.SaleReferenceID);
            Assert.Equal(TokenRequestedType.Transaction, p.SaleData.TokenRequestedType);
            // Assert the POIData object
            Assert.NotNull(p.POIData);
            Assert.NotNull(p.POIData.POITransactionID);
            Assert.Equal("6594d8b8cde34fa05ad7f966", p.POIData.POITransactionID.TransactionID);
            Assert.Equal(DateTime.Parse("2024-01-03T14:47:10.493+11:00"), p.POIData.POITransactionID.TimeStamp);
            Assert.Equal("648f9e0585be9b5dbfc3399a", p.POIData.POIReconciliationID);

            // Assert the PaymentResult object
            Assert.NotNull(p.PaymentResult);
            Assert.Equal(PaymentType.Normal, p.PaymentResult.PaymentType);
            Assert.NotNull(p.PaymentResult.PaymentInstrumentData);
            Assert.Equal(PaymentInstrumentType.Card, p.PaymentResult.PaymentInstrumentData.PaymentInstrumentType);
            Assert.NotNull(p.PaymentResult.PaymentInstrumentData.CardData);
            Assert.Equal(EntryMode.Tapped, p.PaymentResult.PaymentInstrumentData.CardData.EntryMode);
            Assert.Equal("American Express", p.PaymentResult.PaymentInstrumentData.CardData.PaymentBrand);
            Assert.Equal(PaymentBrand.AmericanExpress, p.PaymentResult.PaymentInstrumentData.CardData.PaymentBrandEnum);
            Assert.Equal("2411", p.PaymentResult.PaymentInstrumentData.CardData.Expiry);
            Assert.Equal("379949XXXXX2395", p.PaymentResult.PaymentInstrumentData.CardData.MaskedPAN);
            Assert.NotNull(p.PaymentResult.PaymentInstrumentData.CardData.PaymentToken);
            Assert.Equal(TokenRequestedType.Transaction, p.PaymentResult.PaymentInstrumentData.CardData.PaymentToken.TokenRequestedType);
            Assert.Equal("DCC19DEEF2C0DD0C669A095B2AD47D3A403BAFF5C6F14E", p.PaymentResult.PaymentInstrumentData.CardData.PaymentToken.TokenValue);
            Assert.Equal(DateTime.Parse("2024-12-01T00:00:00.00+11:00"), p.PaymentResult.PaymentInstrumentData.CardData.PaymentToken.ExpiryDateTime);

            // Assert the AmountsResp object
            Assert.NotNull(p.PaymentResult.AmountsResp);
            Assert.Equal(CurrencySymbol.AUD, p.PaymentResult.AmountsResp.Currency);
            Assert.Equal(30.44m, p.PaymentResult.AmountsResp.AuthorizedAmount);
            Assert.Equal(0, p.PaymentResult.AmountsResp.TotalFeesAmount);
            Assert.Equal(0, p.PaymentResult.AmountsResp.CashBackAmount);
            Assert.Equal(0.44m, p.PaymentResult.AmountsResp.SurchargeAmount);
            Assert.Equal(0, p.PaymentResult.AmountsResp.TipAmount);

            // Assert the OnlineFlag
            Assert.True(p.PaymentResult.OnlineFlag);

            // Assert the PaymentAcquirerData object
            Assert.NotNull(p.PaymentResult.PaymentAcquirerData);
            Assert.Equal("308400", p.PaymentResult.PaymentAcquirerData.AcquirerID);
            Assert.Equal("42298585621399", p.PaymentResult.PaymentAcquirerData.MerchantID);
            Assert.Equal("50654236", p.PaymentResult.PaymentAcquirerData.AcquirerPOIID);
            Assert.NotNull(p.PaymentResult.PaymentAcquirerData.AcquirerTransactionID);
            Assert.Equal(DateTime.Parse("2024-01-03T03:47:11.696Z").ToUniversalTime(), p.PaymentResult.PaymentAcquirerData.AcquirerTransactionID.TimeStamp);
            Assert.Equal("839939", p.PaymentResult.PaymentAcquirerData.ApprovalCode);
            Assert.Equal("00", p.PaymentResult.PaymentAcquirerData.ResponseCode);
            Assert.Equal("20240104", p.PaymentResult.PaymentAcquirerData.HostReconciliationID);
            Assert.Equal("007266", p.PaymentResult.PaymentAcquirerData.STAN);
            Assert.Equal("007266144710", p.PaymentResult.PaymentAcquirerData.RRN);

            // Assert the PaymentReceipt object
            Assert.NotNull(p.PaymentReceipt);
            Assert.Single(p.PaymentReceipt);
            Assert.Equal(DocumentQualifier.SaleReceipt, p.PaymentReceipt[0].DocumentQualifier);
            Assert.True(p.PaymentReceipt[0].IntegratedPrintFlag);
            Assert.False(p.PaymentReceipt[0].RequiredSignatureFlag);
            Assert.NotNull(p.PaymentReceipt[0].OutputContent);
            Assert.Equal(OutputFormat.XHTML, p.PaymentReceipt[0].OutputContent.OutputFormat);
            Assert.NotNull(p.PaymentReceipt[0].OutputContent.OutputXHTML);

        }
    }
}
