using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Project_With_DataBase.Models
{
    public interface IPersonPetRepository
    {
        void Add(PersonPetModel model);
        void Edit(PersonPetModel model);
        void Delete(int id);

        IEnumerable<PersonPetModel> GetAll();
        IEnumerable<PersonPetModel> GetAllByValue(string value);
        IEnumerable<PersonModel> GetPersonAll();
        IEnumerable<PetModel> GetPetAll();
        PersonModel GetPersonById(int id);
        PetModel GetPetById(int id);
    }
}
