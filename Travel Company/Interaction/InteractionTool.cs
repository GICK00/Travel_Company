﻿using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Travel_Company.Interaction
{
    class InteractionTool
    {
        public void выполнитьЗапросToolStripMenuItem()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            if (Program.formMain.openFileDialogSQL.ShowDialog() == DialogResult.Cancel) return;
            try
            {
                string filename = Program.formMain.openFileDialogSQL.FileName;
                string sql = System.IO.File.ReadAllText(filename, Encoding.GetEncoding(1251));

                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    FormMain.connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Запрос выполнен";
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Запрос не выполнен! Ошибка {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        public void очиститьБазуДанныхToolStripMenuItem()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            DialogResult result = MessageBox.Show("Вы уверены, что хотите очистить базу данных?", "Удаление данных.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            using (SqlCommand sqlCommand = new SqlCommand("DeletedAll", FormMain.connection))
            {
                FormMain.connection.Open();
                sqlCommand.ExecuteNonQuery();
                FormMain.connection.Close();
            }
            Program.formMain.Reload(Program.formMain.comboBox.Text);
            Program.formMain.toolStripStatusLabel2.Text = "База данных очищенна";
        }

        public void создатьРезервнуюКопиюToolStripMenuItem()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            if (Program.formMain.saveFileDialogBack.ShowDialog() == DialogResult.Cancel) return;
            string path = Program.formMain.saveFileDialogBack.FileName;
            string sql = @"BACKUP DATABASE[Travel_Company] TO DISK = N'" + path + "' WITH NOFORMAT, NOINIT, NAME = N'Travel_Company-Полная База данных Резервное копирование', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            string res = "back";
            FormLoad formLoad = new FormLoad(sql, res);
            formLoad.ShowDialog();
        }

        public void восстановитьБазуДанныхToolStripMenuItem()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            DialogResult result = MessageBox.Show("Вы уверены, что хотите востановить базу данных?", "Восстановление базы данных.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            if (Program.formMain.openFileDialogRes.ShowDialog() == DialogResult.Cancel) return;
            string path = Program.formMain.openFileDialogRes.FileName;
            string sql = @"USE Master RESTORE DATABASE [Travel_Company] FROM  DISK = N'" + path + "' WITH REPLACE, FILE = 1,  NOUNLOAD,  STATS = 5";
            string res = "res";
            FormLoad formLoad = new FormLoad(sql, res);
            formLoad.ShowDialog();
        }
    }
}
