using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace criptare_RSA
{
    public class MyRSA
    {
        public static Random rnd = new Random();

        public static PrimeSequenceGenerator generator = new PrimeSequenceGenerator();
        public static long prim1 = generator.GetSequence().ElementAt(rnd.Next(1000, 10000));
        public static long prim2 = generator.GetSequence().ElementAt(rnd.Next(1000, 10000));
        public static long n = (prim1* prim2);
        public static long fi = ((prim1 - 1) * (prim2 - 1));
        public string Encrypt(string m,long el)
        {
            int temp_fi = 0;  //avem nevoie de un nr mai mic decat fi ca sa putem crea un random nr intre (1,fi)

            //am incercat sa rezolv eroarea OverflowException care apare uneori aici:
            do
            {
                try
                {
                    temp_fi = Convert.ToInt32(fi / 2);
                }
                catch (Exception)
                {
                    fi--;

                }
            } while (temp_fi != Convert.ToInt32(fi / 2));
            int rndint = rnd.Next(1, temp_fi);
            el = rndint * 2;

            for (int i = 0; i < n; i++)
            {
                
                m = rnd.Next(i).ToString();
               
            }
            long m_int = long.Parse(m);
            long c = putere(m_int, el)%n;
           // MessageBox.Show("encrypted: " + c.ToString());
            return c.ToString();
        }

        public string Decrypt(string m, long c, long d)
        {
            long d1= generatePrivateKey(d);
            for (int i = 0; i < n; i++)
            {

                m = rnd.Next(i).ToString();

            }
            long m_int = long.Parse(m);
            m_int = putere(c, d1) % n;
            return c.ToString();
        }
        public long generatePublicKey(long n, long el)
        {
            rnd = new Random();
            int temp_fi = 0;  //avem nevoie de un nr mai mic decat fi ca sa putem crea un random nr intre (1,fi)


            //am incercat sa rezolv eroarea OverflowException care apare uneori aici:
            do
            {
                try
                {
                    temp_fi = Convert.ToInt32(fi / 2);
                }
                catch (Exception)
                {
                    fi--;

                }
            } while (temp_fi != Convert.ToInt32(fi / 2));
            int rndint = rnd.Next(1, temp_fi);
             el = rndint * 2;

            
            MyRSA hm = new MyRSA();
            hm.GCDRecursive(el, fi);
            return hm.GCDRecursive(el, fi);
           // MessageBox.Show(" public key: " + hm.GCDRecursive(el, fi).ToString() + n.ToString()+el.ToString());
        }
        public long generatePrivateKey(long d)
        {
            EuclidExtended ee = new EuclidExtended(1, fi);
            EuclidExtendedSolution result = ee.calculate();
            d = Convert.ToInt64(result.D);
            return d;
        }
        public long GCDRecursive(long a, long b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            if (a > b)
                return GCDRecursive(a % b, b);
            else
                return GCDRecursive(a, b % a);
        }

        public long putere(long nr, long put)
        {
            if (put == 1)
                return nr;
            else
                if (put % 2 == 0)
                return (putere(nr, put / 2) * putere(nr, put / 2));
            else
                return (putere(nr, (put - 1) / 2) * putere(nr, (put - 1) / 2) * nr);
        }
    }
    public class PrimeSequenceGenerator : ISequenceGenerator
    {
        public IEnumerable<long> GetSequence()
        {
            var primeCache = new List<long>();
            long curentNumber = 2;
            while (true)
            {
                var isPrime = true;
                var currentRoot = Math.Sqrt(curentNumber);
                foreach (var cachedPrime in primeCache)
                {
                    if (curentNumber % cachedPrime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                    if (cachedPrime > currentRoot)
                    {
                        break;
                    }
                }
                if (isPrime)
                {
                    primeCache.Add(curentNumber);
                    yield return curentNumber;
                }
                curentNumber++;
            }
        }

    }
    interface ISequenceGenerator
    {
        IEnumerable<long> GetSequence();
    }


}
