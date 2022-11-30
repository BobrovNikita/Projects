using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVP_Project_With_DataBase.Views
{
    public interface IPersonVIew
    {
        //Properties - Fields
        string PersonId { get; set; }
        string PersonName { get; set; }
        int PersonAge { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }


        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        //Methods

        void SetPersonListBindingSource(BindingSource sourse);
        void Show();

    }
}
