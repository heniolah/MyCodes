using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Generators;

namespace DigitalSignitures
{
    class Program
    {
        static void Main(string[] args)
        {
            string SourceData;
            byte[] tmpSource;
            byte[] tmpHash;

            Console.WriteLine("Enter any text:");
            SourceData = Console.ReadLine();

            tmpSource = ASCIIEncoding.ASCII.GetBytes(SourceData);
            Console.WriteLine();
            Console.WriteLine();

            //folosesc algoritmul asimetric randomizat
            RsaKeyPairGenerator rsaKeyPairGen = new RsaKeyPairGenerator();
            rsaKeyPairGen.Init(new KeyGenerationParameters(new SecureRandom(), 2042));
            AsymmetricCipherKeyPair keyPair = rsaKeyPairGen.GenerateKeyPair();

            //extragerea cheii privata
            RsaKeyParameters PrivateKey = (RsaKeyParameters)keyPair.Private;
            RsaKeyParameters PublicKey = (RsaKeyParameters)keyPair.Public;

            TextWriter txtWriter1 = new StringWriter();
            PemWriter pemWriter1 = new PemWriter(txtWriter1);
            pemWriter1.WriteObject(PublicKey);
            pemWriter1.Writer.Flush();
            string print_publicKey = txtWriter1.ToString();
            Console.Write("the public key is: " + print_publicKey);
            Console.WriteLine();

            //generarea semnaturii digitale
            ISigner sign = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha1WithRsaEncryption.Id);
            sign.Init(true, PrivateKey);
            sign.BlockUpdate(tmpSource, 0, tmpSource.Length);
            byte[] signature = sign.GenerateSignature();
            Console.WriteLine();
            Console.Write("the digital signiture is: " + ByteArrayToString(signature));

            //verificare
            ISigner sign1 = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha1WithRsaEncryption.Id);
            sign1.Init(false, PublicKey);
            sign1.BlockUpdate(tmpSource, 0, tmpSource.Length);
            bool status = sign1.VerifySignature(signature);
            Console.WriteLine();
            if(status)
                Console.WriteLine("signature is valid");
            else Console.WriteLine("signature is not valid");
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (int i = 0; i <arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("x").ToLower());
            }
            return sOutput.ToString();
        }
    }
}
