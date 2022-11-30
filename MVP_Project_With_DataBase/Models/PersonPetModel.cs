using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace MVP_Project_With_DataBase.Models
{
    public class PersonPetModel
    {
        //Fields
        private int id;
        public PersonModel IdPerson;
        public PetModel IdPets;


        //Properties
        [DisplayName("ID")]
        public int Id 
        {
            get => id;
            set => id = value; 
        }

        [DisplayName("Name person")]
        [Required(ErrorMessage = "Person name is required")]
        public string NamePerson
        {
            get => IdPerson.Name;
            set => IdPerson.Name = value;
        }

        [DisplayName("Name pet")]
        [Required(ErrorMessage = "Pet name is required")]
        public string NamePet
        {
            get => IdPets.Name;
            set => IdPets.Name = value;
        }

        [DisplayName("Type pet")]
        public string TypePet
        {
            get => IdPets.Type;
            set => IdPets.Type = value;
        }
    }
}
