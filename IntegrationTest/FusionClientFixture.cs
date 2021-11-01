using DataMeshGroup.Fusion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class FusionClientFixture: IDisposable
    {
        public FusionClientFixture()
        {
            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));
            Client = new FusionClient(useTestEnvironment: Settings.UseTestEnvironment)
            {
                LoginRequest = new DataMeshGroup.Fusion.Model.LoginRequest(Settings.ProviderIdentification, Settings.ApplicationName, Settings.SoftwareVersion, Settings.CertificationCode, new List<DataMeshGroup.Fusion.Model.SaleCapability>() { DataMeshGroup.Fusion.Model.SaleCapability.CashierStatus }),
                SaleID = Settings.SaleID,
                POIID = Settings.POIID,
                KEK = Settings.KEK
            };
        }

        public async void Dispose()
        {
            await Client.DisconnectAsync();
            Client = null;
            GC.SuppressFinalize(this);
        }

        public Settings Settings { get; private set; }
        public FusionClient Client { get; private set; }
    }

    [CollectionDefinition(nameof(FusionClientFixtureCollection))]
    public class FusionClientFixtureCollection : ICollectionFixture<FusionClientFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
