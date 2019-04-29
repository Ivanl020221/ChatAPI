using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;


namespace ChatAPI
{
    public class Sescry
    {
        RSACryptoServiceProvider RSACrypto = new RSACryptoServiceProvider(2048);

        public RSAParameters privateKey;

        public RSAParameters publicKey;

        public Sescry()
        {
            this.privateKey = this.RSACrypto.ExportParameters(true);

            this.publicKey = this.RSACrypto.ExportParameters(false);
        }

        public void Encrypt()
        {
            publicKey.ToString();
        }
    }
}