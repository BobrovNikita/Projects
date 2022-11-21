using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Total_Comander.WorkWithDirAndFiles
{
    internal class Delete
    {
        public static void DeleteFrom(KeyEventArgs e, DataGridView dtg, ref string? fullPath)
        {
            e.Handled = true;
            if (fullPath != "" && fullPath != null)
            {
                try
                {
                    if (MessageBox.Show($"Вы действительно хотите удалить файл {fullPath}?",
                        "Удаление записи",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        if (dtg.CurrentRow.Cells[3].Value.ToString() == "<dir>")
                        {
                            DirectoryInfo dir = new DirectoryInfo(fullPath);
                            if (dir.Exists)
                            {
                                dir.Delete(true);
                                fullPath = "";
                            }
                        }
                        else
                        {
                            FileInfo file = new FileInfo(fullPath);
                            if (file.Exists)
                            {
                                file.Delete();
                                fullPath = "";
                            }
                        }
                        e.Handled = false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("А темболее не нужно удалять их :)");
                }
            }

        }
    }
}
