using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Total_Comander.WorkWithDirAndFiles
{
    internal class MoveTo
    {
        public static void MoveToDirectory(string? source, string? newPath)
        {
            try
            {
                if (source != null && source != "")
                {
                    DirectoryInfo dir = new DirectoryInfo(source);
                    string fullPath = Path.Combine(newPath, dir.Name);
                    if (dir.Exists && !Directory.Exists(fullPath))
                    {
                        dir.MoveTo(fullPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        public static void MoveToFile(string? source, string? newPath)
        {
            try
            {
                if (source != null && source != "")
                {
                    FileInfo file = new FileInfo(source);

                    string fullPath = Path.Combine(newPath, file.Name);
                    if (file.Exists)
                    {
                        file.MoveTo(fullPath, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
