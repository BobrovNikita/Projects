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
    public class PersonPetPresenter
    {
        //Fields
        private IPersonPetView view;
        private IPersonPetRepository repository;

        private BindingSource personPetBindingSource;
        private BindingSource personBindingSource;
        private BindingSource petBindingSource;

        private IEnumerable<PersonPetModel> personPetModels;
        private IEnumerable<PersonModel> personModels;
        private IEnumerable<PetModel> petModels;

        //Constructor
        public PersonPetPresenter(IPersonPetView view, IPersonPetRepository repository)
        {
            this.personPetBindingSource = new BindingSource();
            this.personBindingSource = new BindingSource();
            this.petBindingSource = new BindingSource();

            this.view = view;
            this.repository = repository;

            //Subscribe event
            view.SearchEvent += Search;
            view.AddNewEvent += AddNew;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadPersonPetList();

            //PersonPet Binding Source
            view.SetPersonPetListBindingSource(personPetBindingSource);
            view.SetPersonListBindingSource(personBindingSource);
            view.SetPetListBindingSource(petBindingSource);

            LoadCombobox();

            this.view.Show();
        }

        //Methods
        private void LoadPersonPetList()
        {
            personPetModels = repository.GetAll();
            personPetBindingSource.DataSource = personPetModels;
        }

        private void LoadCombobox()
        {
            personModels = repository.GetPersonAll();
            personBindingSource.DataSource = personModels;

            petModels = repository.GetPetAll();
            petBindingSource.DataSource = petModels;
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void CleanViewFields()
        {
            view.Id_PersonPet = "0";
            view.Id_Person = null;
            view.Id_Pet = null;
        }

        private void Save(object sender, EventArgs e)
        {
            var personPetModel = new PersonPetModel();
            personPetModel.Id = Convert.ToInt32(view.Id_PersonPet);
            personPetModel.IdPerson = view.Id_Person;
            personPetModel.IdPets = view.Id_Pet;
            try
            {
                new Common.ModelDataValidation().Validate(personPetModel);
                if (view.IsEdit)
                {
                    repository.Edit(personPetModel);
                    view.Message = "PersonPets edited successfuly";
                }
                else
                {
                    repository.Add(personPetModel);
                    view.Message = "PersonPets added successfuly";
                }
                view.IsSuccessful = true;
                LoadPersonPetList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void DeleteSelected(object sender, EventArgs e)
        {
            try
            {
                var personPetModel = (PersonPetModel)personPetBindingSource.Current;
                
                repository.Delete(personPetModel.Id);
                view.IsSuccessful = true;
                view.Message = "PersonPets deleted successfuly";
                LoadPersonPetList();
            }
            catch (Exception)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete personPets";
            }
        }

        private void LoadSelectedToEdit(object sender, EventArgs e)
        {
            var personPetModel = (PersonPetModel)personPetBindingSource.Current;
            view.Id_PersonPet = personPetModel.Id.ToString();
            view.Id_Person = personPetModel.IdPerson;
            view.Id_Pet = personPetModel.IdPets;
            view.IsEdit = true;
        }

        private void AddNew(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        private void Search(object sender, EventArgs e)
        {
            bool emptyValue = String.IsNullOrWhiteSpace(view.SearchValue);

            if (emptyValue == false)
                personPetModels = repository.GetAllByValue(view.SearchValue);
            else
                personPetModels = repository.GetAll();

            personPetBindingSource.DataSource = personPetModels;
        }
    }
}
