//using DataAccessLayer.Exceptions;
using DMA_FinalProject.DAL.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DMA_FinalProject.DAL.DAO
{
    public class EmployeeDAO : IDMAFinalProjectDAO<Employee>
    {
        public bool Add(Employee e)
        {
            SqlTransaction trans;
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                using (trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Check if there's already an existing user with the email
                        bool userExistsAlready = false;
                        using (SqlCommand readCommand = new SqlCommand(
                            "SELECT * FROM fp_Employee WHERE email = @email", conn, trans))
                        {
                            readCommand.Parameters.AddWithValue("@email", e.Email);
                            using (SqlDataReader reader = readCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    userExistsAlready = true;
                                }
                            }
                        }

                        if (!userExistsAlready)
                        {
                            using (SqlCommand insertCommand = new SqlCommand(
                                "INSERT INTO fp_Employee VALUES (@name, @email, @phone, @passwordHash, @companyID);", conn, trans))
                            {
                                insertCommand.Parameters.AddWithValue("@name", e.Name);
                                insertCommand.Parameters.AddWithValue("@email", e.Email);
                                insertCommand.Parameters.AddWithValue("@phone", e.Phone);
                                insertCommand.Parameters.AddWithValue("@passwordHash", e.PasswordHash);
                                insertCommand.Parameters.AddWithValue("@companyID", e.CompanyId);
                                insertCommand.ExecuteNonQuery();
                            }
                            trans.Commit();
                        }
                        else
                        {
                            conn.Close();
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
            return true;
        }

        public Employee? Get(dynamic key)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                using SqlCommand command = new SqlCommand(
                    "SELECT name, phone, email, companyID FROM fp_Employee WHERE email = @email", conn);
                command.Parameters.AddWithValue("@email", key);
                {
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Employee employee = new Employee()
                            {
                                Name = (string?)reader["name"],
                                Email = (string?)reader["email"],
                                Phone = (string?)reader["phone"],
                                CompanyId = (int)reader["companyID"]
                            };
                            return employee;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return null;
        }

        public IEnumerable<Employee> GetAll()
        {
            string sqlStatement = "SELECT * FROM fp_Employee;";
            List<Employee> employees = new();

            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                SqlCommand getAllCommand = new SqlCommand(sqlStatement, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = getAllCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee()
                        {
                            Name = (string?)reader["name"],
                            Email = (string?)reader["email"],
                            Phone = (string?)reader["phone"],
                            CompanyId = (int)reader["companyID"]
                        };
                        employees.Add(employee);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return employees;
        }

        public bool Remove(dynamic key)
        {
            SqlTransaction trans;
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                using (trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM fp_Employee WHERE email = @email", conn, trans))
                        {
                            deleteCommand.Parameters.AddWithValue("@email", key);
                            deleteCommand.ExecuteNonQuery();
                            trans.Commit();
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool Update(Employee o)
        {
            //int rows = -1;
            SqlTransaction trans;
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open ();
                using (trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand updateCommand = new SqlCommand(
                            "UPDATE fp_Employee SET name = @name, phone = @phone, companyID = @companyID WHERE email = @email", conn, trans))
                        {
                            updateCommand.Parameters.AddWithValue("@name", o.Name);
                            updateCommand.Parameters.AddWithValue("@phone", o.Phone);
                            updateCommand.Parameters.AddWithValue("@email", o.Email);
                            updateCommand.Parameters.AddWithValue("@companyID", o.CompanyId);
                            updateCommand.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw new Exception("Update exception");
                    }
                }
            }
            return true;
        }
    }
}
