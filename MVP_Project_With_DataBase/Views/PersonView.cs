using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVP_Project_With_DataBase.Views
{
    public partial class PersonView : Form, IPersonVIew
    {
        //Fields
        private string message;
        private bool isSuccessful;
        private bool isEdit;
        public PersonView()
        {
            InitializeComponent();
            AssosiateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(TabPagePersonDetail);
            CloseForm.Click += delegate { this.Close(); };
        }

        //Methods

        private void AssosiateAndRaiseViewEvents()
        {
            //Input Control
            txtPersonAge.KeyPress += (s, e) =>
            {
                if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8) return;
                else
                    e.Handled = true;
            };

            //Search
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txbSearchValue.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };

            //Add new
            btnAddNew.Click += delegate 
            { 
                AddNewEvent?.Invoke(this, EventArgs.Empty); 
                tabControl1.TabPages.Add(TabPagePersonDetail);
                tabControl1.TabPages.Remove(TabPagePersonList);
                TabPagePersonList.Text = "Person Add New";
            };

            //Edit
            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(TabPagePersonDetail);
                tabControl1.TabPages.Remove(TabPagePersonList);
                TabPagePersonDetail.Text = "Person Edit";
            };

            //Save changes
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl1.TabPages.Add(TabPagePersonList);
                    tabControl1.TabPages.Remove(TabPagePersonDetail);
                }

                MessageBox.Show(Message);
            };

            //Cancel
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(TabPagePersonList);
                tabControl1.TabPages.Remove(TabPagePersonDetail);
            };

            //Delete
            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected person", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        //Properties
        public string PersonId 
        { 
            get => txtPersonID.Text; 
            set => txtPersonID.Text = value; 
        }
        public string PersonName 
        { 
            get => txtPersonName.Text; 
            set => txtPersonName.Text = value; 
        }
        public int PersonAge 
        { 
            get => Convert.ToInt32(txtPersonAge.Text); 
            set => txtPersonAge.Text = value.ToString(); 
        }
        public string SearchValue 
        { 
            get => txbSearchValue.Text; 
            set => txbSearchValue.Text = value; 
        }
        public bool IsEdit 
        { 
            get => isEdit; 
            set => isEdit = value; 
        }
        public bool IsSuccessful 
        { 
            get => isSuccessful; 
            set => isSuccessful = value; 
        }
        public string Message 
        { 
            get => message;
            set => message = value; 
        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public void SetPersonListBindingSource(BindingSource personList)
        {
            dataGridView.DataSource = personList;
        }


        private static PersonView instance;
        public static PersonView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                if (parentContainer.ActiveMdiChild != null)
                    parentContainer.ActiveMdiChild.Close();

                instance = new PersonView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;

                instance.BringToFront();
            }

            return instance;
        }
    }
}
