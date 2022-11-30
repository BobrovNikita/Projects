using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVP_Project_With_DataBase.Models;
using MVP_Project_With_DataBase.Repositories;
using MVP_Project_With_DataBase.Views;
using System.Windows.Forms;

namespace MVP_Project_With_DataBase.Presenters
{
    public class MainPresenter
    {
        private IMainView _mainView;
        private readonly string sqlConnetionString;

        public MainPresenter(IMainView mainView, string sqlConnetionString)
        {
            _mainView = mainView;
            this.sqlConnetionString = sqlConnetionString;
            _mainView.ShowPetsView += ShowPetsView;
            _mainView.ShowPersonView += ShowPersonView;
            _mainView.ShowPetsAndPersonView += ShowPersonPetsView;
        }

        private void ShowPersonPetsView(object sender, EventArgs e)
        {
            IPersonPetView view = PersonPetView.GetInstance((MainView)_mainView);
            IPersonPetRepository repository = new PersonPetRepository(sqlConnetionString);
            new PersonPetPresenter(view, repository);
        }

        private void ShowPetsView(object sender, EventArgs e)
        {            
            IPetView view = PetView.GetInstance((MainView)_mainView);
            IPetRepository repository = new PetRepository(sqlConnetionString);
            new PetPresenter(view, repository);
        }

        private void ShowPersonView(object sender, EventArgs e)
        {
            IPersonVIew view = PersonView.GetInstance((MainView)_mainView);
            IPersonRepository repository = new PersonRepository(sqlConnetionString);
            new PersonPresenter(view, repository);
        }
    }
}
