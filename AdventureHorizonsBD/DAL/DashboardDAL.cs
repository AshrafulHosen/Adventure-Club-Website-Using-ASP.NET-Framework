using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdventureHorizonsBD.DAL
{
    public class DashboardDAL
    {
        public Dictionary<string, int> GetDashboardStats()
        {
            var stats = new Dictionary<string, int>();
            
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT
                        (SELECT COUNT(*) FROM Users WHERE Role='Member') AS TotalUsers,
                        (SELECT COUNT(*) FROM Users WHERE IsApproved=1 AND Role='Member') AS ApprovedMembers,
                        (SELECT COUNT(*) FROM MembershipRequests WHERE Status='Pending') AS PendingRequests,
                        (SELECT COUNT(*) FROM Events) AS TotalEvents,
                        (SELECT COUNT(*) FROM EventRegistrations) AS TotalRegistrations,
                        (SELECT COUNT(*) FROM ContactMessages) AS TotalMessages,
                        (SELECT COUNT(*) FROM ContactMessages WHERE IsRead=0) AS UnreadMessages,
                        (SELECT COUNT(*) FROM Gallery) AS TotalGalleryImages
                ";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stats["TotalUsers"] = Convert.ToInt32(reader["TotalUsers"]);
                        stats["ApprovedMembers"] = Convert.ToInt32(reader["ApprovedMembers"]);
                        stats["PendingRequests"] = Convert.ToInt32(reader["PendingRequests"]);
                        stats["TotalEvents"] = Convert.ToInt32(reader["TotalEvents"]);
                        stats["TotalRegistrations"] = Convert.ToInt32(reader["TotalRegistrations"]);
                        stats["TotalMessages"] = Convert.ToInt32(reader["TotalMessages"]);
                        stats["UnreadMessages"] = Convert.ToInt32(reader["UnreadMessages"]);
                        stats["TotalGalleryImages"] = Convert.ToInt32(reader["TotalGalleryImages"]);
                    }
                }
            }
            
            return stats;
        }
    }
}
