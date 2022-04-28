using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Travel_Company
{
    public partial class FormRequest : Form
    {
        public FormRequest()
        {
            InitializeComponent();
        }

        private void toolStripButtonOpenSQL_Click(object sender, EventArgs e)
        {
            if (Program.formMain.openFileDialogSQL.ShowDialog() == DialogResult.Cancel) return;
            try
            {
                string filename = Program.formMain.openFileDialogSQL.FileName;
                string sql = System.IO.File.ReadAllText(filename, Encoding.GetEncoding(1251));
                textBoxSQLReader.Text = sql;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = $"Запрос не выполнен! Ошибка {ex.Message}";
                Program.formMain.toolStripStatusLabel2.Text = $"Запрос не выполнен! Ошибка {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
            }
        }        

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(textBoxSQLReader.Text, FormMain.connection))
                {
                    FormMain.connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                toolStripStatusLabel2.Text = "Запрос выполнен";
                Program.formMain.toolStripStatusLabel2.Text = "Запрос выполнен";
            } 
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            } 
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e) => this.Close();
    }
}
