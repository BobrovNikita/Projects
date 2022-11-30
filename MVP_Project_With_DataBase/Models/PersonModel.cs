using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVP_Project_With_DataBase.Models
{
    public class PersonModel
    {
        //Fields
        private int id;
        private string name;
        private int age;


        //Properties
        [DisplayName("Person ID")]
        public int Id 
        { 
            get => id;
            set => id = value;
        }

        [DisplayName("Person Name")]
        [Required(ErrorMessage = "Person name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Person name must be between 2 and 50")]
        public string Name
        {
            get => name;
            set => name = value;
        }
        [DisplayName("Person Age")]
        [Required(ErrorMessage = "Person age is required")]
        [Range(18,100, ErrorMessage = "Person age must be between 18 and 100")]
        public int Age
        {
            get => age;
            set => age = value;
        }
    }
}
