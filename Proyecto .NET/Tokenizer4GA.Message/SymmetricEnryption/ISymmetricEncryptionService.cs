using System;
using System.Collections.Generic;
using System.Text;

namespace Message.SymmetricEnryption
{
    public interface ISymmetricEncryptionService
    {
        SymmetricEncryptionResult Encrypt(string messageToEncrypt, int symmetricKeyLengthBits);
        string Decrypt(byte[] cipherTextBytes, byte[] key, byte[] iv);
    }
}
