using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace criptare_RSA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public TextReader text;
        public string[] elements = null;
        public byte[] key;
        public int count;
        public byte[][] encryptedData;
        public byte[][] decryptedData;
        private void button1_Click(object sender, EventArgs e)
        {
            text = new StreamReader(@"..\..\mesaj.txt");
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "FILE|*.txt" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.FileName == text.ToString())
                        textBox1.Text = (ofd.FileName);

                }
               // else MessageBox.Show("You didn't open the right file");
            }

            string buffer; count = 0;
            elements = new string[9];
            while ((buffer = text.ReadLine()) != null)
            {
                string[] s = buffer.Split(' ');
                listBox1.Items.Add(buffer);
                elements[count] = buffer;
                count++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Create a UnicodeEncoder to convert between byte array and string.
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            encryptedData=new byte[listBox1.Items.Count][];
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                //Pass the data to ENCRYPT, the public key information 
                //(using RSACryptoServiceProvider.ExportParameters(false),
                //and a boolean flag specifying no OAEP padding.
                for (int i = 0; i < count; i++)
                    encryptedData[i] = RSAEncrypt(ByteConverter.GetBytes(elements[i]), RSA.ExportParameters(false), false);

                listBox1.Items.Add(" ");
                listBox1.Items.Add("Textul criptat:");
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add(Convert.ToBase64String(encryptedData[i]));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            
            decryptedData = new byte[listBox1.Items.Count][];

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                for (int i = 0; i < count; i++)
                    encryptedData[i] = RSAEncrypt(ByteConverter.GetBytes(elements[i]), RSA.ExportParameters(false), false);

                //Pass the data to DECRYPT, the private key information 
                //(using RSACryptoServiceProvider.ExportParameters(true),
                //and a boolean flag specifying no OAEP padding.
                for (int i = 0; i < count; i++)
                    decryptedData[i] = RSADecrypt(encryptedData[i], RSA.ExportParameters(true), false);

                listBox1.Items.Add(" ");
                listBox1.Items.Add("Textul decriptat:");
                for (int i = 0; i < count; i++)
                  listBox1.Items.Add(ByteConverter.GetString(decryptedData[i])).ToString();
            }
        }
        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
           
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return (encryptedData);
        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            MyRSA.prim1 = MyRSA.generator.GetSequence().ElementAt(rnd.Next(1000, 10000));
            MyRSA.prim2 = MyRSA.generator.GetSequence().ElementAt(rnd.Next(1000, 10000));
            int temp_fi = 0;

            temp_fi = Convert.ToInt32(MyRSA.fi / 2);  //se poate afla eroare pe linia acesta dar numai daca prim1 sau prim2 iese prea mare la generare rnd
            int rndint = rnd.Next(1, temp_fi);
            long element = rndint * 2;


            MyRSA mr = new MyRSA();
            mr.GCDRecursive(element, MyRSA.fi);

            EuclidExtended ee = new EuclidExtended(1, MyRSA.fi);
            EuclidExtendedSolution result = ee.calculate();
            long d = Convert.ToInt64(result.D);
            
            MessageBox.Show("the public key: "+ mr.generatePublicKey(MyRSA.n, element).ToString());
            MessageBox.Show("the private key: " + mr.generatePrivateKey(d).ToString());

            listBox1.Items.Add(" ");
            listBox1.Items.Add("Textul criptat:");
            for (int i = 0; i < count; i++)
            {
                mr.Encrypt(elements[i], d);
                long c1 = long.Parse(mr.Encrypt(elements[i], d));
            }

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            MyRSA.prim1 = MyRSA.generator.GetSequence().ElementAt(rnd.Next(1000, 10000));
            MyRSA.prim2 = MyRSA.generator.GetSequence().ElementAt(rnd.Next(1000, 10000));
            int temp_fi = 0;

            temp_fi = Convert.ToInt32(MyRSA.fi / 2);
            int rndint = rnd.Next(1, temp_fi);
            long element = rndint * 2;


            MyRSA mr = new MyRSA();
            mr.GCDRecursive(element, MyRSA.fi);

            EuclidExtended ee = new EuclidExtended(1, MyRSA.fi);
            EuclidExtendedSolution result = ee.calculate();
            long d = Convert.ToInt64(result.D);
            listBox1.Items.Add(" ");
            listBox1.Items.Add("Textul decriptat:");
            for (int i = 0; i < count; i++)
            {
                long c1 = long.Parse(mr.Encrypt(elements[i], d));
                mr.Decrypt(elements[i], c1, d);
            }
        }
    }
}
