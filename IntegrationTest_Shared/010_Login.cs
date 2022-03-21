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
    public class LoginIntegrationTest
    {
        readonly FusionClientFixture fusionClientFixture;
        FusionClient Client => fusionClientFixture.Client;
        

        public LoginIntegrationTest(FusionClientFixture fusionClientFixture)
        {
            this.fusionClientFixture = fusionClientFixture;
        }

        [Fact]
        public async Task Login_AutoLogin_Fail()
        {
            LoginRequest originalLoginRequest = fusionClientFixture.Client.LoginRequest;

            await Client.DisconnectAsync();
            Client.LoginRequest = null;
            await Assert.ThrowsAsync<InvalidOperationException>(async () => { await Client.SendAsync(new AbortRequest()); });


            fusionClientFixture.Client.LoginRequest = originalLoginRequest;
        }

        [Fact]
        public async Task Login_AutoLogin_Success()
        {
            await Client.DisconnectAsync();

            // Sending a ReconciliationRequest to force an autologin
            await Client.SendRecvAsync<ReconciliationResponse>(new ReconciliationRequest(reconciliationType: ReconciliationType.SaleReconciliation));

            var r = Client.LoginResponse;
            Assert.NotNull(r);

            // Message type
            Assert.True(r.MessageCategory == MessageCategory.Login);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);

            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);

            // POISystemData
            Assert.NotNull(r.POISystemData);
            Assert.NotNull(r.POISystemData.DateTime);
            Assert.True(r.POISystemData.TokenRequestStatus);

            // POISystemData.POITerminalData
            Assert.NotNull(r.POISystemData.POITerminalData);
            Assert.Equal("Attended", r.POISystemData.POITerminalData.TerminalEnvironment);
            Assert.Equal(POICapability.ICC, r.POISystemData.POITerminalData.POICapabilities.First(c => c == POICapability.ICC));
            Assert.Equal(POICapability.MagStripe, r.POISystemData.POITerminalData.POICapabilities.First(c => c == POICapability.MagStripe));
            Assert.Equal(POICapability.EMVContactless, r.POISystemData.POITerminalData.POICapabilities.First(c => c == POICapability.EMVContactless));
            Assert.NotNull(r.POISystemData.POITerminalData.POISerialNumber);
            
            // POISystemData.POIStatus
            Assert.NotNull(r.POISystemData.POIStatus);
            Assert.Equal(GlobalStatus.OK, r.POISystemData.POIStatus.GlobalStatus);
            Assert.True(r.POISystemData.POIStatus.PEDOKFlag);
            Assert.True(r.POISystemData.POIStatus.CardReaderOKFlag);
            Assert.Equal(PrinterStatus.OK, r.POISystemData.POIStatus.PrinterStatus);
            Assert.True(r.POISystemData.POIStatus.CommunicationOKFlag);
        }

    }
}
