using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DataMeshGroup.Fusion;
using DataMeshGroup.Fusion.Model;

namespace IntegrationTest
{
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
            Assert.NotNull(messageParser.BuildSaleToPOIMessage("00000000", "00000000", "00000000", "00112233445566778899AABBCCDDEEFF0011223344556677", request));

        }
    }
}
