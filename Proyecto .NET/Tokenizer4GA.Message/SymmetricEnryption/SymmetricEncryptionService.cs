using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
//using RandomNumberGenerator;
using System.IO;

namespace Message.SymmetricEnryption
{
    public class SymmetricEncryptionService : ISymmetricEncryptionService
    {
        private readonly SymmetricAlgorithm _symmetricAlgorithm;

        public SymmetricEncryptionService()
        {
            _symmetricAlgorithm = new AesCryptoServiceProvider();
        }

        public SymmetricEncryptionService(SymmetricAlgorithm symmetricAlgorithm)
        {
            if (symmetricAlgorithm == null) throw new ArgumentNullException("SymmetricAlgorithm");
            _symmetricAlgorithm = symmetricAlgorithm;
        }

        public SymmetricEncryptionResult Encrypt(string messageToEncrypt, int symmetricKeyLengthBits)
        {
            SymmetricEncryptionResult encryptionResult = new SymmetricEncryptionResult();
            try
            {
                //first test if bit length is valid
                if (_symmetricAlgorithm.ValidKeySize(symmetricKeyLengthBits))
                {
                    _symmetricAlgorithm.KeySize = symmetricKeyLengthBits;
                    using (MemoryStream mem = new MemoryStream())
                    {
                        CryptoStream crypto = new CryptoStream(mem, _symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
                        byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(messageToEncrypt);
                        crypto.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                        crypto.FlushFinalBlock();
                        byte[] encryptedBytes = mem.ToArray();
                        string encryptedBytesBase64 = Convert.ToBase64String(encryptedBytes);
                        encryptionResult.Success = true;
                        encryptionResult.Cipher = encryptedBytes;
                        encryptionResult.CipherBase64 = encryptedBytesBase64;
                        encryptionResult.IV = _symmetricAlgorithm.IV;
                        encryptionResult.SymmetricKey = _symmetricAlgorithm.Key;
                    }
                }
                else
                {
                    string NL = Environment.NewLine;
                    StringBuilder exceptionMessageBuilder = new StringBuilder();
                    exceptionMessageBuilder.Append("The provided key size - ")
                        .Append(symmetricKeyLengthBits).Append(" bits - is not valid for this algorithm.");
                    exceptionMessageBuilder.Append(NL)
                        .Append("Valid key sizes: ").Append(NL);
                    KeySizes[] validKeySizes = _symmetricAlgorithm.LegalKeySizes;
                    foreach (KeySizes keySizes in validKeySizes)
                    {
                        exceptionMessageBuilder.Append("Min: ")
                            .Append(keySizes.MinSize).Append(NL)
                            .Append("Max: ").Append(keySizes.MaxSize).Append(NL)
                            .Append("Step: ").Append(keySizes.SkipSize);
                    }
                    throw new CryptographicException(exceptionMessageBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                encryptionResult.Success = false;
                encryptionResult.ExceptionMessage = ex.Message;
            }

            return encryptionResult;
        }

        public string Decrypt(byte[] cipherTextBytes, byte[] key, byte[] iv)
        {
            _symmetricAlgorithm.IV = iv;
            _symmetricAlgorithm.Key = key;
            using (MemoryStream mem = new MemoryStream())
            {
                CryptoStream crypto = new CryptoStream(mem, _symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write);
                crypto.Write(cipherTextBytes, 0, cipherTextBytes.Length);
                crypto.FlushFinalBlock();
                return Encoding.UTF8.GetString(mem.ToArray());
            }
        }
    }
}
