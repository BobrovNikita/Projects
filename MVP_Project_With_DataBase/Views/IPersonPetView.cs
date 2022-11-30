using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVP_Project_With_DataBase.Models;
using System.Windows.Forms;

namespace MVP_Project_With_DataBase.Views
{
    public interface IPersonPetView
    {
        //Properties - Fields

        string Id_PersonPet { get; set; }
        PersonModel Id_Person { get; set; }
        PetModel Id_Pet { get; set; }

        string SearchValue{ get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Event
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        //Methods
        void SetPersonPetListBindingSource(BindingSource source);
        void SetPersonListBindingSource(BindingSource source);
        void SetPetListBindingSource(BindingSource source);
        void Show();


    }
}
