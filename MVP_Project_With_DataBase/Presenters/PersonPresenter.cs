using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVP_Project_With_DataBase.Models;
using MVP_Project_With_DataBase.Views;
using System.Windows.Forms;

namespace MVP_Project_With_DataBase.Presenters
{
    public class PersonPresenter
    {
        //Fields
        private IPersonVIew view;
        private IPersonRepository repository;
        private BindingSource personBindingSource;
        private IEnumerable<PersonModel> personList;

        //Constructor
        public PersonPresenter(IPersonVIew view, IPersonRepository repository)
        {
            this.personBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            //Subscrube event
            view.SearchEvent += SearchPerson;
            view.AddNewEvent += AddNewPerson;
            view.EditEvent += LoadSelectedPersonToEdit;
            view.DeleteEvent += DeleteSelectedPerson;
            view.SaveEvent += SavePerson;
            view.CancelEvent += CancelAction;


            //Person Binding Source
            view.SetPersonListBindingSource(personBindingSource);

            LoadPersonList();

            this.view.Show();
        }


        //Methods
        private void LoadPersonList()
        {
            personList = repository.GetAll();
            personBindingSource.DataSource = personList;
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void CleanViewFields()
        {
            view.PersonId = "0";
            view.PersonName = "";
            view.PersonAge = 0;
        }

        private void SavePerson(object sender, EventArgs e)
        {
            var personModel = new PersonModel();
            personModel.Id = Convert.ToInt32(view.PersonId);
            personModel.Name = view.PersonName;
            personModel.Age = Convert.ToInt32(view.PersonAge);
            try
            {
                new Common.ModelDataValidation().Validate(personModel);
                if (view.IsEdit)
                {
                    repository.Edit(personModel);
                    view.Message = "Person edited successfuly";
                }
                else
                {
                    repository.Add(personModel);
                    view.Message = "Person added successfuly";
                }
                view.IsSuccessful = true;
                LoadPersonList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void DeleteSelectedPerson(object sender, EventArgs e)
        {
            try
            {
                var personModel = (PersonModel)personBindingSource.Current;
                repository.Delete(personModel.Id);
                view.IsSuccessful = true;
                view.Message = "Person deleted successfuly";
                LoadPersonList();
            }
            catch (Exception)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete person";
            }
        }

        private void LoadSelectedPersonToEdit(object sender, EventArgs e)
        {
            var personModel = (PersonModel)personBindingSource.Current;
            view.PersonId = personModel.Id.ToString();
            view.PersonName = personModel.Name.ToString();
            view.PersonAge = personModel.Age;
            view.IsEdit = true;
        }

        private void AddNewPerson(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        private void SearchPerson(object sender, EventArgs e)
        {
            bool emptyValue = String.IsNullOrWhiteSpace(view.SearchValue);

            if (emptyValue == false)
                personList = repository.GetByValue(view.SearchValue);
            else
                personList = repository.GetAll();

            personBindingSource.DataSource = personList;
        }
    }
}
