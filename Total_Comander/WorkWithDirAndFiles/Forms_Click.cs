using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Total_Comander.WorkWithDirAndFiles
{
    internal class Forms_Click
    {
        public static void ClickCells(DataGridView dtg, DataGridViewCellMouseEventArgs e, string? path_from_dtg, ref string? path_current_dtg)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                string? path = path_from_dtg;
                if (path != null)
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    if (dtg.CurrentRow.Cells[3].Value.ToString() == "<dir>")
                    {
                        DirectoryInfo[] directories = dir.GetDirectories();

                        for (int i = 0; i < directories.Length; i++)
                        {
                            if (directories[i].Name == dtg.CurrentRow.Cells[1].Value.ToString())
                            {
                                path_current_dtg = directories[i].FullName;
                            }
                        }
                    }
                    else
                    {
                        FileInfo[] files = dir.GetFiles();

                        for (int i = 0; i < files.Length; i++)
                        {
                            if (files[i].Name == dtg.CurrentRow.Cells[1].Value.ToString())
                            {
                                path_current_dtg = files[i].FullName;
                            }
                        }
                    }
                }
            }
        }
    }
}
