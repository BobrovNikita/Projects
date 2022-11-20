using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Total_Comander
{
    public partial class Form1 : Form
    {
        private string? path_dtg1;
        private string? path_dtg2;

        private string? path_current_file_dtg1;
        private string? path_current_file_dtg2;


        public Form1()
        {
            InitializeComponent();
            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Back");
            t.SetToolTip(button2, "Back");
            t.SetToolTip(button3, "Copy");
            t.SetToolTip(button4, "Replace");
            t.SetToolTip(button6, "Copy");

            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            переименоватьToolStripMenuItem.Enabled = false;
            переименоватьToolStripMenuItem1.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                comboBox1.Items.Add(drive.Name);
                comboBox2.Items.Add(drive.Name);
            }
            DataGridView1_Properties();
            DataGridView2_Properties();
        }

        #region Combobox Event Index Changed
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? path = comboBox1.SelectedItem.ToString();
            if (path != null)
            {
                ViewDirectories(path, dataGridView1);
                button3.Enabled = false;
                button4.Enabled = false;
                переименоватьToolStripMenuItem.Enabled = false;
            }

            path_dtg1 = comboBox1.SelectedItem.ToString();
            dataGridView1.ContextMenuStrip = contextMenuStrip1;

            
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? path = comboBox2.SelectedItem.ToString();
            if (path != null)
            {
                ViewDirectories(path, dataGridView2);
                button5.Enabled = false;
                button6.Enabled = false;
                переименоватьToolStripMenuItem1.Enabled = false;
            }

            path_dtg2 = comboBox2.SelectedItem.ToString();
            dataGridView2.ContextMenuStrip = contextMenuStrip2;
            
        }
        #endregion

        #region DataGrid Events
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoubleClickCells(e, dataGridView1);
                button3.Enabled = false;
                button4.Enabled = false;
                переименоватьToolStripMenuItem.Enabled = false;
            }
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoubleClickCells(e, dataGridView2);
                button5.Enabled = false;
                button6.Enabled = false;
                переименоватьToolStripMenuItem1.Enabled = false;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ClickCells(dataGridView1, e, path_dtg1, ref path_current_file_dtg1);
                button3.Enabled = true;
                button4.Enabled = true;
                переименоватьToolStripMenuItem.Enabled = true;
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ClickCells(dataGridView2, e, path_dtg2, ref path_current_file_dtg2);
                button5.Enabled = true;
                button6.Enabled = true;
                переименоватьToolStripMenuItem1.Enabled = true;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteFiles(e, dataGridView1, ref path_current_file_dtg1);
                button3.Enabled = false;
                button4.Enabled = false;
                переименоватьToolStripMenuItem.Enabled = false;
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteFiles(e, dataGridView2, ref path_current_file_dtg2);
                button5.Enabled = false;
                button6.Enabled = false;
                переименоватьToolStripMenuItem1.Enabled = false;
            }
        }

        #endregion
        #region Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            ButtonsBackClick(comboBox1, ref path_dtg1, dataGridView1);
            button3.Enabled = false;
            button4.Enabled = false;
            переименоватьToolStripMenuItem.Enabled = false;
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ButtonsBackClick(comboBox2, ref path_dtg2, dataGridView2);
            button5.Enabled = false;
            button6.Enabled = false;
            переименоватьToolStripMenuItem1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (path_current_file_dtg1 != "" && path_dtg2 != "" && path_current_file_dtg1 != null)
            {
                if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "<dir>")
                {
                    int lastIndex = path_current_file_dtg1.LastIndexOf('\\');
                    string? dirname = path_current_file_dtg1.Substring(lastIndex, path_current_file_dtg1.Length - lastIndex);

                    CopyDirectory(path_current_file_dtg1, path_dtg2 + "\\" + dirname, true);
                    ViewDirectories(path_dtg2, dataGridView2);
                }
                else
                {
                    CopyFile(path_current_file_dtg1, path_dtg2);
                    ViewDirectories(path_dtg2, dataGridView2);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (path_current_file_dtg1 != "" && path_dtg2 != "" && path_current_file_dtg1 != null)
            {
                if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "<dir>")
                {

                    MoveToDirectory(path_current_file_dtg1, path_dtg2);
                    ViewDirectories(path_dtg1, dataGridView1);
                    ViewDirectories(path_dtg2, dataGridView2);
                }
                else
                {
                    MoveToFile(path_current_file_dtg1, path_dtg2);
                    ViewDirectories(path_dtg1, dataGridView1);
                    ViewDirectories(path_dtg2, dataGridView2);
                    
                }
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                переименоватьToolStripMenuItem.Enabled = false;
                переименоватьToolStripMenuItem1.Enabled = false;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (path_current_file_dtg2 != "" && path_dtg1 != "")
            {
                if (dataGridView2.CurrentRow.Cells[3].Value.ToString() == "<dir>")
                {

                    MoveToDirectory(path_current_file_dtg2, path_dtg1);
                    ViewDirectories(path_dtg1, dataGridView1);
                    ViewDirectories(path_dtg2, dataGridView2);
                }
                else
                {
                    MoveToFile(path_current_file_dtg2, path_dtg1);
                    ViewDirectories(path_dtg1, dataGridView1);
                    ViewDirectories(path_dtg2, dataGridView2);
                }
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                переименоватьToolStripMenuItem.Enabled = false;
                переименоватьToolStripMenuItem1.Enabled = false;
            }
            
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (path_current_file_dtg2 != "" && path_dtg1 != "")
            {
                if (dataGridView2.CurrentRow.Cells[3].Value.ToString() == "<dir>")
                {
                    int lastIndex = path_current_file_dtg2.LastIndexOf('\\');
                    string? dirname = path_current_file_dtg2.Substring(lastIndex, path_current_file_dtg2.Length - lastIndex);

                    CopyDirectory(path_current_file_dtg2, path_dtg1 + "\\" + dirname, true);
                    ViewDirectories(path_dtg1, dataGridView1);
                }
                else
                {
                    CopyFile(path_current_file_dtg2, path_dtg1);
                    ViewDirectories(path_dtg1, dataGridView1);
                }
            }
        }
        #endregion

        #region ContextMenu
        //DataGridView 1
        private void папкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(path_dtg1, dataGridView1, 0);
            form.ShowDialog();
        }
        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(path_dtg1, dataGridView1, 1);
            form.ShowDialog();
        }
        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path_current_file_dtg1 != "" && path_dtg1 != "")
            {
                if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "<dir>")
                {
                    Form3 form = new Form3(path_current_file_dtg1, path_dtg1, 0);
                    form.ShowDialog();
                }
                else
                {
                    Form3 form = new Form3(path_current_file_dtg1, path_dtg1, 1);
                    form.ShowDialog();
                }

                ViewDirectories(path_dtg1, dataGridView1);
            }
        }
        //DataGridView 2
        private void папкуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(path_dtg2, dataGridView2, 0);
            form.ShowDialog();
        }
        private void файлToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(path_dtg2, dataGridView2, 1);
            form.ShowDialog();
        }
        private void переименоватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (path_current_file_dtg2 != "" && path_dtg2 != "")
            {
                if (dataGridView2.CurrentRow.Cells[3].Value.ToString() == "<dir>")
                {
                    Form3 form = new Form3(path_current_file_dtg2, path_dtg2, 0);
                    form.ShowDialog();
                }
                else
                {
                    Form3 form = new Form3(path_current_file_dtg2, path_dtg2, 1);
                    form.ShowDialog();
                }
                ViewDirectories(path_dtg2, dataGridView2);
            }
        }
        #endregion
        #region Methods
        public void ViewDirectories(string? path, DataGridView dtg)
        {
            dtg.Rows.Clear();
            if (path != null)
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    DirectoryInfo[] directories = dir.GetDirectories();

                    for (int i = 0; i < directories.Length; i++)
                    {
                        dtg.Rows.Add();

                        int count = dtg.Rows.Count - 1;

                        dtg.Rows[count].Cells[0].Value = new Bitmap(Total_Comander.Resource1.folder, new Size(20, 20));
                        dtg.Rows[count].Cells[1].Value = directories[i].Name;
                        dtg.Rows[count].Cells[2].Value = directories[i].Extension;
                        dtg.Rows[count].Cells[3].Value = "<dir>";
                        dtg.Rows[count].Cells[4].Value = directories[i].CreationTime.ToString("dd-MMM-yy");
                    }
                    FileInfo[] files = dir.GetFiles();

                    for (int i = 0; i < files.Length; i++)
                    {
                        dtg.Rows.Add();

                        int count = dtg.Rows.Count - 1;

                        dtg.Rows[count].Cells[0].Value = new Bitmap(Total_Comander.Resource1.files, new Size(20, 20));
                        dtg.Rows[count].Cells[1].Value = files[i].Name;
                        dtg.Rows[count].Cells[2].Value = files[i].Extension;

                        //Можно это условие как то адекватно обернуть? Ибо оно как то тяжко читабельно))

                        if (files[i].Length < 1000000.0)
                        {
                            dtg.Rows[count].Cells[3].Value = Math.Round(files[i].Length / 1000.0, 2) + "Kb";
                        }
                        else if (
                            files[i].Length > 1000000.0 &&
                            files[i].Length / 1000000.0 / 1000.0 < 1.0)
                        {
                            dtg.Rows[count].Cells[3].Value = Math.Round(files[i].Length / 1000000.0, 2) + "Mb";
                        }
                        else
                        {
                            dtg.Rows[count].Cells[3].Value = Math.Round(files[i].Length / 1000000.0 / 1000, 2) + "Gb";
                        }

                        dtg.Rows[count].Cells[4].Value = files[i].CreationTime.ToString("dd-MMM-yy");
                    }

                }
                catch (Exception)
                {
                    if (dtg.Name == dataGridView1.Name)
                    {
                        path_dtg1 = path.Substring(0, path.LastIndexOf('\\'));
                        ViewDirectories(path_dtg1, dtg);
                    }
                    else
                    {
                        path_dtg2 = path.Substring(0, path.LastIndexOf('\\'));
                        ViewDirectories(path_dtg2, dtg);
                    }

                    MessageBox.Show("Не лазь в системные папки");
                }
            }
        }

        private void DoubleClickCells(DataGridViewCellMouseEventArgs e, DataGridView dtg)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dtg.SelectedRows[0].Cells[3].Value.ToString() == "<dir>")
                {
                    if (dtg.Name == dataGridView1.Name)
                    {
                        path_dtg1 += "\\" + dtg.SelectedRows[0].Cells[1].Value.ToString();
                        ViewDirectories(path_dtg1, dtg);
                    }
                    else
                    {
                        path_dtg2 += "\\" + dtg.SelectedRows[0].Cells[1].Value.ToString();
                        ViewDirectories(path_dtg2, dtg);
                    }
                    
                }

            }
        }
        private void ButtonsBackClick(ComboBox box, ref string? path, DataGridView dtg)
        {
            if (path != null)
            {
                if (path != box.SelectedItem.ToString())
                {
                    path = path?.Substring(0, path.LastIndexOf('\\'));
                    ViewDirectories(path, dtg);
                }
            }
        }
        private void ClickCells(DataGridView dtg, DataGridViewCellMouseEventArgs e, string? path_from_dtg, ref string? path_current_dtg)
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

        private void DeleteFiles(KeyEventArgs e, DataGridView dtg, ref string? fullPath)
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
        private void CopyDirectory(string? sourceDir, string? destinationDir, bool recursive)
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
        private void DataGridView1_Properties()
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Name";
            dataGridView1.Columns[1].HeaderText = "Extension";
            dataGridView1.Columns[2].HeaderText = "Size";
            dataGridView1.Columns[3].HeaderText = "Create date";

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;

            var imageColumn = new DataGridViewImageColumn();
            dataGridView1.Columns.Insert(0, imageColumn);
            dataGridView1.Columns[0].Resizable = DataGridViewTriState.False;
        }
        private void DataGridView2_Properties()
        {
            dataGridView2.ColumnCount = 4;
            dataGridView2.Columns[0].HeaderText = "Name";
            dataGridView2.Columns[1].HeaderText = "Extension";
            dataGridView2.Columns[2].HeaderText = "Size";
            dataGridView2.Columns[3].HeaderText = "Create date";

            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;

            var imageColumn1 = new DataGridViewImageColumn();
            dataGridView2.Columns.Insert(0, imageColumn1);
            dataGridView2.Columns[0].Resizable = DataGridViewTriState.False;
        }
        private void CopyFile(string? sourceFile, string destinationDir)
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
        private void MoveToDirectory(string? source, string? newPath)
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
        private void MoveToFile(string? source, string? newPath)
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
        #endregion
    }
}
