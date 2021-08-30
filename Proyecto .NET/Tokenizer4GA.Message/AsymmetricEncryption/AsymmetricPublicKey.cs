using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Message.AsymmetricEncryption
{
    public class AsymmetricPublicKey
    {
        public AsymmetricPublicKey(Guid publicKeyId, XDocument publicKeyXml)
        {
            PublicKeyId = publicKeyId;
            PublicKeyXml = publicKeyXml;
        }

        public Guid PublicKeyId { get; }
        public XDocument PublicKeyXml { get; }
    }
}
