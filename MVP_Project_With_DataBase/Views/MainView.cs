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
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            btnPets.Click += delegate { ShowPetsView?.Invoke(this, EventArgs.Empty); };
            btnPersons.Click += delegate { ShowPersonView?.Invoke(this, EventArgs.Empty); };
            btnPersonPets.Click += delegate { ShowPetsAndPersonView?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler ShowPetsView;
        public event EventHandler ShowPersonView;
        public event EventHandler ShowPetsAndPersonView;
    }
}
