using System;
using System.Windows.Forms;

namespace Travel_Company
{
    public partial class FormLogin : Form
    {
        public static string Login;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (Login != null)
            {
                MessageBox.Show("Вы уже вошли в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (textBox1.Text != "admin" | textBox2.Text != "admin")
            {
                MessageBox.Show("Нет администратора с таким логином и паролем!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                Login = textBox1.Text;
                Program.formMain.toolStripStatusLabel2.Text = "Произведен вход под логином " + textBox1.Text;
                Program.formMain.Text = "Туристическая компания - Администратор";
                this.Close();
            }
        }
    }
}