using Message.AsymmetricEncryption;
using Message.SymmetricEnryption;
using System;
using System.Threading.Tasks;

namespace Message
{
    public class CryptoHelper
    {
        private readonly int _defaultSymmetricKeySize = 256;
        //private readonly int _defaultAsymmetricKeySize = 2048;
        private readonly ISymmetricEncryptionService _symmetricEncryptionService;
        private readonly IXmlBasedAsymmetricEncryptionService _xmlBasedAsymmetricEncryptionService;

        public CryptoHelper(ISymmetricEncryptionService symmetricEncryptionService,
            IXmlBasedAsymmetricEncryptionService xmlBasedAsymmetricEncryptionService)
        {
            _symmetricEncryptionService = symmetricEncryptionService;
            _xmlBasedAsymmetricEncryptionService = xmlBasedAsymmetricEncryptionService;
        }

        public Task<EncryptedMessage> EncryptAsync(AsymmetricPublicKey oneTimeAsymmetricPublicKey, string extremelyConfidentialMessage)
        {
            SymmetricEncryptionResult symmetricEncryptionOfSecretMessage =
                _symmetricEncryptionService.Encrypt(extremelyConfidentialMessage, _defaultSymmetricKeySize);
            string symmetricKeyBase64 = Convert.ToBase64String(symmetricEncryptionOfSecretMessage.SymmetricKey);
            string ivBase64 = Convert.ToBase64String(symmetricEncryptionOfSecretMessage.IV);
            AsymmetricEncryptionResult asymmetricallyEncryptedSymmetricKeyResult =
                _xmlBasedAsymmetricEncryptionService.EncryptWithPublicKeyXml(symmetricKeyBase64, oneTimeAsymmetricPublicKey.PublicKeyXml.ToString());
            EncryptedMessage encryptedMessage = new EncryptedMessage(asymmetricallyEncryptedSymmetricKeyResult.EncryptedAsBase64
                , ivBase64, symmetricEncryptionOfSecretMessage.CipherBase64, oneTimeAsymmetricPublicKey.PublicKeyId);
            return Task.FromResult(encryptedMessage);
        }

        public Task<string> DecryptAsync(string publicPrivateKeyPairXml, EncryptedMessage encryptedMessage)
        {
            byte[] encryptedSymmetricKey = Convert.FromBase64String(encryptedMessage.SymmetricKeyEncryptedBase64);
            AsymmetricDecryptionResult decryptSymmetricKey = _xmlBasedAsymmetricEncryptionService.DecryptWithFullKeyXml
                (encryptedSymmetricKey, publicPrivateKeyPairXml);
            if (decryptSymmetricKey.Success)
            {
                string symmetricKeyBase64 = decryptSymmetricKey.DecryptedMessage;
                byte[] cipherText = Convert.FromBase64String(encryptedMessage.CipherTextBase64);
                byte[] iv = Convert.FromBase64String(encryptedMessage.InitializationVectorBase64);
                byte[] symmetricKey = Convert.FromBase64String(symmetricKeyBase64);
                string secretMessage = _symmetricEncryptionService.Decrypt(cipherText
                    , symmetricKey, iv);

                return Task.FromResult(secretMessage);
            }
            return Task.FromResult("");
        }

    }
}
