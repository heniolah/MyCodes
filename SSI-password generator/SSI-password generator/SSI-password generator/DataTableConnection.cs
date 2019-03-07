using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSI_password_generator
{
    public class DataTableConnection
    {
        public Form1 mainWindow;
        public DataTable RefreshDataTable()
        {
            var select = "SELECT * FROM [dbo].[Table]";
            var c = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PassGen.mdf;Integrated Security=True;Connect Timeout=30");
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            mainWindow.passwordRecordGridView.ReadOnly = true;
            mainWindow.passwordRecordGridView.DataSource = ds.Tables[0];

            return ds.Tables[0];
        }
    }
}
