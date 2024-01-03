using DataMeshGroup.Fusion.Model;
using System;
using Xunit;



namespace DataMeshGroup.Fusion.IntegrationTest
{
#if (!NETFRAMEWORK)
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest")]
#else
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest_Net48")]
#endif

    public class UnitTests
    {
        private readonly IMessageParser messageParser;
        private readonly PaymentRequest request;
        

        public UnitTests()
        {
            messageParser = new DataMeshGroup.Fusion.NexoMessageParser();
            request = new DataMeshGroup.Fusion.Model.PaymentRequest(DateTime.Now.ToString("yyMMddHHmmssfff"), 1.00M);
        }
         

        [Fact]
        public void MessageParser_InvalidFields()
        {
            // Invalid serviceID
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("", "00000000", "00000000", "00112233445566778899AABBCCDDEEFF0011223344556677", request));
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage(null, "00000000", "00000000", "00112233445566778899AABBCCDDEEFF0011223344556677", request));
            // Invalid saleID
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", "", "00000000", "00112233445566778899AABBCCDDEEFF0011223344556677", request));
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", null, "00000000", "00112233445566778899AABBCCDDEEFF0011223344556677", request));
            // Invalid poiID
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", "00000000", "", "00112233445566778899AABBCCDDEEFF0011223344556677", request));
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", "00000000", null, "00112233445566778899AABBCCDDEEFF0011223344556677", request));
            // Invalid kek
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", "00000000", "00000000", "00112233445566778899", request));
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", "00000000", "00000000", "00112233445566778899AABBCCDDEEFF001122334455667", request));
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", "00000000", "00000000", "00112233445566778899AABBCCDDEEFF00112233445566777", request));
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", "00000000", "00000000", "", request));
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", "00000000", "00000000", null, request));
            // Invalid messageBody
            Assert.Throws<ArgumentException>(() => messageParser.BuildSaleToPOIMessage("00000000", "00000000", "00000000", "00112233445566778899AABBCCDDEEFF0011223344556677", null));

            // valid 
            SaleToPOIMessage saleToPOIMessage = messageParser.BuildSaleToPOIMessage("00000000", "00000000", "00000000", "00112233445566778899AABBCCDDEEFF0011223344556677", request);
            Assert.NotNull(saleToPOIMessage);

            string json = messageParser.MessagePayloadToString(saleToPOIMessage.MessagePayload);
        }

        [Fact]
        public void PaymentBrand()
        {
            void AssertPaymentBrand(string paymentBrand, PaymentBrand paymentBrandEnum) 
            {
                CardData cd = new CardData() { PaymentBrand = paymentBrand };
                Assert.Equal(cd.PaymentBrandEnum, paymentBrandEnum);
            }
            AssertPaymentBrand("VISA", Model.PaymentBrand.VISA);
            AssertPaymentBrand("MasterCard", Model.PaymentBrand.MasterCard);
            AssertPaymentBrand("American Express", Model.PaymentBrand.AmericanExpress);
            AssertPaymentBrand("Diners Club", Model.PaymentBrand.DinersClub);
            AssertPaymentBrand("JCB", Model.PaymentBrand.JCB);
            AssertPaymentBrand("UnionPay", Model.PaymentBrand.UnionPay);
            AssertPaymentBrand("CUP Debit", Model.PaymentBrand.CUPDebit);
            AssertPaymentBrand("Discover", Model.PaymentBrand.Discover);
            AssertPaymentBrand("WeChat Pay", Model.PaymentBrand.WeChatPay);
            AssertPaymentBrand("CryptoDotCom", Model.PaymentBrand.CryptoDotCom);
            AssertPaymentBrand("Fastcard", Model.PaymentBrand.Fastcard);
            AssertPaymentBrand("eTicket", Model.PaymentBrand.eTicket);
            AssertPaymentBrand("Digital Pass", Model.PaymentBrand.DigitalPass);
            AssertPaymentBrand("NSW TSS", Model.PaymentBrand.NSWTSS);
            AssertPaymentBrand("QLD TSS", Model.PaymentBrand.QLDTSS);
            AssertPaymentBrand("ACT TSS", Model.PaymentBrand.ACTTSS);
            AssertPaymentBrand("VIC TSS", Model.PaymentBrand.VICTSS);
            AssertPaymentBrand("TAS TSS", Model.PaymentBrand.TASTSS);
            AssertPaymentBrand("NT TSS", Model.PaymentBrand.NTTSS);
            AssertPaymentBrand("Unknown payment brand", Model.PaymentBrand.Unknown);
            AssertPaymentBrand("Card", Model.PaymentBrand.Card);
            AssertPaymentBrand("Other", Model.PaymentBrand.Other);
        }
    }
}
