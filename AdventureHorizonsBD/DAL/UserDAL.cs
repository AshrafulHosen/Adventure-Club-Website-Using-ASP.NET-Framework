using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD.DAL
{
    public class UserDAL
    {
        public int InsertUser(UserModel user)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO Users (FullName, Email, PasswordHash, Phone, MembershipPlan, ExperienceLevel, Role, IsApproved, RegistrationDate)
                    OUTPUT INSERTED.UserID
                    VALUES (@FullName, @Email, @PasswordHash, @Phone, @MembershipPlan, @ExperienceLevel, @Role, @IsApproved, GETDATE())";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Phone", (object)user.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MembershipPlan", (object)user.MembershipPlan ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ExperienceLevel", (object)user.ExperienceLevel ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Role", user.Role ?? "Member");
                cmd.Parameters.AddWithValue("@IsApproved", user.IsApproved);

                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public UserModel GetUserByEmail(string email)
        {
            UserModel user = null;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT * FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapReaderToUser(reader);
                    }
                }
            }
            return user;
        }

        public void UpdateUserApproval(int userId, bool isApproved)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "UPDATE Users SET IsApproved = @IsApproved WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IsApproved", isApproved);
                cmd.Parameters.AddWithValue("@UserID", userId);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<UserModel> GetApprovedMembers()
        {
            List<UserModel> members = new List<UserModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT * FROM Users WHERE Role = 'Member' AND IsApproved = 1 ORDER BY RegistrationDate DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        members.Add(MapReaderToUser(reader));
                    }
                }
            }
            return members;
        }

        private UserModel MapReaderToUser(SqlDataReader reader)
        {
            return new UserModel
            {
                UserID = Convert.ToInt32(reader["UserID"]),
                FullName = reader["FullName"].ToString(),
                Email = reader["Email"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : null,
                MembershipPlan = reader["MembershipPlan"] != DBNull.Value ? reader["MembershipPlan"].ToString() : null,
                ExperienceLevel = reader["ExperienceLevel"] != DBNull.Value ? reader["ExperienceLevel"].ToString() : null,
                Role = reader["Role"].ToString(),
                IsApproved = Convert.ToBoolean(reader["IsApproved"]),
                RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"])
            };
        }
    }
}
