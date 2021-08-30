using System;
using System.Collections.Generic;
using System.Text;

namespace Message.SymmetricEnryption
{
    public class SymmetricEncryptionResult : OperationResult
    {
        public byte[] Cipher { get; set; }
        public string CipherBase64 { get; set; }
        public byte[] IV { get; set; }
        public byte[] SymmetricKey { get; set; }
    }
}
