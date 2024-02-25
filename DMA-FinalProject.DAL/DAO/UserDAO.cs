using DMA_FinalProject.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Exceptions;

namespace DMA_FinalProject.DAL.DAO
{
    public class UserDAO : IDMAFinalProjectDAO<User>
    {
        public bool Add(User u)
        {
            SqlTransaction trans;
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                using (trans = conn.BeginTransaction())
                {
                    try
                    {
                        //Check if there is an existing user with the given browserId
                        bool isUserExisting = false;
                        using (SqlCommand readCommand = new SqlCommand(
                            "SELECT * FROM fp_User WHERE browserId = @browserId", conn, trans))
                        {
                            readCommand.Parameters.AddWithValue("@browserId", u.browserId);
                            using( SqlDataReader reader = readCommand.ExecuteReader())
                            {
                                if(reader.Read())
                                {
                                    isUserExisting = true;
                                }
                            }
                        }

                        if (!isUserExisting)
                        {
                            using (SqlCommand insertCommand = new SqlCommand(
                                "INSERT INTO fp_User VALUES (@browserId);", conn, trans))
                            {
                                insertCommand.Parameters.AddWithValue("browserId", u.browserId);
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
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
            return true;
        }

        public User? Get(dynamic key)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                using SqlCommand command = new SqlCommand(
                    "SELECT * FROM fp_User WHERE browserId = @browserId", conn);
                command.Parameters.AddWithValue("@browserId", key);
                {
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            User user = new User()
                            {
                                browserId = (string?)reader["browserId"],
                            };
                            return user;
                        }
                    }
                    catch (DataAccessException)
                    {
                        throw new DataAccessException("Can't access data");
                    }
                }
            }
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            string sqlStatement = "SELECT * FROM fp_User;";
            List<User> users = new();

            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                SqlCommand getAllCommand = new SqlCommand(sqlStatement, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = getAllCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User()
                        {
                            browserId = (string?)reader["browserId"],
                        };
                        users.Add(user);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return users;
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
                        using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM fp_User WHERE browserId = @browserId", conn, trans))
                        {
                            deleteCommand.Parameters.AddWithValue("@browserId", key);

                            //If no user was found and deleted return a false
                            if(deleteCommand.ExecuteNonQuery() == 0) {
                                return false;
                            }
                            trans.Commit();
                            return true;
                        }
                    }
                    catch (DataAccessException)
                    {
                        trans.Rollback();
                        throw new DataAccessException("Can't access data");
                    }
                }
            }
        }

        public bool Update(User o)
        {
            throw new NotImplementedException();
        }
    }
}
