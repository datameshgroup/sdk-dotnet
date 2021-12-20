using DataMeshGroup.Fusion.Model;
using Newtonsoft.Json;

namespace DataMeshGroup.Fusion
{
    public static class SecurityTrailerHelper
    {
        /// <summary>
        /// Validates a security trailer has the correct MAC & EncryptedKey
        /// </summary>
        /// <param name="kek">The KEK used to encrypt the session key</param>
        /// <param name="messageHeaderJson">Message header JSON</param>
        /// <param name="payloadDescription">Payload description. From messageHeader.GetMessageDescription()</param>
        /// <param name="payloadJson">Payload JSON</param>
        /// <param name="expectedMAC">The expected MAC. From securityTrailer.AuthenticatedData.Recipient.MAC</param>
        /// <param name="expectedEncryptedKey">The expected encryptedKe. From securityTrailer.AuthenticatedData.Recipient.KEK.EncryptedKey</param>
        public static void ValidateSecurityTrailer(string kek, string messageHeaderJson, string payloadDescription, string payloadJson, string expectedMAC, string expectedEncryptedKey)
        {
            if (string.IsNullOrEmpty(kek))
            {
                throw new MessageFormatException($"SecurityTrailer validation error. {nameof(kek)} is null or empty");
            }
            if (string.IsNullOrEmpty(messageHeaderJson))
            {
                throw new MessageFormatException($"SecurityTrailer validation error. {nameof(messageHeaderJson)} is null or empty");
            }
            if (string.IsNullOrEmpty(payloadDescription))
            {
                throw new MessageFormatException($"SecurityTrailer validation error. {nameof(payloadDescription)} is null or empty");
            }
            if (string.IsNullOrEmpty(payloadJson))
            {
                throw new MessageFormatException($"SecurityTrailer validation error. {nameof(payloadJson)} is null or empty");
            }
            if (string.IsNullOrEmpty(expectedMAC))
            {
                throw new MessageFormatException($"SecurityTrailer validation error. {nameof(expectedMAC)} is null or empty");
            }
            if (string.IsNullOrEmpty(expectedEncryptedKey))
            {
                throw new MessageFormatException($"SecurityTrailer validation error. {nameof(expectedEncryptedKey)} is null or empty");
            }

            var sessionKey = Crypto.DecryptWithTripleDES(expectedEncryptedKey, kek);

            string macBody = $"\"MessageHeader\":{messageHeaderJson},\"{payloadDescription}\":{payloadJson}";
            string sha256 = Crypto.HashBySHA256(macBody);
            string buffer = sha256 + 8000000000000000;
            string encryptedSha256 = Crypto.EncryptWithTripleDES(buffer, sessionKey);
            string mac = encryptedSha256.Substring(encryptedSha256.Length - 16);
            string encryptedKey = Crypto.EncryptWithTripleDES(sessionKey, kek);

            if (mac != expectedMAC)
            {
                throw new MessageFormatException($"SecurityTrailer validation error. MAC error. expected {expectedMAC}, got {mac}");
            }

            if (encryptedKey != expectedEncryptedKey)
            {
                throw new MessageFormatException($"SecurityTrailer validation error. EncryptedKey error. expected {expectedEncryptedKey}, got {encryptedKey}");
            }
        }

        public static SecurityTrailer GenerateSecurityTrailer(string kek, MessageHeader messageHeader, object messagePayload, bool useTestKeyIdentifier)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

            string sessionKey = Crypto.GenerateKey();
            string macBody = $"\"MessageHeader\":{JsonConvert.SerializeObject(messageHeader, Formatting.None, jsonSerializerSettings)},\"{messageHeader.GetMessageDescription()}\":{JsonConvert.SerializeObject(messagePayload, Formatting.None, jsonSerializerSettings)}";
            string sha256 = Crypto.HashBySHA256(macBody);
            string buffer = sha256 + 8000000000000000;
            string encryptedSha256 = Crypto.EncryptWithTripleDES(buffer, sessionKey);
            string mac = encryptedSha256.Substring(encryptedSha256.Length - 16);
            string encryptedKey = Crypto.EncryptWithTripleDES(sessionKey, kek);

            return new SecurityTrailer()
            {
                ContentType = "id-ct-authData",
                AuthenticatedData = new AuthenticatedData()
                {
                    Version = "v0",
                    Recipient = new Recipient()
                    {
                        KEK = new KEK()
                        {
                            Version = "v4",
                            KEKIdentifier = new KEKIdentifier()
                            {
                                KeyIdentifier = useTestKeyIdentifier ? "SpecV2TestMACKey" : "SpecV2ProdMACKey",
                                KeyVersion = useTestKeyIdentifier ? "20191122164326.594" : "20191122164326.594",
                            },
                            KeyEncryptionAlgorithm = new KeyEncryptionAlgorithm()
                            {
                                Algorithm = "des-ede3-cbc"
                            },
                            EncryptedKey = encryptedKey
                        },
                        MACAlgorithm = new MACAlgorithm()
                        {
                            Algorithm = "id-retail-cbc-mac-sha-256"
                        },
                        EncapsulatedContent = new EncapsulatedContent()
                        {
                            ContentType = "id-data"
                        },
                        MAC = mac
                    }
                }
            };
        }

        /// <summary>
        /// Validates that the kek passed in is a valid KEK
        /// </summary>
        /// <param name="kek"></param>
        public static bool ValidateKEK(string kek)
        {
            return Crypto.ValidateKEK(kek);
        }

    }
}
