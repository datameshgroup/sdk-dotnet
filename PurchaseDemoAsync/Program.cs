﻿using DataMeshGroup.Fusion;
using DataMeshGroup.Fusion.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseDemo
{
    public class PurchaseDemoAsync : IDisposable
    {
        private readonly IFusionClient fusionClient;
        
        public PurchaseDemoAsync(string saleID, string poiID, string kek, LoginRequest loginRequest)
        {
            fusionClient = new FusionClient(useTestEnvironment: true)
            {
                SaleID = saleID,
                POIID = poiID,
                KEK = kek,
                LoginRequest = loginRequest
            };
        }

        public void Dispose()
        {
            fusionClient?.Dispose();
        }

        public async Task<bool> DoPayment(PaymentRequest paymentRequest)
        {
            SaleToPOIMessage request = null;
            bool result = false;
            try
            {
                request = await fusionClient.SendAsync(paymentRequest);

                bool waitingForResponse = true;
                do
                {
                    MessagePayload MessagePayload = await fusionClient.RecvAsync();
                    switch (MessagePayload)
                    {
                        case LoginResponse r: // Autologin must have been sent
                            Console.WriteLine($"AutoLogin result = {r.Response?.Result.ToString() ?? "Unknown"}, ErrorCondition = {r.Response?.ErrorCondition}, Result = {r.Response?.AdditionalResponse}");
                            break;

                        case PaymentResponse r:
                            // Validate SaleTransactionID
                            if (!r.SaleData.SaleTransactionID.Equals(paymentRequest.SaleData.SaleTransactionID))
                            {
                                Console.WriteLine($"Unknown SaleId {r.SaleData.SaleTransactionID.TransactionID}");
                                break;
                            }
                            Console.WriteLine($"Payment result = {r.Response.Result.ToString() ?? "Unknown"}, ErrorCondition = {r.Response?.ErrorCondition}, Result = {r.Response?.AdditionalResponse}");
                            result = (r.Response.Result == Result.Success) || (r.Response.Result == Result.Partial);
                            waitingForResponse = false;
                            break;
                        default:
                            Console.WriteLine("Unknown result");
                            break;

                    }
                }
                while (waitingForResponse);
            }
            catch (FusionException fe)
            {
                Console.WriteLine($"Exception processing payment {fe.Message} {fe.StackTrace}");
                if (fe.ErrorRecoveryRequired && request != null)
                {
                    // await error handling
                    result = await HandleErrorRecovery(request);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception processing payment {e.Message} {e.StackTrace}");
            }
            
            return result;
        }

        private async Task<bool> HandleErrorRecovery(SaleToPOIMessage request)
        {
            Console.WriteLine($"Error recovery...");

            bool result = false;

            TimeSpan timeout = TimeSpan.FromSeconds(90);
            TimeSpan requestDelay = TimeSpan.FromSeconds(10);
            Stopwatch timeoutTimer = new Stopwatch();
            timeoutTimer.Start();

            bool waitingForResponse = true;
            do
            {
                TransactionStatusRequest transactionStatusRequest = new TransactionStatusRequest()
                {
                    MessageReference = new MessageReference()
                    {
                        MessageCategory = request.MessageHeader.MessageCategory,
                        POIID = request.MessageHeader.POIID,
                        SaleID = request.MessageHeader.SaleID,
                        ServiceID = request.MessageHeader.ServiceID
                    }
                };

                try
                {
                    TransactionStatusResponse transactionStatusResponse = await fusionClient.SendRecvAsync<TransactionStatusResponse>(transactionStatusRequest);

                    // If the response to our TransactionStatus request is "Success", we have a PaymentResponse to check
                    if (transactionStatusResponse.Response.Result == Result.Success)
                    {
                        Response paymentResponse = transactionStatusResponse.RepeatedMessageResponse.RepeatedResponseMessageBody.PaymentResponse.Response;
                        Console.WriteLine($"Payment result = {paymentResponse.Result.ToString() ?? "Unknown"}, ErrorCondition = {paymentResponse?.ErrorCondition}, Result = {paymentResponse?.AdditionalResponse}");
                        result = paymentResponse.Result is Result.Success or Result.Partial;
                        waitingForResponse = false;
                    }

                    // else check if the transaction is still in progress, and we haven't reached out timeout
                    else if (transactionStatusResponse.Response.ErrorCondition == ErrorCondition.InProgress && timeoutTimer.Elapsed < timeout)
                    {
                        Console.WriteLine("Payment in progress...");
                    }

                    // otherwise, fail
                    else
                    {
                        waitingForResponse = false;
                    }
                }
                catch (NetworkException)
                {
                    Console.WriteLine("Waiting for connection...");
                }
                catch (DataMeshGroup.Fusion.TimeoutException)
                {
                    Console.WriteLine("Timeout waiting for result...");
                }
                catch (Exception)
                {
                    Console.WriteLine("Waiting for connection...");
                }

                if (waitingForResponse)
                {
                    await Task.Delay(requestDelay);
                }

            } while (waitingForResponse);


            return result;
        }
    }

    class Program
    {
        private static async Task Main(string[] args)
        {
            // POS identification provided by DataMesh
            string saleID = "Clinton Pos";
            string poiID = "PLB00002";
            string kek = "44DACB2A22A4A752ADC1BBFFE6CEFB589451E0FFD83F8B21";
            string certificationCode = "98cf9dfc-0db7-4a92-8b8cb66d4d2d7169";
            // POS identification provided by POS vendor
            string providerIdentification = "Company A";
            string applicationName = "POS Retail";
            string softwareVersion = "01.00.00";

            // Build logon request
            LoginRequest loginRequest = new LoginRequest(providerIdentification, applicationName, softwareVersion, certificationCode);

            // Build payment request
            decimal amount = 42.00M;
            PaymentRequest paymentRequest = new PaymentRequest(
                transactionID: DateTime.UtcNow.ToString("yyyyMMddhhmmssffff", CultureInfo.InvariantCulture),
                requestedAmount: amount,
                saleItems: new List<SaleItem>()
                {
                    new SaleItem()
                    {
                        ItemID = "1",
                        ProductCode = "1",
                        ProductLabel = "Product",
                        Quantity = 1,
                        UnitOfMeasure = UnitOfMeasure.Other,
                        ItemAmount = amount,
                        UnitPrice = amount
                    }
                });

            try
            {
                // Perform 'async' purchase demo
                bool asyncResult = await new PurchaseDemoAsync(saleID, poiID, kek, loginRequest).DoPayment(paymentRequest);
                Console.WriteLine($"Async purchase result: {asyncResult}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception processing payment {e.Message} {e.StackTrace}");
            }
        }
    }
}
