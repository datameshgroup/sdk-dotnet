using DataMeshGroup.Fusion.Model;
using Newtonsoft.Json;

namespace DataMeshGroup.Fusion
{
    public static class SecurityTrailerHelper
    {
        /// <summary>
        /// Validate security trailer
        /// </summary>
        /// <param name="kek"></param>
        /// <param name="messageHeader"></param>
        /// <param name="securityTrailer"></param>
        /// <param name="messageHeaderJson"></param>
        /// <param name="payloadJson"></param>
        /// <exception cref="MessageFormatException">Thrown on MAC/EncryptedKey validation error</exception>
        public static void ValidateSecurityTrailer(string kek, MessageHeader messageHeader, SecurityTrailer securityTrailer, string messageHeaderJson, string payloadJson)
        {
            if (messageHeader == null || securityTrailer == null)
            {
                throw new MessageFormatException("SecurityTrailer validation error. messageHeader == null || securityTrailer == null");
            }

            var sessionKey = Crypto.DecryptWithTripleDES(securityTrailer.AuthenticatedData.Recipient.KEK.EncryptedKey, kek);

            string macBody = $"\"MessageHeader\":{messageHeaderJson},\"{messageHeader.GetMessageDescription()}\":{payloadJson}";
            string sha256 = Crypto.HashBySHA256(macBody);
            string buffer = sha256 + 8000000000000000;
            string encryptedSha256 = Crypto.EncryptWithTripleDES(buffer, sessionKey);
            string mac = encryptedSha256.Substring(encryptedSha256.Length - 16);
            string encryptedKey = Crypto.EncryptWithTripleDES(sessionKey, kek);

            if (mac != securityTrailer.AuthenticatedData.Recipient.MAC)
            {
                throw new MessageFormatException($"SecurityTrailer validation error. MAC error. expected {securityTrailer.AuthenticatedData.Recipient.MAC}, got {mac}");
            }

            if (encryptedKey != securityTrailer.AuthenticatedData.Recipient.KEK.EncryptedKey)
            {
                throw new MessageFormatException($"SecurityTrailer validation error. EncryptedKey error. expected {securityTrailer.AuthenticatedData.Recipient.KEK.EncryptedKey}, got {encryptedKey}");
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

    }
}
