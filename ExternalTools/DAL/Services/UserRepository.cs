using DAL.IRepositories;
using DAL.Models;

using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Services
{
    //ma classe film repository depends de l'interface repository générique de type TEntity, ici ce sera film passer en param
    //cette classe devra donc implémenté tout les prop et method de l'interface
    //cette interface défini idéalement la liste des méthode dispo pour cette calsse de donnée
    public class UserRepository : IRepository<User>
    {
        // string connString = ConfigurationManager.ConnectionStrings["DemoEFCore"].ConnectionString;
        string connString = null;
        private SqlConnection connection;
        public UserRepository()
        {
            connString = @"Data Source=DESKTOP-D0PIH1A\EVENGYL_SQL_SERV;Initial Catalog=DemoEFCore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            connection = new SqlConnection(connString);
            connection.Open();

        }


        public List<User> GetAll()
        {
            List<User> ListUser = new List<User>();

            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM [User]";

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListUser.Add(new User
                        {
                            Id = (int)reader["Id"],
                            Login = (string)reader["Login"],
                            Password = (string)reader["Password"],
                            Active = (bool)reader["Active"],
                            Role = (string)reader["role"]
                        });
                    }

                    return ListUser;
                }
            }
        }
        public User Get(int user_id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM [User] WHERE Id = @id";
                SqlParameter Param_id = new SqlParameter
                {
                    ParameterName = "id",
                    Value = user_id
                }; commander.Parameters.Add(Param_id);

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User()
                        {
                            Id = (int)reader["Id"],
                            Login = (string)reader["Login"],
                            Password = (string)reader["Password"],
                            Active = (bool)reader["Active"],
                            Role = (string)reader["role"]
                        };
                    }
                    else
                    {
                        return new User();
                    }
                }
            }
        }


        public User GetBy(string row, string value)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM [User] WHERE @row = @value";
                commander.Parameters.AddWithValue("row", row);
                commander.Parameters.AddWithValue("value", value);

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User()
                        {
                            Id = (int)reader["Id"],
                            Login = (string)reader["Login"],
                            Password = (string)reader["Password"],
                            Active = (bool)reader["Active"],
                            Role = (string)reader["role"]
                        };
                    }
                    else
                    {
                        return new User();
                    }
                }
            }
        }



        public void Create(User user)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "INSERT INTO [User] (Login, Password, Active, Role) " +
                                  "VALUES (@Login, @Password, @Active, @Role)";
                commander.Parameters.AddWithValue("Login", user.Login);
                commander.Parameters.AddWithValue("Password", user.Password);
                commander.Parameters.AddWithValue("Active", user.Active);
                commander.Parameters.AddWithValue("Role", "user");

                commander.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "UPDATE [User] SET Active = False WHERE Id = @Id";
                commander.Parameters.AddWithValue("Id", id);
                commander.ExecuteNonQuery();
            }
        }

        public void Update(User user)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "UPDATE [User] SET Login = @Login, Password = @Password, Active = @Active, Role = @Role " +
                                  "WHERE Id = @Id";
                commander.Parameters.AddWithValue("Login", user.Login);
                commander.Parameters.AddWithValue("Password", user.Password);
                commander.Parameters.AddWithValue("Active", user.Active);
                commander.Parameters.AddWithValue("Role", user.Role);
                commander.Parameters.AddWithValue("Id", user.Id);

                commander.ExecuteNonQuery();
            }
        }


        public void SetUser(int id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "UPDATE [User] SET Role = @Role " +
                                  "WHERE Id = @Id";
                commander.Parameters.AddWithValue("Role", "user");
                commander.Parameters.AddWithValue("Id", id);

                commander.ExecuteNonQuery();
            }
        }

        public void SetAdmin(int id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "UPDATE [User] SET Role = @Role " +
                                  "WHERE Id = @Id";
                commander.Parameters.AddWithValue("Role", "admin");
                commander.Parameters.AddWithValue("Id", id);

                commander.ExecuteNonQuery();
            }
        }



    }
}
