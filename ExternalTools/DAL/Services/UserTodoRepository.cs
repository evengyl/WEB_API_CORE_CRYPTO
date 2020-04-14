using DAL.IRepositories;
using DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class UserTodoRepository : IRepository<UserTodo>
    {
        // string connString = ConfigurationManager.ConnectionStrings["DemoEFCore"].ConnectionString;
        string connString = null;
        private SqlConnection connection;
        public UserTodoRepository()
        {
            connString = @"Data Source=DESKTOP-D0PIH1A\EVENGYL_SQL_SERV;Initial Catalog=Demo_todo_mvvm_wpf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            connection = new SqlConnection(connString);
            connection.Open();

        }


        public List<UserTodo> GetAll()
        {
            List<UserTodo> ListUser = new List<UserTodo>();

            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM [user]";

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListUser.Add(new UserTodo
                        {
                            Id = (int)reader["id"],
                            Login = (string)reader["login"],
                            Name = (string)reader["name"],
                            LastName = (string)reader["lastName"]
                        });
                    }
                    return ListUser;
                }
            }
        }
        public UserTodo Get(int user_id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM [user] WHERE id = @id";
                commander.Parameters.AddWithValue("id", user_id);


                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserTodo()
                        {
                            Id = (int)reader["id"],
                            Login = (string)reader["login"],
                            Name = (string)reader["name"],
                            LastName = (string)reader["lastName"]
                        };
                    }
                    else
                    {
                        return new UserTodo();
                    }
                }
            }
        }


        public UserTodo Login(UserTodo user)
        {
            using (SqlCommand commander = new SqlCommand("SP_LoginUser", connection))
            {
                commander.CommandType = CommandType.StoredProcedure;

                commander.Parameters.AddWithValue("login", user.Login);
                commander.Parameters.AddWithValue("password", user.Password);


                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserTodo()
                        {
                            Id = (int)reader["id"],
                            Login = (string)reader["login"],
                            Name = (string)reader["name"],
                            LastName = (string)reader["lastName"]
                        };
                    }
                    else
                    {
                        return new UserTodo();
                    }
                }
            }
        }


        

        public UserTodo GetBy(string row, string value)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM [user] WHERE @row = @value";
                commander.Parameters.AddWithValue("row", row);
                commander.Parameters.AddWithValue("value", value);

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserTodo()
                        {
                            Login = (string)reader["Login"],
                        };
                    }
                    else
                    {
                        return new UserTodo();
                    }
                }
            }
        }



        public void Create(UserTodo user)
        {
            using (SqlCommand commander = new SqlCommand("SP_RegisterUser", connection))
            {
                commander.CommandType = CommandType.StoredProcedure;

                commander.Parameters.AddWithValue("login", user.Login);
                commander.Parameters.AddWithValue("password", user.Password);
                commander.Parameters.AddWithValue("name", user.Name);
                commander.Parameters.AddWithValue("lastName", user.LastName);

                commander.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "UPDATE [User] SET Active = False WHERE id = @Id";
                commander.Parameters.AddWithValue("Id", id);
                commander.ExecuteNonQuery();
            }
        }

        public void Update(UserTodo user)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "UPDATE [user] SET login = @Login, password = @Password, name = @Name, lastName = @LastName " +
                                    "WHERE id = @Id";
                commander.Parameters.AddWithValue("Login", user.Login);
                commander.Parameters.AddWithValue("Password", user.Password);
                commander.Parameters.AddWithValue("Name", user.Name);
                commander.Parameters.AddWithValue("LastName", user.LastName);
                commander.Parameters.AddWithValue("Id", user.Id);

                commander.ExecuteNonQuery();
            }
        }



    }
}
