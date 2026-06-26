using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CyberBotApp
{
    public class DatabaseHelper
    {
        private string connectionString =
            "server=localhost;database=CyberBotDB;uid=root;pwd=;";

        // ADD TASK
        public void AddTask(CyberTask task)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Tasks
                                (Title, Description, ReminderDate, IsCompleted)
                                VALUES (@title, @desc, @date, @done)";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@desc", task.Description);
                cmd.Parameters.AddWithValue("@date", task.ReminderDate);
                cmd.Parameters.AddWithValue("@done", task.IsCompleted);

                cmd.ExecuteNonQuery();
            }
        }

        // GET TASKS
        public List<CyberTask> GetTasks()
        {
            List<CyberTask> tasks = new List<CyberTask>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Tasks";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new CyberTask
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        ReminderDate = reader["ReminderDate"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["ReminderDate"]),
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
                    });
                }
            }

            return tasks;
        }

        // DELETE TASK
        public void DeleteTask(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM Tasks WHERE Id=@id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

        // COMPLETE TASK
        public void CompleteTask(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Tasks SET IsCompleted = 1 WHERE Id=@id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
