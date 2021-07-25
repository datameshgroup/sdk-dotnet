﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion.Model
{
    public static class CertificateValidation
    {

        #region Certificates
        /// <summary>
        /// Test environment RootCA for cloudposintegration.io
        /// </summary>
        private const string TestEnvironment = @"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";

        /// <summary>
        /// Production RootCA for prod.datameshgroup.io
        /// </summary>
        private const string ProductionEnvironment = @"MIIEwzCCA6ugAwIBAgIIAasJ1Kr6ywkwDQYJKoZIhvcNAQELBQAwgagxCzAJBgNVBAYTAkFVMQwwCgYDVQQIDANOU1cxDzANBgNVBAcMBlN5ZG5leTEMMAoGA1UECgwDRE1HMQswCQYDVQQLDAJJVDEWMBQGA1UEDAwNRE1HQ0FST09UUHJvZDEgMB4GA1UEAwwXcm9vdGNhLmRhdGFtZXNoZ3JvdXAuaW8xJTAjBgkqhkiG9w0BCQEWFmFkbWluQGRhdGFtZXNoZ3JvdXAuaW8wIBcNMjAwNjIzMDAwMDAwWhgPMjA1MDA3MDgwMDAwMDBaMIGoMQswCQYDVQQGEwJBVTEMMAoGA1UECAwDTlNXMQ8wDQYDVQQHDAZTeWRuZXkxDDAKBgNVBAoMA0RNRzELMAkGA1UECwwCSVQxFjAUBgNVBAwMDURNR0NBUk9PVFByb2QxIDAeBgNVBAMMF3Jvb3RjYS5kYXRhbWVzaGdyb3VwLmlvMSUwIwYJKoZIhvcNAQkBFhZhZG1pbkBkYXRhbWVzaGdyb3VwLmlvMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAy/iDG+t3gHJqWC9VRY0YvK1L/WKm6F0uRYkHivbvN82e6IdoiUSrYJlcZelpUmTaZUNxZjhPXLpRXAovRSVnUebvDomQN+h71yfsBVYMKiJZ2Th5magZI9hBzyrA1PIbHJcTT+eC5V9tQmb7h9dCO8O3BjDcM84khKP34rLjagNsyYpk3gdn86vb+yak6pUVe/WL/Vc4sHI+e1nGrsDvsd1V1z3vjeKmM/Ur8uBD95fv0d80+tM8phrnP96Bbq8pN92E1qDUzQ/d/PX2NG92m/P5dh309w/Bl1my+heDgAc/G3GRgJ8XJLfXRcVFXfB4tAZZ2B8NwnB9J9fCSq5GOQIDAQABo4HsMIHpMCsGA1UdIwQkMCKAIEgUCV1/saqmdlIoaHxZ1mQ4NuXhqlF+KFHNtEYKHYDAMCkGA1UdDgQiBCBIFAldf7GqpnZSKGh8WdZkODbl4apRfihRzbRGCh2AwDAMBgNVHRMEBTADAQH/MAsGA1UdDwQEAwIBBjA0BgNVHREELTArghdyb290Y2EuZGF0YW1lc2hncm91cC5pb4IQcm9vdGNhLm15YXhpcy5pbzA+BggrBgEFBQcBAQQyMDAwLgYIKwYBBQUHMAGGImh0dHBzOi8vcHJvZC5kYXRhbWVzaGdyb3VwLmlvOjQ1MTAwDQYJKoZIhvcNAQELBQADggEBAHyo8xiBQv7sD4r5uCEQibTYOiFWLfL2wW3zc1IyUNtnCdVTT0A20xutlb+n7vnMfTctQ8dBLqJzvRlZMm/6hqWQSMXKTBRE4XoLVCLBNtIRqfOlvGMHkrR+uwtYxxx6b5w38PPtZGx5/wSdBkLvO8K0Kkb9ovQy0sFuO+QTrSs4hz8tMWIrOMlMb8syZBkebIn5tNnE0CX7zsit4oBVckkInDyKdf5vhkw/ax2Y5pj0bfOLdfp2bogPuK23fzLRHS4TV5OmSv2ecyPPRDHjSeWMwU/XQkwfsMojXvriULHAWqmERhi5D1jBuUWW8j0Y2ipES5Fn7tJEkHgLmqFOKgk=";
        #endregion


        public static UnifyRootCA RootCA { get; set; }

        /// <summary>
        /// Verifies the remote Secure Sockets Layer (SSL) certificate used for authentication.
        /// </summary>
        /// <param name="sender">An object that contains state information for this validation.</param>
        /// <param name="certificate">The certificate used to authenticate the remote party.</param>
        /// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="sslPolicyErrors">One or more errors associated with the remote certificate.</param>
        /// <returns>A System.Boolean value that determines whether the specified certificate is accepted for authentication.</returns>
        public static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true; // TODO : need to check for certs above

            //if (RootCA == UnifyRootCA.System)
            //{
            //    return sslPolicyErrors == SslPolicyErrors.None;
            //}
            //var validRootCertificates = new[]
            //{
            //    Convert.FromBase64String(ProductionEnvironment), // Set your own root certificates (format CER)
            //};

            //if (chain.ChainStatus.Any(status => status.Status != X509ChainStatusFlags.UntrustedRoot))
            //    return false;

            //foreach (var element in chain.ChainElements)
            //{
            //    foreach (var status in element.ChainElementStatus)
            //    {
            //        if (status.Status == X509ChainStatusFlags.UntrustedRoot)
            //        {
            //            // improvement: we could validate that the request matches an internal domain by using request.RequestUri in addition to the certicate validation

            //            // Check that the root certificate matches one of the valid root certificates
            //            if (validRootCertificates.Any(cert => cert.SequenceEqual(element.Certificate.RawData)))
            //                continue; // Process the next status
            //        }

            //        return false;
            //    }
            //}

            //// Return true only if all certificates of the chain are valid
            //return true;
        }


    }
}
