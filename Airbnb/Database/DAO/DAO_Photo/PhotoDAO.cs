using Airbnb.Classes;
using Airbnb.Database.Singleton;
using Airbnb.Exceptions;
using Airbnb.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace Airbnb.Database.DAO.DAO_Photo
{
    public class PhotoDAO : IRepository<Photo>
    {
        public string query;
        public void Delete(Photo photo)
        {
            query = @"DELETE [User] WHERE id  = @id";
            try
            {
                if (photo == null)
                {
                    throw new InvalidInput("Invalid input");
                }
                using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = photo.Id;
                    conn.Open();
                    command.ExecuteNonQuery();

                }
            }
            catch (InvalidInput e)
            {

                MessageBox.Show(e.Message);
            }
        }

        public IEnumerable<Photo> GetAll()
        {
            query = @"SELECT * FROM [Photo]";
            using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Photo photo = new Photo
                            {
                                Id = reader.GetInt32(0),
                                Name_of_photo = reader.GetString(1),
                                Fk_author = reader.GetInt32(2),
                                Day_of_acqusition = reader.GetDateTime(3),
                                Photo_path = reader.GetString(4)
                               
                            };
                            yield return photo;
                        }
                    }
                }
            }
        }

        public Photo GetById(int id)
        {
            Photo photo = null;
            query = @"SELECT * FROM [Photo] WHERE id = @Id";
            try
            {
                if (id <= 0 || id.GetType() != typeof(int))
                {
                    throw new InvalidInput("Invalid input");
                }


                using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                {

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@Id";
                        param.Value = id;

                        command.Parameters.Add(param);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            photo = new Photo
                            {
                                Id = reader.GetInt32(0),
                                Name_of_photo = reader.GetString(1),
                                Fk_author = reader.GetInt32(2),
                                Day_of_acqusition = reader.GetDateTime(3),
                                Photo_path = reader.GetString(4)


                            };
                        }
                        reader.Close();
                    }
                }


            }
            catch (InvalidInput e)
            {

                MessageBox.Show(e.Message);

            }
            return photo;
        }

        public bool IsServerConnected()
        {
            throw new NotImplementedException();
        }

        public void Save(Photo photo)
        {
            try
            {
                if (photo== null)
                {
                    throw new InvalidInput("Invalid input");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                    {
                        conn.Open();
                        query = "INSERT INTO [Photo] (name_of_photo, fk_author_user_id,day_of_acqusittion, photo_path)" +
                             "VALUES (@name_of_photo, @fk_author_user_id, @day_of_acqusittion, @photo_path);";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.Add("@name_of_photo", SqlDbType.VarChar, 50).Value = photo.Name_of_photo;
                            command.Parameters.Add("@fk_author_user_id", SqlDbType.Int).Value = photo.Fk_author;
                            command.Parameters.Add("@day_of_acqusittion", SqlDbType.DateTime).Value = photo.Day_of_acqusition;
                            command.Parameters.Add("@photo_path", SqlDbType.VarChar).Value = photo.Photo_path;

                            var returnedValue = command.ExecuteNonQuery();
                            photo.Id = Convert.ToInt32(returnedValue);
                            MessageBox.Show("Saved!");

                        }
                        conn.Close();
                    }
                }
            }
            catch (InvalidInput e)
            {

                MessageBox.Show(e.Message);
            }
        }
    }
}
