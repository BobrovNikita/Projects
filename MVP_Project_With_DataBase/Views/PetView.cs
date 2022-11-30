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
    public partial class PetView : Form, IPetView
    {
        //Fields
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        //Constructor
        public PetView()
        {
            InitializeComponent();
            AssosiateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(TabControlPetDetail);
            CloseForm.Click += delegate { this.Close(); };
            
        }

        private void AssosiateAndRaiseViewEvents()
        {
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
            // Add new
            btnAddNew.Click += delegate 
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(TabControlPetDetail);
                tabControl1.TabPages.Remove(TabControlPetList);
                TabControlPetDetail.Text = "Add new pet";
            };
            // Edit
            btnEdit.Click += delegate 
            { 
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(TabControlPetDetail);
                tabControl1.TabPages.Remove(TabControlPetList);
                TabControlPetDetail.Text = "Edit pet";
            };
            // Save Changes
            btnSave.Click += delegate 
            { 
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl1.TabPages.Add(TabControlPetList);
                    tabControl1.TabPages.Remove(TabControlPetDetail);
                }
    
                MessageBox.Show(Message);
            };
            // Cancel
            btnCancel.Click += delegate 
            { 
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(TabControlPetList);
                tabControl1.TabPages.Remove(TabControlPetDetail);
            };
            // Delete
            btnDelete.Click += delegate 
            { 
                var result = MessageBox.Show("Are you sure you want to delete the selected pet?", "Warning", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };    
        }

        //Properties
        public string PetId
        {
            get { return txbPetIdValue.Text; }
            set { txbPetIdValue.Text = value; }
        }
        public string PetName 
        {
            get { return txbPetNameValue.Text; }
            set { txbPetNameValue.Text = value; }
        }
        public string PetType 
        {
            get { return txbPetTypeValue.Text; }
            set { txbPetTypeValue.Text = value; }
        }
        public string PetColour 
        {
            get { return txbPetColourValue.Text; }
            set { txbPetColourValue.Text = value; }
        }
        public string SearchValue 
        {
            get { return txbSearchValue.Text; }
            set { txbSearchValue.Text = value; }
        }
        public bool IsEdit 
        {
            get { return isEdit; }
            set { isEdit = value; }
        }
        public bool IsSuccessful 
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }
        public string Message 
        {
            get { return message; }
            set { message = value; }
        }

        //Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //Methods
        public void SetPetListBindingSource(BindingSource petList)
        {
            dataGridView.DataSource = petList;
        }

        private static PetView instance;
        public static PetView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                if (parentContainer.ActiveMdiChild != null)
                    parentContainer.ActiveMdiChild.Close();

                instance = new PetView();
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
