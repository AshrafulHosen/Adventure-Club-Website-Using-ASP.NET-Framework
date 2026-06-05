using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD.DAL
{
    public class ContactDAL
    {
        public void SubmitMessage(ContactMessageModel msg)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO ContactMessages (Email, Message, IsRead, SentDate)
                    VALUES (@Email, @Message, 0, GETDATE())";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", msg.Email);
                cmd.Parameters.AddWithValue("@Message", msg.Message);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<ContactMessageModel> GetAllMessages()
        {
            List<ContactMessageModel> msgs = new List<ContactMessageModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT * FROM ContactMessages ORDER BY SentDate DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        msgs.Add(new ContactMessageModel
                        {
                            MessageID = Convert.ToInt32(reader["MessageID"]),
                            Email = reader["Email"].ToString(),
                            Message = reader["Message"].ToString(),
                            IsRead = Convert.ToBoolean(reader["IsRead"]),
                            SentDate = Convert.ToDateTime(reader["SentDate"])
                        });
                    }
                }
            }
            return msgs;
        }

        public void MarkAsRead(int messageId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "UPDATE ContactMessages SET IsRead = 1 WHERE MessageID = @MessageID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MessageID", messageId);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
