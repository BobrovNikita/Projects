using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVP_Project_With_DataBase.Models;
using System.Data.SqlClient;
using System.Data;

namespace MVP_Project_With_DataBase.Repositories
{
    public class PersonPetRepository : BaseRepository, IPersonPetRepository
    {
        //Constructor
        public PersonPetRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(PersonPetModel model)
        {
            CheckUniqueValueForAdd(model);
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into PersonPets values (@id_Pets, @id_Person)";
                command.Parameters.Add("@id_Person", SqlDbType.Int).Value = model.IdPerson.Id;
                command.Parameters.Add("@id_Pets", SqlDbType.Int).Value = model.IdPets.Id;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from PersonPets where Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(PersonPetModel model)
        {
            CheckUniqueValueForEdit(model);
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update PersonPets " +
                    "set Id_Pets = @id_Pet, Id_Person = @id_Person " +
                    "where Id = @id";
                command.Parameters.Add("@id_Pet", SqlDbType.Int).Value = model.IdPets.Id;
                command.Parameters.Add("@id_Person", SqlDbType.Int).Value = model.IdPerson.Id;
                command.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<PersonPetModel> GetAll()
        {

            var personPetList = new List<PersonPetModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM PersonPets";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var personPetModel = new PersonPetModel();
                        personPetModel.Id = (int)reader[0];
                        personPetModel.IdPets = GetPetById((int)reader[1]);
                        personPetModel.IdPerson = GetPersonById((int)reader[2]);
                        personPetList.Add(personPetModel);
                    }
                }
            }
            return personPetList;
        }

        public IEnumerable<PersonPetModel> GetAllByValue(string value)
        {
            var personPetList = new List<PersonPetModel>();
            int personPetId = int.TryParse(value, out _) ? Convert.ToInt32(value) : -1;
            string personPetValue = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT PersonPets.Id, Person.Id, Pets.Id, Person.Name, Pets.Type FROM PersonPets " +
                                       "INNER JOIN Person ON Person.Id = PersonPets.Id_Person " +
                                       "INNER JOIN Pets ON Pets.Id = PersonPets.Id_Pets " +
                                       "WHERE PersonPets.Id = @id or Person.Name like @value+'%' or Pets.Type like @value+'%'";
                command.Parameters.Add("@id", SqlDbType.Int).Value = personPetId;
                command.Parameters.Add("@value", SqlDbType.NVarChar).Value = personPetValue;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var personPetModel = new PersonPetModel();
                        personPetModel.Id = (int)reader[0];
                        personPetModel.IdPerson = GetPersonById((int)reader[1]);
                        personPetModel.IdPets = GetPetById((int)reader[2]);
                        personPetList.Add(personPetModel);
                    }
                }
            }
            return personPetList;
        }

        public IEnumerable<PersonModel> GetPersonAll()
        {
            var personList = new List<PersonModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Person";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var personModel = new PersonModel();
                        personModel.Id = (int)reader[0];
                        personModel.Name = reader[1].ToString();
                        personModel.Age = (int)reader[2];
                        personList.Add(personModel);
                    }
                }
            }
            return personList;
        }

        public IEnumerable<PetModel> GetPetAll()
        {
            var petList = new List<PetModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Pets";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var petModel = new PetModel();
                        petModel.Id = (int)reader[0];
                        petModel.Name = reader[1].ToString();
                        petModel.Type = reader[2].ToString();
                        petModel.Colour = reader[3].ToString();
                        petList.Add(petModel);
                    }
                }
            }
            return petList;
        }

        public PetModel GetPetById(int id)
        {
            var petModel = new PetModel();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Pets where Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        petModel.Id = (int)reader[0];
                        petModel.Name = reader[1].ToString();
                        petModel.Type = reader[2].ToString();
                        petModel.Colour = reader[3].ToString();
                    }
                }

            }
            return petModel;
        }

        public PersonModel GetPersonById(int id)
        {
            var personModel = new PersonModel();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Person where Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        personModel.Id = (int)reader[0];
                        personModel.Name = reader[1].ToString();
                        personModel.Age = (int)reader[2];
                    }
                }

            }
            return personModel;
        }

        public void CheckUniqueValueForAdd(PersonPetModel model)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                var personPetList = new List<PersonPetModel>();
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT PersonPets.Id, Person.Id, Pets.Id, Person.Name, Pets.Type FROM PersonPets " +
                                       "INNER JOIN Person ON Person.Id = PersonPets.Id_Person " +
                                       "INNER JOIN Pets ON Pets.Id = PersonPets.Id_Pets " +
                                       "WHERE Pets.Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = model.IdPets.Id;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var personPetModel = new PersonPetModel();
                        personPetModel.Id = (int)reader[0];
                        personPetModel.IdPerson = GetPersonById((int)reader[1]);
                        personPetModel.IdPets = GetPetById((int)reader[2]);
                        personPetList.Add(personPetModel);
                    }
                    if (personPetList.Count > 0)
                        throw new Exception("Pets must be unique value");
                }

            }
        }
        public void CheckUniqueValueForEdit(PersonPetModel model)
        {

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                var personPetList = new List<PersonPetModel>();
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT PersonPets.Id, Person.Id, Pets.Id, Person.Name, Pets.Type FROM PersonPets " +
                                       "INNER JOIN Person ON Person.Id = PersonPets.Id_Person " +
                                       "INNER JOIN Pets ON Pets.Id = PersonPets.Id_Pets " +
                                       "WHERE Pets.Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = model.IdPets.Id;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var personPetModel = new PersonPetModel();
                        personPetModel.Id = (int)reader[0];
                        personPetModel.IdPerson = GetPersonById((int)reader[1]);
                        personPetModel.IdPets = GetPetById((int)reader[2]);
                        personPetList.Add(personPetModel);
                    }
                    if (personPetList[0].IdPets.Id == model.IdPets.Id)
                        return;
                    if (personPetList.Count > 0)
                        throw new Exception("Pets must be unique value");
                }

            }
        }
    }
}
