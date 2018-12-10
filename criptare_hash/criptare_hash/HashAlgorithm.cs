using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace criptare_hash
{
    public class SimpleHash
    {
        
        public static byte[] Hash(HashAlgorithm algorithm, byte[] input)
        {
            return algorithm.ComputeHash(input);
        }

        public static string HexHash(HashAlgorithm algorithm, string text)
        {
            byte[] input = UnicodeEncoding.Unicode.GetBytes(text);
            return string.Concat(Hash(algorithm, input).Select(b => b.ToString("x2")));
        }

        public static string Base64Hash(HashAlgorithm algorithm, string text)
        {
            byte[] input = UnicodeEncoding.Unicode.GetBytes(text);
            return Convert.ToBase64String(Hash(algorithm, input));
        }
    }

     //algoritmul de mai jos este doar o schita de incercare de implementare a algoritmului hash

    //public abstract class HashAlgorithm : SHA1CryptoServiceProvider
    //{
    //    
    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
