using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Total_Comander.WorkWithDirAndFiles
{
    internal class Copy
    {
        public static void CopyDirectory(string? sourceDir, string? destinationDir, bool recursive)
        {
            if (sourceDir != null && destinationDir != null)
            {
                try
                {
                    var dir = new DirectoryInfo(sourceDir);
                    if (!dir.Exists)
                        throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

                    DirectoryInfo[] dirs = dir.GetDirectories();

                    Directory.CreateDirectory(destinationDir);

                    foreach (FileInfo file in dir.GetFiles())
                    {
                        string targetFilePath = Path.Combine(destinationDir, file.Name);
                        file.CopyTo(targetFilePath, true);
                    }

                    if (recursive)
                    {
                        foreach (DirectoryInfo subDir in dirs)
                        {
                            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                            CopyDirectory(subDir.FullName, newDestinationDir, true);
                        }
                    }
                }
                catch (DirectoryNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static void CopyFile(string? sourceFile, string destinationDir)
        {
            try
            {
                var file = new FileInfo(sourceFile);

                if (!file.Exists)
                    throw new FileNotFoundException($"Source file not found: {file.FullName}");

                string pathFile = Path.Combine(destinationDir, file.Name);
                file.CopyTo(pathFile, true);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
