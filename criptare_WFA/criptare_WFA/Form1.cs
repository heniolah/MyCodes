using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace criptare_WFA
{
    public partial class Form1 : Form
    {
        public string[] elements=null;
        public int count;
        public byte[] key1;
        public byte[] iv1;

        byte[][] encrypted ;
        public Form1()
        {
            InitializeComponent();
        }
        public string[] files;
        public TextReader text;
        public int n;
        private void button1_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "FILE|*.txt" })
            //{
            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        listBox1.Text = (ofd.FileName);
            //    }
            //}
            listBox1.Items.Clear();
            files = Directory.GetFiles(@"C:..\\..\\data");
            count = 0;

            foreach (string f in files)
            {
                text = new StreamReader(@"..\..\probatext.txt");
                string buffer; int i = 0;
                while ((buffer = text.ReadLine()) != null)
                { count++; }
            }
            elements = new string[count];
            foreach (string f in files)
            {
                text = new StreamReader(@"..\..\probatext.txt");
                string buffer; int i = 0;
                while ((buffer = text.ReadLine()) != null)
                {
                    string[] s = buffer.Split(' ');
                    textBox1.Text = f;
                    listBox1.Items.Add(buffer);
                    elements[i] = buffer; i += 1;
                }

            }


        encrypted= new byte[count][];


        }
    


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(" ");
            listBox1.Items.Add("textul criptat:");
            string selectedItem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
           
            if (selectedItem == "aes")
                using (Aes myAes = Aes.Create())
            {
                    encrypted= new byte[listBox1.Items.Count][]; ;
                    myAes.Key =new byte[16];
                    myAes.GenerateKey();
                    myAes.IV = new byte[16];
                    myAes.GenerateIV();
                    key1 = myAes.Key;
                    iv1 = myAes.IV;
                    for (int i = 0; i < count; i++)
                    {
                        // Encrypt the string to an array of bytes.
                      encrypted[i]  = SymmetricAlgorithm.EncryptStringToBytes_Aes(elements[i], myAes.Key, myAes.IV);
                        
                    }

                   
                    for (int i = 0; i < count; i++)
                    {
                        listBox1.Items.Add(Convert.ToBase64String(encrypted[i]));
                    }
                    

                }
            else if (selectedItem == "cbc")
                using (var random = new RNGCryptoServiceProvider())
                {
                    var key = new byte[16];
                    random.GetBytes(key);
                    key1 = key;
                    for (int i = 0; i < count; i++)
                    {
                        // Encrypt the string to an array of bytes. 
                        encrypted[i] = AnotherCipher.EncryptStringToBytes_Aes(elements[i], key);
                    }
                    for (int i = 0; i < count; i++)
                    {
                        listBox1.Items.Add(Convert.ToBase64String(encrypted[i]));
                    }
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(" ");
            listBox1.Items.Add("textul decriptat:");
            string selectedItem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            if (selectedItem == "aes")
                using (Aes myAes = Aes.Create())
                {
                    myAes.Key= key1;
                    myAes.IV = iv1;
                    string[] roundtrip = new string[count];

                    for (int i = 0; i < count; i++)
                    { // Decrypt the bytes to a string.
                        roundtrip[i] = SymmetricAlgorithm.DecryptStringFromBytes_Aes(encrypted[i], myAes.Key, myAes.IV);
                    }
                    for (int i = 0; i < count; i++)
                    {
                        listBox1.Items.Add(roundtrip[i]);
                    }
                }
            else if (selectedItem == "cbc")
                using (var random = new RNGCryptoServiceProvider())
                {
                    // Decrypt the bytes to a string. 
                    string[] roundtrip = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        roundtrip[i] = AnotherCipher.DecryptStringFromBytes_Aes(encrypted[i], key1);
                    }
                        for (int i = 0; i < count; i++)
                    {
                        listBox1.Items.Add(roundtrip[i]);
                    }
                }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void button4_Click(object sender, EventArgs e)
        {
            // listBox1.Items.Add(" ");
            listBox1.Items.Clear();
            listBox1.Items.Add("textul criptat cu padding:");
            string selectedItem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            if (selectedItem == "aes")
                using (Aes myAes = Aes.Create())
                {
                    myAes.Padding = PaddingMode.ISO10126;   
                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = SymmetricAlgorithm.EncryptStringToBytes_Aes(listBox1.Items.ToString(), myAes.Key, myAes.IV);

                    // Decrypt the bytes to a string.
                    string roundtrip = SymmetricAlgorithm.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

                    //Display the original data and the decrypted data.
                    // listBox1.Items.Clear();
                    for (int i = 0; i < 5; i++)
                    {
                        listBox1.Items.Add(encrypted);
                    }
                }
            else if (selectedItem == "cbc")
                using (var random = new RNGCryptoServiceProvider())
                {
                    var key = new byte[16];
                    random.GetBytes(key);
                    // Encrypt the string to an array of bytes. 
                    byte[] encrypted = AnotherCipher.EncryptStringToBytes_Aes(listBox1.Items.ToString(), key);
                    // Decrypt the bytes to a string. 
                    string roundtrip = AnotherCipher.DecryptStringFromBytes_Aes(encrypted, key);

                    //Display the original data and the decrypted data.
                    listBox1.Items.Add(Convert.ToBase64String(encrypted));
                }
        }
    }
}
