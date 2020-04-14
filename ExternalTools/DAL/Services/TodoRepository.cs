using DAL.IRepositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class TodoRepository : IRepository<Todo>
    {
        // string connString = ConfigurationManager.ConnectionStrings["DemoEFCore"].ConnectionString;
        string connString = null;
        private SqlConnection connection;
        public TodoRepository()
        {
            connString = @"Data Source=DESKTOP-D0PIH1A\EVENGYL_SQL_SERV;Initial Catalog=Demo_todo_mvvm_wpf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            connection = new SqlConnection(connString);
            connection.Open();

        }


        public List<Todo> GetAll()
        {
            List<Todo> ListTodo = new List<Todo>();

            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM Todo";

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListTodo.Add(new Todo
                        {
                            id = (int)reader["id"],
                            Titre = (string)reader["Titre"],
                            Description = (string)reader["Description"],
                            Date = (DateTime)reader["Date"],
                            Done = (bool)reader["Done"],
                            user_id = (int)reader["user_id"]
                        });
                    }

                    return ListTodo;
                }
            }
        }

        public List<Todo> GetAll(int user_id)
        {
            List<Todo> ListTodo = new List<Todo>();

            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM Todo WHERE user_id = @user_id";
                commander.Parameters.AddWithValue("user_id", user_id);

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListTodo.Add(new Todo
                        {
                            id = (int)reader["id"],
                            Titre = (string)reader["Titre"],
                            Description = (string)reader["Description"],
                            Date = (DateTime)reader["Date"],
                            Done = (bool)reader["Done"],
                            user_id = (int)reader["user_id"]
                        });
                    }

                    return ListTodo;
                }
            }
        }


        public Todo Get(int user_id, int todo_id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM Todo WHERE id = @id AND user_id = @user_id";
                commander.Parameters.AddWithValue("id", todo_id);
                commander.Parameters.AddWithValue("user_id", user_id);

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Todo()
                        {
                            id = (int)reader["id"],
                            Titre = (string)reader["Titre"],
                            Description = (string)reader["Description"],
                            Date = (DateTime)reader["Date"],
                            Done = (bool)reader["Done"],
                            user_id = (int)reader["user_id"]
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        public Todo GetBy(string row, string value)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM Todo WHERE @row = @value";
                commander.Parameters.AddWithValue("row", row);
                commander.Parameters.AddWithValue("value", value);


                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Todo()
                        {
                            id = (int)reader["id"],
                            Titre = (string)reader["Titre"],
                            Description = (string)reader["Description"],
                            Date = (DateTime)reader["Date"],
                            Done = (bool)reader["Done"],
                            user_id = (int)reader["user_id"]
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }



        public void Create(Todo todo)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "INSERT INTO Todo (Titre, Description, Date, Done, user_id) " +
                                  "VALUES (@Titre, @Description, @Date, @Done, @UserId)";
                commander.Parameters.AddWithValue("Titre", todo.Titre);
                commander.Parameters.AddWithValue("Description", todo.Description);
                commander.Parameters.AddWithValue("Date", todo.Date);
                commander.Parameters.AddWithValue("Done", todo.Done);
                commander.Parameters.AddWithValue("UserID", todo.user_id);

                commander.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "DELETE FROM Todo WHERE id = @Id";
                commander.Parameters.AddWithValue("Id", id);

                commander.ExecuteNonQuery();
            }
        }

        public void Update(Todo todo)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "UPDATE Todo SET Titre = @Titre, Description = @Description, Date = @Date, Done = @Done, user_id = @UserId " +
                                  "WHERE id = @Id";
                commander.Parameters.AddWithValue("Titre", todo.Titre);
                commander.Parameters.AddWithValue("Description", todo.Description);
                commander.Parameters.AddWithValue("Date", todo.Date);
                commander.Parameters.AddWithValue("Done", todo.Done);
                commander.Parameters.AddWithValue("UserId", todo.user_id);
                commander.Parameters.AddWithValue("Id", todo.id);

                commander.ExecuteNonQuery();
            }
        }

        public Todo Get(int id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM Todo WHERE id = @id";
                commander.Parameters.AddWithValue("id", id);

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Todo()
                        {
                            id = (int)reader["id"],
                            Titre = (string)reader["Titre"],
                            Description = (string)reader["Description"],
                            Date = (DateTime)reader["Date"],
                            Done = (bool)reader["Done"],
                            user_id = (int)reader["user_id"]
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
