using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD.DAL
{
    public class RegionDAL
    {
        public List<RegionModel> GetAllRegions()
        {
            List<RegionModel> regions = new List<RegionModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT * FROM Regions";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        regions.Add(new RegionModel
                        {
                            RegionID = Convert.ToInt32(reader["RegionID"]),
                            RegionName = reader["RegionName"].ToString(),
                            Highlights = reader["Highlights"].ToString(),
                            Description = reader["Description"].ToString(),
                            PopularTrips = reader["PopularTrips"].ToString()
                        });
                    }
                }
            }
            return regions;
        }
    }
}
