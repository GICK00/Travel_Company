using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Travel_Company
{
    public partial class FormDeleted : Form
    {
        public FormDeleted()
        {
            InitializeComponent();
        }

        public void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                FormMain.connection.Open();
                string sql = "DELETE FROM " + Program.formMain.comboBox.Text + " WHERE " + Program.formMain.comboBox.Text.ToUpper() + "_ID = @ID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                    sqlCommand.Parameters["@ID"].Value = Convert.ToInt32(this.textBox1.Text);

                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Данные удалены из таблицы " + Program.formMain.comboBox.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
                this.Close();
            }
        }
    }
}
