using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD.DAL
{
    public class EventDAL
    {
        public List<EventModel> GetAllEvents()
        {
            List<EventModel> events = new List<EventModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT * FROM Events ORDER BY CreatedDate DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new EventModel
                        {
                            EventID = Convert.ToInt32(reader["EventID"]),
                            Title = reader["Title"].ToString(),
                            EventDate = reader["EventDate"].ToString(),
                            EventDuration = reader["EventDuration"].ToString(),
                            Region = reader["Region"].ToString(),
                            Description = reader["Description"].ToString()
                        });
                    }
                }
            }
            return events;
        }

        public void AddEvent(EventModel evt)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO Events (Title, EventDate, EventDuration, Region, Description, CreatedDate)
                    VALUES (@Title, @EventDate, @EventDuration, @Region, @Description, GETDATE())";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Title", evt.Title);
                cmd.Parameters.AddWithValue("@EventDate", evt.EventDate);
                cmd.Parameters.AddWithValue("@EventDuration", evt.EventDuration);
                cmd.Parameters.AddWithValue("@Region", evt.Region);
                cmd.Parameters.AddWithValue("@Description", evt.Description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEvent(int eventId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                // First delete registrations
                string sql1 = "DELETE FROM EventRegistrations WHERE EventID = @EventID";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@EventID", eventId);
                
                string sql2 = "DELETE FROM Events WHERE EventID = @EventID";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@EventID", eventId);
                
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                cmd1.Transaction = transaction;
                cmd2.Transaction = transaction;
                
                try 
                {
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void RegisterForEvent(EventRegistrationModel reg)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO EventRegistrations (UserID, EventID, NumberOfParticipants, SpecialRequests, Status, RegistrationDate)
                    VALUES (@UserID, @EventID, @NumberOfParticipants, @SpecialRequests, 'Pending', GETDATE())";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID", reg.UserID);
                cmd.Parameters.AddWithValue("@EventID", reg.EventID);
                cmd.Parameters.AddWithValue("@NumberOfParticipants", reg.NumberOfParticipants);
                cmd.Parameters.AddWithValue("@SpecialRequests", (object)reg.SpecialRequests ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<EventRegistrationModel> GetUserRegistrations(int userId)
        {
            List<EventRegistrationModel> regs = new List<EventRegistrationModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT r.*, e.Title as EventTitle, e.EventDate 
                    FROM EventRegistrations r
                    INNER JOIN Events e ON r.EventID = e.EventID
                    WHERE r.UserID = @UserID
                    ORDER BY r.RegistrationDate DESC";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        regs.Add(new EventRegistrationModel
                        {
                            RegistrationID = Convert.ToInt32(reader["RegistrationID"]),
                            EventID = Convert.ToInt32(reader["EventID"]),
                            NumberOfParticipants = Convert.ToInt32(reader["NumberOfParticipants"]),
                            Status = reader["Status"].ToString(),
                            RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
                            EventTitle = reader["EventTitle"].ToString(),
                            EventDate = reader["EventDate"].ToString()
                        });
                    }
                }
            }
            return regs;
        }

        public List<EventRegistrationModel> GetAllRegistrations()
        {
            List<EventRegistrationModel> regs = new List<EventRegistrationModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT r.*, e.Title as EventTitle, u.FullName as UserFullName, u.Email as UserEmail
                    FROM EventRegistrations r
                    INNER JOIN Events e ON r.EventID = e.EventID
                    INNER JOIN Users u ON r.UserID = u.UserID
                    ORDER BY r.RegistrationDate DESC";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        regs.Add(new EventRegistrationModel
                        {
                            RegistrationID = Convert.ToInt32(reader["RegistrationID"]),
                            EventTitle = reader["EventTitle"].ToString(),
                            UserFullName = reader["UserFullName"].ToString(),
                            UserEmail = reader["UserEmail"].ToString(),
                            NumberOfParticipants = Convert.ToInt32(reader["NumberOfParticipants"]),
                            Status = reader["Status"].ToString(),
                            RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"])
                        });
                    }
                }
            }
            return regs;
        }

        public void UpdateRegistrationStatus(int registrationId, string status)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "UPDATE EventRegistrations SET Status = @Status WHERE RegistrationID = @RegistrationID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@RegistrationID", registrationId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public EventModel GetEventById(int eventId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = "SELECT * FROM Events WHERE EventID = @EventID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@EventID", eventId);
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                        return new EventModel
                        {
                            EventID       = Convert.ToInt32(r["EventID"]),
                            Title         = r["Title"].ToString(),
                            EventDate     = r["EventDate"].ToString(),
                            EventDuration = r["EventDuration"].ToString(),
                            Region        = r["Region"].ToString(),
                            Description   = r["Description"].ToString()
                        };
                }
            }
            return null;
        }

        public void UpdateEvent(EventModel evt)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"UPDATE Events SET Title=@Title, EventDate=@EventDate,
                    EventDuration=@EventDuration, Region=@Region, Description=@Description
                    WHERE EventID=@EventID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Title",         evt.Title);
                cmd.Parameters.AddWithValue("@EventDate",     evt.EventDate);
                cmd.Parameters.AddWithValue("@EventDuration", evt.EventDuration);
                cmd.Parameters.AddWithValue("@Region",        evt.Region);
                cmd.Parameters.AddWithValue("@Description",   evt.Description);
                cmd.Parameters.AddWithValue("@EventID",       evt.EventID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}