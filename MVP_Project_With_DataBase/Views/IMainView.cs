using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Project_With_DataBase.Views
{
    public interface IMainView
    {
        event EventHandler ShowPetsView;
        event EventHandler ShowPersonView;
        event EventHandler ShowPetsAndPersonView;
    }
}
