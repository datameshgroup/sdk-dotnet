﻿using DataMeshGroup.Fusion;
using DataMeshGroup.Fusion.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataMeshGroup.Fusion.IntegrationTest
{
    public class FusionClientFixture: IDisposable
    {
        public FusionClientFixture()
        {
            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("IntegrationTestSettings.json"));
            Client = new FusionClient(useTestEnvironment: Settings.UseTestEnvironment)
            {
                LoginRequest = new DataMeshGroup.Fusion.Model.LoginRequest(Settings.ProviderIdentification, Settings.ApplicationName, Settings.SoftwareVersion, Settings.CertificationCode, new List<DataMeshGroup.Fusion.Model.SaleCapability>() { DataMeshGroup.Fusion.Model.SaleCapability.CashierStatus, DataMeshGroup.Fusion.Model.SaleCapability.PrinterReceipt }),
                SaleID = Settings.SaleID,
                POIID = Settings.POIID,
                KEK = Settings.KEK,
                CustomURL = Settings.CustomURL
            };
            Client.URL = !string.IsNullOrEmpty(Settings.CustomURL) ? UnifyURL.Custom : Client.URL;
            Client.OnLog += (s,e) => { File.AppendAllText("IntegrationTest.log", $"{DateTime.Now} {e.LogLevel}\t\t{e.Data}{Environment.NewLine}"); };
            Client.LogLevel = LogLevel.Trace;
            SaleToPOIRequestHistory = new List<SaleToPOIMessage>();
        }

        private void Client_OnLog(object sender, LogEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async void Dispose()
        {
            await Client.DisconnectAsync();
            Client = null;
            GC.SuppressFinalize(this);
        }

        public Settings Settings { get; private set; }
        public FusionClient Client { get; private set; }

        /// <summary>
        /// Retain a history of SaleToPOIRequest messages sent
        /// </summary>
        public List<SaleToPOIMessage> SaleToPOIRequestHistory { get; private set; }
        public SaleToPOIMessage BeforeLastSaleToPOIRequest { get; internal set; }
    }

    [CollectionDefinition(nameof(FusionClientFixtureCollection))]
    public class FusionClientFixtureCollection : ICollectionFixture<FusionClientFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
