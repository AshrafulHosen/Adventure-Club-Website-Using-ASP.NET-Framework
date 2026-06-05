using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD.DAL
{
    public class ReviewDAL
    {
        public void AddReview(ReviewModel review)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO Reviews (UserID, EventName, Title, ReviewText, Rating, IsApproved, ReviewDate)
                    VALUES (@UserID, @EventName, @Title, @ReviewText, @Rating, 0, GETDATE())";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID", review.UserID);
                cmd.Parameters.AddWithValue("@EventName", review.EventName);
                cmd.Parameters.AddWithValue("@Title", review.Title);
                cmd.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                cmd.Parameters.AddWithValue("@Rating", review.Rating);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<ReviewModel> GetUserReviews(int userId)
        {
            List<ReviewModel> reviews = new List<ReviewModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT * FROM Reviews WHERE UserID = @UserID ORDER BY ReviewDate DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new ReviewModel
                        {
                            ReviewID = Convert.ToInt32(reader["ReviewID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            EventName = reader["EventName"].ToString(),
                            Title = reader["Title"].ToString(),
                            ReviewText = reader["ReviewText"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            IsApproved = Convert.ToBoolean(reader["IsApproved"]),
                            ReviewDate = Convert.ToDateTime(reader["ReviewDate"])
                        });
                    }
                }
            }
            return reviews;
        }

        public List<ReviewModel> GetAllReviews()
        {
            List<ReviewModel> reviews = new List<ReviewModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT r.*, u.FullName as UserFullName 
                    FROM Reviews r
                    INNER JOIN Users u ON r.UserID = u.UserID
                    ORDER BY r.ReviewDate DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new ReviewModel
                        {
                            ReviewID = Convert.ToInt32(reader["ReviewID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            UserFullName = reader["UserFullName"].ToString(),
                            EventName = reader["EventName"].ToString(),
                            Title = reader["Title"].ToString(),
                            ReviewText = reader["ReviewText"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            IsApproved = Convert.ToBoolean(reader["IsApproved"]),
                            ReviewDate = Convert.ToDateTime(reader["ReviewDate"])
                        });
                    }
                }
            }
            return reviews;
        }

        public void UpdateReviewStatus(int reviewId, bool isApproved)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "UPDATE Reviews SET IsApproved = @IsApproved WHERE ReviewID = @ReviewID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IsApproved", isApproved);
                cmd.Parameters.AddWithValue("@ReviewID", reviewId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<ReviewModel> GetApprovedReviews()
        {
            List<ReviewModel> reviews = new List<ReviewModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT r.*, u.FullName as UserFullName 
                    FROM Reviews r
                    INNER JOIN Users u ON r.UserID = u.UserID
                    WHERE r.IsApproved = 1
                    ORDER BY r.ReviewDate DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new ReviewModel
                        {
                            ReviewID = Convert.ToInt32(reader["ReviewID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            UserFullName = reader["UserFullName"].ToString(),
                            EventName = reader["EventName"].ToString(),
                            Title = reader["Title"].ToString(),
                            ReviewText = reader["ReviewText"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            IsApproved = true,
                            ReviewDate = Convert.ToDateTime(reader["ReviewDate"])
                        });
                    }
                }
            }
            return reviews;
        }

        public void DeleteReview(int reviewId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "DELETE FROM Reviews WHERE ReviewID = @ReviewID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}