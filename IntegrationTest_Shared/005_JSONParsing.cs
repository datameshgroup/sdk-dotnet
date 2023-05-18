using DataMeshGroup.Fusion.Model;
using Newtonsoft.Json;
using System;
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
    }
}
