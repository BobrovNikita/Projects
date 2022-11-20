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
    public partial class Form2 : Form
    {
        private string? path;
        private DataGridView? dtg;
        private int number;

        public Form2(string? path, DataGridView dtg, int number)
        {
            InitializeComponent();

            this.path = path;
            this.dtg = dtg;
            this.number = number;

            comboBox1.Visible = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (number == 1)
            {
                comboBox1.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            if (number == 0)
            {
                if (textBox1.Text != null && textBox1.Text != "")
                {
                    CreateDirectory();
                }
                else
                {
                    MessageBox.Show("Enter folder's name");
                }
            }
            else
            {
                if (textBox1.Text != null && textBox1.Text != "" && comboBox1.SelectedIndex > -1)
                {
                    CreateFile();
                }
            }
        }

        private void CreateDirectory()
        {
            string? path = this.path + $"\\{textBox1.Text}";
            if (path != null && path != "")
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                    MessageBox.Show("Folder has been created");
                    Form1 form = new Form1();


                    if (dtg != null)
                    {
                        form.ViewDirectories(this.path, dtg);
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Папка с таким именем уже существует");
                }
            }
        }
        private void CreateFile()
        {
            string? path = this.path + $"\\{textBox1.Text}{comboBox1.SelectedItem.ToString()}";
            if (path != null && path != "")
            {
                FileInfo file = new FileInfo(path);
                if (!file.Exists)
                {
                    using (file.Create())
                    MessageBox.Show("File has been created");
                    Form1 form = new Form1();

                    if (dtg != null)
                    {
                        form.ViewDirectories(this.path, dtg);
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Файл с таким именем уже существует");
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\\' || e.KeyChar == '.' || e.KeyChar == '/')
            {
                e.Handled = true;
            }
        }
    }
}
