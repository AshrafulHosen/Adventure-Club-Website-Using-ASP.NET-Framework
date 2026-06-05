using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD.DAL
{
    public class GalleryDAL
    {
        public List<GalleryModel> GetApprovedImages()
        {
            List<GalleryModel> images = new List<GalleryModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT g.*, u.FullName as UploadedByUserName
                    FROM Gallery g
                    LEFT JOIN Users u ON g.UploadedByUserID = u.UserID
                    WHERE g.IsApproved = 1
                    ORDER BY g.UploadDate DESC";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        images.Add(new GalleryModel
                        {
                            ImageID = Convert.ToInt32(reader["ImageID"]),
                            Title = reader["Title"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            UploadedByUserName = reader["UploadedByUserName"] != DBNull.Value ? reader["UploadedByUserName"].ToString() : "Admin"
                        });
                    }
                }
            }
            return images;
        }

        public List<GalleryModel> GetAllImages()
        {
            List<GalleryModel> images = new List<GalleryModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT g.*, u.FullName as UploadedByUserName
                    FROM Gallery g
                    LEFT JOIN Users u ON g.UploadedByUserID = u.UserID
                    ORDER BY g.UploadDate DESC";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        images.Add(new GalleryModel
                        {
                            ImageID = Convert.ToInt32(reader["ImageID"]),
                            Title = reader["Title"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            IsApproved = Convert.ToBoolean(reader["IsApproved"]),
                            UploadDate = reader["UploadDate"] != DBNull.Value ? Convert.ToDateTime(reader["UploadDate"]) : DateTime.MinValue,
                            UploadedByUserName = reader["UploadedByUserName"] != DBNull.Value ? reader["UploadedByUserName"].ToString() : "Admin"
                        });
                    }
                }
            }
            return images;
        }

        public void AddImage(GalleryModel img)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO Gallery (Title, ImageURL, Description, UploadedByUserID, IsApproved, UploadDate)
                    VALUES (@Title, @ImageURL, @Description, @UploadedByUserID, @IsApproved, GETDATE())";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Title", img.Title);
                cmd.Parameters.AddWithValue("@ImageURL", img.ImageURL);
                cmd.Parameters.AddWithValue("@Description", (object)img.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UploadedByUserID", (object)img.UploadedByUserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsApproved", img.IsApproved);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
        public void ApproveImage(int imageId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "UPDATE Gallery SET IsApproved = 1 WHERE ImageID = @ImageID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ImageID", imageId);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteImage(int imageId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "DELETE FROM Gallery WHERE ImageID = @ImageID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ImageID", imageId);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
        public List<GalleryModel> GetUserImages(int userId)
        {
            List<GalleryModel> images = new List<GalleryModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT * FROM Gallery WHERE UploadedByUserID = @UserID ORDER BY UploadDate DESC";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        images.Add(new GalleryModel
                        {
                            ImageID = Convert.ToInt32(reader["ImageID"]),
                            Title = reader["Title"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            IsApproved = Convert.ToBoolean(reader["IsApproved"]),
                            UploadDate = Convert.ToDateTime(reader["UploadDate"])
                        });
                    }
                }
            }
            return images;
        }
    }
}
