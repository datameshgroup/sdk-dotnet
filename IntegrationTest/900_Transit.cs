using DataMeshGroup.Fusion;
using System;
using System.Threading.Tasks;
using DataMeshGroup.Fusion.Model;
using DataMeshGroup.Fusion.Model.Transit;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataMeshGroup.Fusion.IntegrationTest
{
#if (!NETFRAMEWORK)
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest")]
#else
    [TestCaseOrderer("DataMeshGroup.Fusion.IntegrationTest.AlphabeticalOrderer", "IntegrationTest_Net48")]
#endif


    [Collection(nameof(FusionClientFixtureCollection))]
    public class TransitIntegrationTest
    {
        readonly FusionClientFixture fusionClientFixture;

        FusionClient Client => fusionClientFixture.Client;

        public TransitIntegrationTest(FusionClientFixture fusionClientFixture)
        {
            this.fusionClientFixture = fusionClientFixture;
        }

        [Fact]
        public async Task Purchase_With_Transit_Data()
        {
            PaymentRequest request = new PaymentRequest()
            {
                SaleData = new SaleData()
                {
                    OperatorID = "4452",
                    ShiftNumber = "2023-04-06_01",
                    SaleTransactionID = new TransactionIdentification()
                    {
                        TransactionID = "0347d00e-5d13-4043-b92b-6bf32381ab16",
                        TimeStamp = DateTime.UtcNow
                    },
                    SaleTerminalData = new SaleTerminalData(false)
                    {
                        DeviceID = "0347d00e-5d13-aaaa-b92b-98235478239875"
                    }
                },
                PaymentTransaction = new PaymentTransaction()
                {
                    AmountsReq = new AmountsReq()
                    {
                        Currency = CurrencySymbol.AUD,
                        RequestedAmount = 30.1M
                    },
                    SaleItem = new List<SaleItem>()
                    {
                        new SaleItem()
                        {
                            ItemID = 0,
                            ProductCode = "MeteredFare",
                            ProductLabel = "TRF 1 SINGLE",
                            UnitOfMeasure = UnitOfMeasure.Kilometre,
                            UnitPrice = 2M,
                            Quantity = 8M,
                            ItemAmount = 20M
                        },
                        new SaleItem()
                        {
                            ItemID = 1,
                            ProductCode = "SAGovLevy",
                            ProductLabel = "SA GOV LEVY",
                            UnitOfMeasure = UnitOfMeasure.Other,
                            UnitPrice = 8M,
                            Quantity = 1M,
                            ItemAmount = 8M,
                            Tags = new List<string>()
                            {
                                "subtotal"
                            }
                        },
                        new SaleItem()
                        {
                            ItemID = 2,
                            ProductCode = "LateNightFee",
                            ProductLabel = "Late Night Fee",
                            UnitOfMeasure = UnitOfMeasure.Other,
                            UnitPrice = 2.1M,
                            Quantity = 1M,
                            ItemAmount = 2.1M,
                            Tags = new List<string>()
                            {
                                "extra"
                            }
                        }
                    }
                },
                PaymentData = new PaymentData()
                {
                    PaymentType = PaymentType.Normal
                },
                ExtensionData = new ExtensionData()
                {
                    TransitData = new TransitData()
                    {
                        IsWheelchairEnabled = true,
                        ODBS = "test",
                        Tags = new List<string>()
                        {
                            "NTAllowTSSSubsidy", "NTAllowTSSLift", "QLDAllowTSSSubsidy", "NSWAllowTSSLift", "NSWAllowTSSSubsidy"
                        },
                        Trip = new Trip()
                        {
                            TotalDistanceTravelled = 29.4M,
                            Pickup = new Stop()
                            {
                                StopIndex = 0,
                                StopName = "Richmond",
                                Latitude = "-37.82274517047244",
                                Longitude = "144.98394642094434",
                                Timestamp = DateTime.Parse("2023-04-06T03:00:15+0000")
                            },
                            Destination = new Stop()
                            {
                                StopIndex = 1,
                                StopName = "Beaumaris",
                                Latitude = "-37.988864997462048",
                                Longitude = "145.04484379736329",
                                Timestamp = DateTime.Parse("2023-04-06T03:39:30+0000")
                            }
                        }
                    }
                }
            };



            _ = await Client.SendAsync(request);

            List<MessagePayload> responses = new List<MessagePayload>();
            PaymentResponse r = await Client.RecvAsync<PaymentResponse>(new CancellationTokenSource(TimeSpan.FromSeconds(60)).Token);

            //
            Assert.NotNull(r);
            Assert.IsType<PaymentResponse>(r);

            // Message type
            Assert.True(r.MessageCategory == MessageCategory.Payment);
            Assert.True(r.MessageClass == MessageClass.Service);
            Assert.True(r.MessageType == MessageType.Response);

            // Response
            Assert.True(r.Response.Success);
            Assert.Equal(Result.Success, r.Response.Result);
        }


    }
}
