using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MVP_Project_With_DataBase.Models;

namespace MVP_Project_With_DataBase.Repositories
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        //Constructor
        public PersonRepository(string sqlConnectionString)
        {
            connectionString = sqlConnectionString;
        }

        public void Add(PersonModel Personmodel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Person values (@name, @age)";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = Personmodel.Name;
                command.Parameters.Add("@age", SqlDbType.Int).Value = Personmodel.Age;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection= connection;
                command.CommandText = "delete from Person where Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(PersonModel Personmodel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update Person " +
                    "set Name = @name, Age = @age " +
                    "where Id = @id";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = Personmodel.Name;
                command.Parameters.Add("@age", SqlDbType.Int).Value = Personmodel.Age;
                command.Parameters.Add("@id", SqlDbType.Int).Value = Personmodel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<PersonModel> GetAll()
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

        public IEnumerable<PersonModel> GetByValue(string value)
        {
            var personList = new List<PersonModel>();
            int personId = int.TryParse(value, out _) ? Convert.ToInt32(value) : -1;
            string personName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Person " +
                                       "WHERE Id = @id or Name like @name+'%'";
                command.Parameters.Add("@id", SqlDbType.Int).Value = personId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = personName;
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
    }
}
