using System;
using System.Collections.Generic;
using System.Text;

namespace Message
{
    public class EncryptedMessage
    {
        public EncryptedMessage() { }

        public EncryptedMessage(string symmetricKeyEncryptedBase64,
            string initializationVectorBase64,
            string cipherTextBase64,
            Guid asymmetricKeyId)
        {
            SymmetricKeyEncryptedBase64 = symmetricKeyEncryptedBase64;
            InitializationVectorBase64 = initializationVectorBase64;
            CipherTextBase64 = cipherTextBase64;
            AsymmetricKeyId = asymmetricKeyId;
        }

        public string SymmetricKeyEncryptedBase64 { get; set; }
        public string InitializationVectorBase64 { get; set; }
        public string CipherTextBase64 { get; set; }
        public Guid AsymmetricKeyId { get; set; }
    }
}
