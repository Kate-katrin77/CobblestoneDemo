using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Data
{
    public class EmployeeData
    {
        private readonly SqlConnection connection;
        private SqlCommand command;

        public EmployeeData()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public IEnumerable<DbEmployee> GetList()
        {
            var getCommand = $@"select Id, firstName, LastName, Birthday, Age from Employee;";
            try
            {
                using (connection)
                using (command = new SqlCommand(getCommand, connection))
                {
                    var employees = new List<DbEmployee>();
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        employees.Add(MapEmployee(reader));

                    return employees;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public DbEmployee GetEmployee(int id, out string error)
        {
            error = string.Empty;
            var getCommand = $@"select Id, firstName, LastName, Birthday, Age from Employee where id = {id};";
            try
            {
                using (connection)
                using (command = new SqlCommand(getCommand, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                        return MapEmployee(reader);
                }
                error = $"Did not find any employee with id: {id}";
                return null;
            }
            catch (Exception e)
            {
                error = "Someting went wrong";
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public int? SaveEmployee(int id, string firstName, string lastName, DateTime birthday, out string errorMessege)
        {
            errorMessege = string.Empty;
            var age = (DateTime.Now.Year - birthday.Year);
            string saveCommand = $@"
                                    insert into Employee(FirstName, LastName, Birthday, Age) 
                                    values ('{firstName}', '{lastName}', '{birthday}', {age}); 
                                    select SCOPE_IDENTITY()"
                                    ;
            try
            {
                using (connection)
                using (command = new SqlCommand(saveCommand, connection))
                {
                    connection.Open();
                    var result = command.ExecuteScalar();

                    return Convert.ToInt32(result);
                }
            }
            catch (Exception e)
            {
                errorMessege = "Something went wrong. contact support";
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        private DbEmployee MapEmployee(SqlDataReader reader)
        {
            return new DbEmployee
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Birthday = reader.GetDateTime(3),
                Age = reader.IsDBNull(4) ? 0 : reader.GetInt32(4)
            };
        }
    }
}