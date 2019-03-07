using SSI_password_generator.PassGenDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSI_password_generator
{
    public class RightClickMenuLogic
    {
        public Form1 mainWindow;
        public int columnToCopy = 0;

        public void DataGridViewRightClickActions(object sender, ToolStripItemClickedEventArgs e, DataTableConnection dt)
        {
            if (e.ClickedItem.Text == "Copy URL" && mainWindow.passwordRecordGridView.CurrentCell.Value != null)
            {
                mainWindow.passwordRecordGridView.CurrentCell = mainWindow.passwordRecordGridView.Rows[mainWindow.passwordRecordGridView.CurrentCell.RowIndex].Cells[1];
                columnToCopy = mainWindow.passwordRecordGridView.CurrentCell.ColumnIndex;
                Clipboard.SetDataObject(mainWindow.passwordRecordGridView.CurrentCell.Value.ToString().Trim(), false);
            }
            else if (e.ClickedItem.Text == "Copy Username" && mainWindow.passwordRecordGridView.CurrentCell.Value != null)
            {
                mainWindow.passwordRecordGridView.CurrentCell = mainWindow.passwordRecordGridView.Rows[mainWindow.passwordRecordGridView.CurrentCell.RowIndex].Cells[2];
                columnToCopy = mainWindow.passwordRecordGridView.CurrentCell.ColumnIndex;
                Clipboard.SetDataObject(mainWindow.passwordRecordGridView.CurrentCell.Value.ToString().Trim(), false);
            }
            else if (e.ClickedItem.Text == "Copy Password" && mainWindow.passwordRecordGridView.CurrentCell.Value != null)
            {
                mainWindow.passwordRecordGridView.CurrentCell = mainWindow.passwordRecordGridView.Rows[mainWindow.passwordRecordGridView.CurrentCell.RowIndex].Cells[3];
                columnToCopy = mainWindow.passwordRecordGridView.CurrentCell.ColumnIndex;
                Clipboard.SetDataObject(mainWindow.passwordRecordGridView.CurrentCell.Value.ToString().Trim(), false);
            }
            else if (e.ClickedItem.Text == "Delete Entry" && mainWindow.passwordRecordGridView.CurrentCell.Value != null)
            {
                TableTableAdapter regionTableAdapter = new TableTableAdapter();
                //DataTableConnection dataTableConnection = new DataTableConnection();
                //var dataTable = dataTableConnection.RefreshDataTable();
                regionTableAdapter.Delete(Convert.ToInt32(mainWindow.passwordRecordGridView.CurrentRow.Cells[0].Value),
                    mainWindow.passwordRecordGridView.CurrentRow.Cells[1].Value.ToString(),
                    mainWindow.passwordRecordGridView.CurrentRow.Cells[2].Value.ToString(),
                    mainWindow.passwordRecordGridView.CurrentRow.Cells[3].Value.ToString());
                dt.RefreshDataTable();

            }
        }

        public void DataGridViewClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                mainWindow.passwordRecordGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = mainWindow.rightClickMenu;
                mainWindow.passwordRecordGridView.CurrentCell = mainWindow.passwordRecordGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }
    }
}
