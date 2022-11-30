using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Project_With_DataBase.Models
{
    public interface IPersonRepository
    {
        void Add(PersonModel Personmodel);
        void Edit(PersonModel Personmodel);
        void Delete(int id);
        IEnumerable<PersonModel> GetAll();
        IEnumerable<PersonModel> GetByValue(string value);
    }
}
