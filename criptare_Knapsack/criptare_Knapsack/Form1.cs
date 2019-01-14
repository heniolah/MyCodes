using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace criptare_Knapsack
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

        public UnicodeEncoding ByteConverter = new UnicodeEncoding();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1,9,4,10,30,40";
            textBox2.Text = "53";
            textBox3.Text = "120";
        }

        public string getcipher(string publickey, string data)
        {
            string data_result = "";
            string[] vals = publickey.Split(',');
            int[] weights = new int[vals.Length];

            for (int i = 0; i < vals.Length; i++)
             weights[i] = Convert.ToInt32(vals[i]);


            int ptr = 0;
            int bit = 0;
            int total = 0;
            do
            {
                total = 0;
                for (int i = 0; i < vals.Length; i++)
                {
                    if (data[ptr] == '1') bit = 1;
                    else bit = 0;
                    total += weights[i] * bit;
                    ptr++;
                }
                if (data_result == "") data_result += total.ToString();
                else  data_result += "," + total.ToString();
                    
            } while (ptr < data.Length);


            return (data_result);

        }
        public string getknap(string key, string n, string m)
        {
            string[] vals = key.Split(',');
            string k = "";
            foreach (string v in vals)
            {
                int i = (Convert.ToInt32(v) * Convert.ToInt32(n)) % Convert.ToInt32(m);
                if (k == "") k += i.ToString();
                else k += "," + i.ToString();

            }
            return (k);

        }
        //calculates & returns the inverse of a modulo n
        public int modInverse(int a, int n)
        {
            int i = n, v = 0, d = 1;
            while (a > 0)
            {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }
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

           
            listBox1.Items.Add(" ");
            string priv_key = textBox1.Text;
            string n = textBox2.Text;
            string m = textBox3.Text;
            string get_public = getknap(priv_key, n, m);
            string data = "",cipher="", plain="";
            int modinv=0;

            for (int i = 0; i < count; i++)
            {
                 foreach (char ch in elements[i])
                   {
                      data += Convert.ToString((int)ch, 2); 
                   }
            }
            
            cipher = getcipher(get_public, data.ToString());
            modinv = modInverse(Convert.ToInt32(n), Convert.ToInt32(m));
            plain = getknap(cipher, Convert.ToString(modinv), m);

            listBox1.Items.Add("datatext_bytes:" +data);
            listBox1.Items.Add("public key: " + get_public);
            listBox1.Items.Add("cipher: " + cipher);
            listBox1.Items.Add("plain text: " + plain);
            listBox1.Items.Add("inverse: " + modinv.ToString());
            listBox1.Items.Add(" ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(" ");
            string data = "";
            decryptedData = new byte[listBox1.Items.Count][];

            for (int i = 0; i < count; i++)
            {
                foreach (char ch in elements[i])
                {
                    data += Convert.ToString((int)ch, 2);
                }
            }
              for (int i = 0; i < count; i++)
            decryptedData[i] = ByteConverter.GetBytes(data);

            listBox1.Items.Add("Textul decriptat (acelasi cu cel original):");
            listBox1.Items.Add(ByteConverter.GetString(decryptedData[0])).ToString();
        }
    }
}
