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

namespace criptare_hash
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
        private void button1_Click(object sender, EventArgs e)
        {
            text = new StreamReader(@"..\..\mesaj.txt");
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "FILE|*.txt" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.FileName == text.ToString())
                        textBox1.Text = (ofd.FileName);
                    else MessageBox.Show("You didn't open the right file");
                }
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
            key = new byte[16];
            string selectedItem1 = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            string selectedItem2 = comboBox2.Items[comboBox2.SelectedIndex].ToString();
            listBox1.Items.Add(" ");
            if (selectedItem1 == "SHA1" && selectedItem2 == "HexHash")
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add(SimpleHash.HexHash(SHA1.Create(), elements[i].ToString()));
            else if (selectedItem1 == "SHA1" && selectedItem2 == "Hash")
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add(Convert.ToBase64String(SimpleHash.Hash(SHA1.Create(), key)));
            else if (selectedItem1 == "SHA1" && selectedItem2 == "Base64Hash")
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add(SimpleHash.Base64Hash(SHA1.Create(), elements[i].ToString()));
            else if (selectedItem1 == "SHA256" && selectedItem2 == "HexHash")
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add((SimpleHash.HexHash(SHA256.Create(), elements[i].ToString())));
            else if (selectedItem1 == "SHA256" && selectedItem2 == "Hash")
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add(Convert.ToBase64String(SimpleHash.Hash(SHA256.Create(), key)));
            else if (selectedItem1 == "SHA256" && selectedItem2 == "Base64Hash")
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add((SimpleHash.Base64Hash(SHA256.Create(), elements[i].ToString())));
            else if (selectedItem1 == "MD5" && selectedItem2 == "HexHash")
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add((SimpleHash.HexHash(MD5.Create(), elements[i].ToString())));
            else if (selectedItem1 == "MD5" && selectedItem2 == "Hash")
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add(Convert.ToBase64String(SimpleHash.Hash(MD5.Create(), key)));
            else if (selectedItem1 == "MD5" && selectedItem2 == "Base64Hash")
                for (int i = 0; i < count; i++)
                    listBox1.Items.Add((SimpleHash.Base64Hash(MD5.Create(), elements[i].ToString())));
        }
    }
}
