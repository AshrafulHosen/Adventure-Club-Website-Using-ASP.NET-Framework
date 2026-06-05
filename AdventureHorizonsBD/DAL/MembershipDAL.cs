using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD.DAL
{
    public class MembershipDAL
    {
        public void SubmitRequest(MembershipRequestModel request)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO MembershipRequests (UserID, MembershipType, Status, RequestDate)
                    VALUES (@UserID, @MembershipType, 'Pending', GETDATE())";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID", request.UserID);
                cmd.Parameters.AddWithValue("@MembershipType", request.MembershipType);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<MembershipRequestModel> GetPendingRequests()
        {
            List<MembershipRequestModel> requests = new List<MembershipRequestModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT m.*, u.FullName, u.Email 
                    FROM MembershipRequests m
                    INNER JOIN Users u ON m.UserID = u.UserID
                    WHERE m.Status = 'Pending'
                    ORDER BY m.RequestDate DESC";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requests.Add(new MembershipRequestModel
                        {
                            RequestID = Convert.ToInt32(reader["RequestID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            MembershipType = reader["MembershipType"].ToString(),
                            Status = reader["Status"].ToString(),
                            RequestDate = Convert.ToDateTime(reader["RequestDate"]),
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            return requests;
        }

        public void UpdateRequestStatus(int requestId, string status)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "UPDATE MembershipRequests SET Status = @Status, ReviewDate = GETDATE() WHERE RequestID = @RequestID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@RequestID", requestId);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
        public MembershipRequestModel GetRequestById(int requestId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT * FROM MembershipRequests WHERE RequestID = @RequestID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RequestID", requestId);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new MembershipRequestModel
                        {
                            RequestID = Convert.ToInt32(reader["RequestID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            MembershipType = reader["MembershipType"].ToString(),
                            Status = reader["Status"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
