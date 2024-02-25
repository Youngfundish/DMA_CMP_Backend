using DMA_FinalProject.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_FinalProject.DAL.DAO
{
    public class UserConsentDAO
    {
        public bool Add(UserConsent userConsent)
        {
            SqlTransaction trans;
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                using (trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand insertCommand = new SqlCommand(
                            "INSERT INTO fp_UserConsent VALUES (@date, @userID, @domainUrl, @necessary, @functionality, @analytics, @marketing);", conn, trans))
                        {
                            insertCommand.Parameters.AddWithValue("@date", DateTime.Now);
                            insertCommand.Parameters.AddWithValue("@userID", userConsent.UserId);
                            insertCommand.Parameters.AddWithValue("domainUrl", userConsent.DomainUrl);
                            insertCommand.Parameters.AddWithValue("@necessary", userConsent.Necessary);
                            insertCommand.Parameters.AddWithValue("@functionality", userConsent.Functionality);
                            insertCommand.Parameters.AddWithValue("@analytics", userConsent.Analytics);
                            insertCommand.Parameters.AddWithValue("@marketing", userConsent.Marketing);

                            insertCommand.ExecuteNonQuery();
                        }
                        trans.Commit();
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

        public UserConsent? Get(string userId, string domainUrl)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                using SqlCommand command = new SqlCommand(
                    "SELECT * FROM fp_UserConsent WHERE userId = @userId and domainUrl = @domainUrl", conn);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@domainUrl", domainUrl);
                {
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            UserConsent userConsent;
                            userConsent = new()
                            {
                                UserId = (string)reader["userID"],
                                DomainUrl = (string)reader["domainUrl"],
                                Necessary = (bool)reader["necessary"],
                                Functionality = (bool)reader["functionality"],
                                Analytics = (bool)reader["analytics"],
                                Marketing = (bool)reader["marketing"]
                            };
                            return userConsent;
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
        
        public bool Update(UserConsent uc)
        {
            SqlTransaction trans;
            using(SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                using (trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand updateCommand = new SqlCommand(
                            "UPDATE fp_UserConsent SET date = @date, necessary = @necessary, functionality = @functionality, analytics = @analytics, " +
                            "marketing = @marketing WHERE userId = @userId AND domainUrl = @domainUrl", conn, trans))
                        {
                            updateCommand.Parameters.AddWithValue("@userId", uc.UserId);
                            updateCommand.Parameters.AddWithValue("@domainUrl", uc.DomainUrl);
                            updateCommand.Parameters.AddWithValue("@date", DateTime.Now);
                            updateCommand.Parameters.AddWithValue("@necessary", uc.Necessary);
                            updateCommand.Parameters.AddWithValue("@functionality", uc.Functionality);
                            updateCommand.Parameters.AddWithValue("@analytics", uc.Analytics);
                            updateCommand.Parameters.AddWithValue("@marketing", uc.Marketing);
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
