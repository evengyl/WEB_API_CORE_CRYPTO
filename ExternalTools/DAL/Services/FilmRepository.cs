using DAL.IRepositories;
using DAL.Models;


using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Services
{
    public class FilmRepository : IRepository<Film>
    {
        // string connString = ConfigurationManager.ConnectionStrings["DemoEFCore"].ConnectionString;
        string connString = null;
        private SqlConnection connection;
        public FilmRepository()
        {
            connString = @"Data Source=DESKTOP-D0PIH1A\EVENGYL_SQL_SERV;Initial Catalog=DemoEFCore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            connection = new SqlConnection(connString);
            connection.Open();

        }


        public List<Film> GetAll()
        {
            List<Film> ListFilm = new List<Film>();

            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM Film";

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListFilm.Add(new Film
                        {
                            Id = (int)reader["Id"],
                            Titre = (string)reader["Titre"],
                            AnneeSortie = (int)reader["AnneeSortie"],
                            Genre = (string)reader["genre"]
                        });
                    }

                    return ListFilm;
                }
            }
        }
        public Film Get(int film_id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM Film WHERE Id = @id";
                SqlParameter Param_id = new SqlParameter
                {
                    ParameterName = "id",
                    Value = film_id
                }; commander.Parameters.Add(Param_id);

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Film()
                        {
                            Id = (int)reader["Id"],
                            Titre = (string)reader["Titre"],
                            AnneeSortie = (int)reader["AnneeSortie"],
                            Genre = (string)reader["genre"]
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        public Film GetBy(string row, string value)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "SELECT * FROM Film WHERE @row = @value";
                SqlParameter Param_row = new SqlParameter
                {
                    ParameterName = "row",
                    Value = row
                }; commander.Parameters.Add(Param_row);


                SqlParameter Param_value = new SqlParameter
                {
                    ParameterName = "value",
                    Value = value
                }; commander.Parameters.Add(Param_value);

                using (SqlDataReader reader = commander.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Film()
                        {
                            Id = (int)reader["Id"],
                            Titre = (string)reader["Titre"],
                            AnneeSortie = (int)reader["AnneeSortie"],
                            Genre = (string)reader["genre"]
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }



        public void Create(Film film)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "INSERT INTO Film (Titre, AnneeSortie, Genre) " +
                                  "VALUES (@Titre, @AnneeSortie, @Genre)";
                commander.Parameters.AddWithValue("Titre", film.Titre);
                commander.Parameters.AddWithValue("AnneeSortie", film.AnneeSortie);
                commander.Parameters.AddWithValue("Genre", film.Genre);

                commander.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "DELETE FROM Film WHERE Id = @Id";
                commander.Parameters.AddWithValue("Id", id);

                commander.ExecuteNonQuery();
            }
        }

        public void Update(Film film)
        {
            using (SqlCommand commander = connection.CreateCommand())
            {
                commander.CommandText = "UPDATE Film SET Titre = @Titre, AnneeSortie = @AnneeSortie, Genre = @Genre " +
                                  "WHERE Id = @Id";
                commander.Parameters.AddWithValue("Titre", film.Titre);
                commander.Parameters.AddWithValue("AnneeSortie", film.AnneeSortie);
                commander.Parameters.AddWithValue("Genre", film.Genre);
                commander.Parameters.AddWithValue("Id", film.Id);

                commander.ExecuteNonQuery();
            }
        }



    }
}
