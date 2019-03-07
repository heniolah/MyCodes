using SSI_password_generator.PassGenDataSetTableAdapters;
using System;
using System.Windows.Forms;

namespace SSI_password_generator
{
    public partial class Form1 : Form
    {
        public DataGridView passwordRecordGridView { get { return PasswordRecordGridView; } }
        public ContextMenuStrip rightClickMenu { get { return contextMenuStrip1; } }
        public TextBox uRLTextBox { get { return URLTextBox; } }
        public TextBox usernameTextBox { get { return UsernameTextBox; } }
        public TextBox passwordTextBox { get { return PasswordTextBox; } }
        DataTableConnection dataTableConnection = new DataTableConnection();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tableTableAdapter.Fill(passGenDataSet.Table);
            if (checkBox1.Checked) Generator.elements[0] = 1; else Generator.elements[0] = 0;
            if (checkBox2.Checked) Generator.elements[1] = 1; else Generator.elements[1] = 0;
            if (checkBox3.Checked) Generator.elements[2] = 1; else Generator.elements[2] = 0;
            if (checkBox4.Checked) Generator.elements[3] = 1; else Generator.elements[3] = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) Generator.elements[0] = 1;
            else Generator.elements[0] = 0;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) Generator.elements[1] = 1;
            else Generator.elements[1] = 0;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) Generator.elements[2] = 1;
            else Generator.elements[2] = 0;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) Generator.elements[3] = 1;
            else Generator.elements[3] = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PasswordTextBox.Text = Generator.GeneratePassword(int.Parse(textBox1.Text)).ToString();
        }

        private void SaveEntry_Click(object sender, EventArgs e)
        {
            TableTableAdapter regionTableAdapter = new TableTableAdapter();
            DataTableConnection dataTableConnection = new DataTableConnection();
            dataTableConnection.mainWindow = this;

            var dataTable = dataTableConnection.RefreshDataTable();
            if (uRLTextBox.Text.Trim() != "" && usernameTextBox.Text.Trim() != "" && passwordTextBox.Text.Trim() != "")
            {
                if (dataTable.Rows.Count == 0)
                    regionTableAdapter.Insert(1, URLTextBox.Text, UsernameTextBox.Text, PasswordTextBox.Text);
                else
                    regionTableAdapter.Insert(Convert.ToInt32(dataTable.Rows[dataTable.Rows.Count - 1].ItemArray[0]) + 1, URLTextBox.Text, UsernameTextBox.Text, PasswordTextBox.Text);
            }
            dataTableConnection.RefreshDataTable();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataTableConnection dataTableConnection = new DataTableConnection();
            dataTableConnection.mainWindow = this;
            RightClickMenuLogic copyLogic = new RightClickMenuLogic();
            copyLogic.mainWindow = this;
            copyLogic.DataGridViewRightClickActions(sender, e, dataTableConnection);
        }

        private void PasswordRecordGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            RightClickMenuLogic copyLogic = new RightClickMenuLogic();
            copyLogic.mainWindow = this;
            copyLogic.DataGridViewClick(sender, e);
        }


        private void PasswordRecordGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (PasswordRecordGridView.Columns[e.ColumnIndex].Name == "passwordDataGridViewTextBoxColumn" && e.Value != null)
            {
                PasswordRecordGridView.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('*', e.Value.ToString().Length);
            }
        }

        private void ShowPassword_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.PasswordChar == '*')
                PasswordTextBox.PasswordChar = '\0';
            else PasswordTextBox.PasswordChar = '*';
        }
    }
}