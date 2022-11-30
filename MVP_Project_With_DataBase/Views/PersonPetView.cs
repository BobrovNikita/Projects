using MVP_Project_With_DataBase.Models;
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
    public partial class PersonPetView : Form, IPersonPetView
    {
        //Fields
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        public PersonPetView()
        {
            InitializeComponent();
            AssosiateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(TabPagePersonPetDetail);
            CloseForm.Click += delegate { this.Close(); };
            
        }
        //Methods
        private void AssosiateAndRaiseViewEvents()
        {

            //Search
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txbSearchValue.KeyDown += (s, e) =>
            {
                if (e.KeyData == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };

            //Add new
            btnAddNew.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(TabPagePersonPetDetail);
                tabControl1.TabPages.Remove(TabPagePersonPetList);
                TabPagePersonPetDetail.Text = "Person Add New";
            };

            //Edit
            btnEdit.Click += delegate
            {
                tabControl1.TabPages.Remove(TabPagePersonPetList);
                tabControl1.TabPages.Add(TabPagePersonPetDetail);
                EditEvent?.Invoke(this, EventArgs.Empty);
                TabPagePersonPetDetail.Text = "Person Edit";
            };

            //Delete
            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected personPet", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            //Save 
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl1.TabPages.Add(TabPagePersonPetList);
                    tabControl1.TabPages.Remove(TabPagePersonPetDetail);                    
                }

                MessageBox.Show(Message);
            };

            //Cancel
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(TabPagePersonPetList);
                tabControl1.TabPages.Remove(TabPagePersonPetDetail);
            };
        }

        //Properties
        public string Id_PersonPet 
        {
            get => txtPersonAndPetID.Text;
            set => txtPersonAndPetID.Text = value; 
        }
        public PersonModel Id_Person
        {
            get => (PersonModel)cmbPersonID.SelectedItem;
            set 
            {
                if (value != null)
                {
                    PersonModel a = value;
                    cmbPersonID.SelectedValue = a;
                    cmbPersonID.SelectedValue = a.Id;
                }
                else
                {
                    cmbPersonID.SelectedItem = value;
                }
            }
        }
        public PetModel Id_Pet
        {
            get => (PetModel)cmbPetID.SelectedItem;
            set 
            {
                if (value != null)
                {
                    PetModel a = value;
                    cmbPetID.SelectedItem = value;
                    cmbPetID.SelectedValue = a.Id;
                }
                else
                {
                    cmbPetID.SelectedItem = value;
                }
            } 
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

        public void SetPersonListBindingSource(BindingSource source)
        {
            cmbPersonID.DataSource = source;
            cmbPersonID.DisplayMember = "Name";
            cmbPersonID.ValueMember = "Id";
        }
        public void SetPetListBindingSource(BindingSource source)
        {
            cmbPetID.DataSource = source;
            cmbPetID.DisplayMember = "Name";
            cmbPetID.ValueMember = "Id";
        }

        public void SetPersonPetListBindingSource(BindingSource source)
        {
            dataGridView.DataSource = source;
        }


        private static PersonPetView instance;

        public static PersonPetView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                if (parentContainer.ActiveMdiChild != null)
                    parentContainer.ActiveMdiChild.Close();

                instance = new PersonPetView();
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
