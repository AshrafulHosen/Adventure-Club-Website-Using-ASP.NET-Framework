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
                string sql = @"INSERT INTO Reviews (UserID,EventName,Title,ReviewText,Rating,IsApproved,ReviewDate)
                               VALUES (@UserID,@EventName,@Title,@ReviewText,@Rating,0,GETDATE())";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID",     review.UserID);
                cmd.Parameters.AddWithValue("@EventName",  review.EventName);
                cmd.Parameters.AddWithValue("@Title",      review.Title);
                cmd.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                cmd.Parameters.AddWithValue("@Rating",     review.Rating);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<ReviewModel> GetUserReviews(int userId)
        {
            var list = new List<ReviewModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Reviews WHERE UserID=@UserID ORDER BY ReviewDate DESC", conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(Map(r));
            }
            return list;
        }

        public List<ReviewModel> GetAllReviews()
        {
            var list = new List<ReviewModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"SELECT r.*,u.FullName AS UserFullName
                    FROM Reviews r INNER JOIN Users u ON r.UserID=u.UserID
                    ORDER BY r.ReviewDate DESC", conn);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapWithUser(r));
            }
            return list;
        }

        public List<ReviewModel> GetApprovedReviews()
        {
            var list = new List<ReviewModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                var cmd = new SqlCommand(@"SELECT r.*,u.FullName AS UserFullName
                    FROM Reviews r INNER JOIN Users u ON r.UserID=u.UserID
                    WHERE r.IsApproved=1 ORDER BY r.ReviewDate DESC", conn);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapWithUser(r));
            }
            return list;
        }

        public void UpdateReviewStatus(int reviewId, bool isApproved)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                var cmd = new SqlCommand("UPDATE Reviews SET IsApproved=@A WHERE ReviewID=@ID", conn);
                cmd.Parameters.AddWithValue("@A",  isApproved);
                cmd.Parameters.AddWithValue("@ID", reviewId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteReview(int reviewId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                var cmd = new SqlCommand("DELETE FROM Reviews WHERE ReviewID=@ID", conn);
                cmd.Parameters.AddWithValue("@ID", reviewId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private ReviewModel Map(SqlDataReader r)
        {
            return new ReviewModel
            {
                ReviewID   = Convert.ToInt32(r["ReviewID"]),
                UserID     = Convert.ToInt32(r["UserID"]),
                EventName  = r["EventName"].ToString(),
                Title      = r["Title"].ToString(),
                ReviewText = r["ReviewText"].ToString(),
                Rating     = Convert.ToInt32(r["Rating"]),
                IsApproved = Convert.ToBoolean(r["IsApproved"]),
                ReviewDate = Convert.ToDateTime(r["ReviewDate"])
            };
        }
        private ReviewModel MapWithUser(SqlDataReader r)
        {
            var m = Map(r);
            m.UserFullName = r["UserFullName"] != DBNull.Value ? r["UserFullName"].ToString() : "";
            return m;
        }
    }
}
