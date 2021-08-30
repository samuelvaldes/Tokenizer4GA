using System;
using System.Collections.Generic;
using System.Text;

namespace Message.AsymmetricEncryption
{
    public interface IXmlBasedAsymmetricEncryptionService
    {
        AsymmetricKeyPairGenerationResult GenerateKeysAsXml(int keySizeBits);
        AsymmetricEncryptionResult EncryptWithPublicKeyXml(string message, string publicKeyAsXml);
        AsymmetricDecryptionResult DecryptWithFullKeyXml(byte[] cipherBytes, string fullKeyPairXml);
    }

    public class AsymmetricKeyPairGenerationResult : OperationResult
    {
        public string PublicKeyXml { get; set; }
        public string PublicPrivateKeyPairXml { get; set; }
    }

    public class AsymmetricEncryptionResult : OperationResult
    {
        public byte[] EncryptedAsBytes { get; set; }
        public string EncryptedAsBase64 { get; set; }
    }

    public class AsymmetricDecryptionResult : OperationResult
    {
        public string DecryptedMessage { get; set; }
    }
}
