using Airbnb.Classes;
using Airbnb.Database.Singleton;
using Airbnb.Exceptions;
using Airbnb.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;

namespace Airbnb.Database.DAO.DAO_Review
{
    public class DAOReview : IRepository<Review>
    {
        public string query;
        public void Delete(Review review)
        {
            query = @"DELETE [Review] WHERE id  = @id";
            try
            {
                if (review == null)
                {
                    throw new InvalidInput("Invalid input");
                }
                if (!IsServerConnected())
                {
                    throw new Exception("Server connection failed");
                }
                else
                {

                using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = review.Id;
                    conn.Open();
                    command.ExecuteNonQuery();

                }
                }
            }
            catch (InvalidInput e)
            {

                MessageBox.Show(e.Message);
            }
            catch (ConnectionException e)
            {

                MessageBox.Show(e.Message);
            }
        }

        public IEnumerable<Review> GetAll()
        {

            try
            {
                if (!IsServerConnected())
                {
                    throw new Exception("Server connection failed");
                }
            }
            catch (ConnectionException e)
            {

                MessageBox.Show(e.Message);
            }
            query = @"SELECT * FROM [Review]";
            using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Review review = new Review
                            {
                                Id = reader.GetInt32(0),
                                Rating = (double)reader.GetDecimal(1),
                                Comment = reader.GetString(2),
                                Fk_user = reader.GetInt32(3)
                               
                            };
                            yield return review;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Metoda, která vrací všechny Recenze podle ID bytu
        /// </summary>
        /// <param name="id">id bytu</param>
        /// <returns>Kolekci Recenzi</returns>
        public IEnumerable<Review> GetAllReviews(int id)
        {

            try
            {
                if (id <= 0 || id.GetType() != typeof(int))
                {
                    throw new InvalidInput("Invalid id input: " + id);
                }
                if (!IsServerConnected())
                {
                    throw new Exception("Server connection failed");
                }
            }
            catch (InvalidInput ex)
            {

                MessageBox.Show(ex.Message);
            }
            catch (ConnectionException e)
            {

                MessageBox.Show(e.Message);
            }
            query = @"SELECT * FROM Review INNER JOIN Apartment ON Review.id = Apartment.fk_apartment_review_id WHERE Apartment.id = @id;";
            using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conn.Open();
                    command.ExecuteNonQuery();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Review review = new Review
                            {
                                Id = reader.GetInt32(0),
                                Rating = (double)reader.GetDecimal(1),
                                Comment = reader.GetString(2),
                                Fk_user = reader.GetInt32(3)

                            };
                            yield return review;
                        }
                    }
                }
            }
        }


        public Review GetById(int id)
        {
            Review review = null;
            query = @"SELECT * FROM [Review] WHERE id = @Id";
            try
            {
                if (id <= 0 || id.GetType() != typeof(int))
                {
                    throw new InvalidInput("Invalid id input: " + id);
                }
                if (!IsServerConnected())
                {
                    throw new Exception("Server connection failed");
                }
                else
                {

                


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
                            review = new Review
                            {
                                Id = reader.GetInt32(0),
                                Rating = (double)reader.GetDecimal(1),
                                Comment = reader.GetString(2),
                                Fk_user = reader.GetInt32(3)
                            };
                        }
                        reader.Close();
                    }
                }
                }


            }
            catch (InvalidInput e)
            {

                MessageBox.Show(e.Message);

            }
            catch (ConnectionException e)
            {

                MessageBox.Show(e.Message);
            }
            return review;
        }

        public void Save(Review review)
        {
            try
            {
                if (review == null)
                {
                    throw new InvalidInput("Invalid input");
                }
                if (!IsServerConnected())
                {
                    throw new Exception("Server connection failed");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                    {
                        conn.Open();
                        query = "INSERT INTO [Review] (rating, comment, fk_user_id)" +
                             "VALUES (@rating, @comment,@fk_user_id);";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.Add("@rating", SqlDbType.Decimal).Value = review.Rating;
                            command.Parameters.Add("@comment", SqlDbType.VarChar, 250).Value = review.Comment;
                            command.Parameters.Add("@fk_user_id", SqlDbType.Int).Value = review.Fk_user;
                            var returnedValue = command.ExecuteNonQuery();
                            review.Id = Convert.ToInt32(returnedValue);
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
            catch (ConnectionException e)
            {

                MessageBox.Show(e.Message);
            }
        }
        
        public bool IsServerConnected()
        {
            try
            {
                using (var client = new Ping())
                {
                    var reply = client.Send("192.168.1.8");
                    if (reply.Status == IPStatus.Success)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public double GetAverageRating(int apartmentId)
        {
            double averageRating = 0;
            using (SqlConnection connection = new SqlConnection(DatabaseSingleton.connString))
            {
                connection.Open();
                string query = "SELECT AVG(r.rating) as AverageRating " +
                               "FROM Review as r " +
                               "INNER JOIN Apartment as a " +
                               "ON r.id = a.id " +
                               "WHERE a.id = @apartmentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@apartmentId", SqlDbType.Decimal).Value = apartmentId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("AverageRating")))
                            {
                                averageRating = Convert.ToDouble(reader["AverageRating"]);
                            }
                        }
                    }
                }
            }
            return averageRating;
        }

    }
}
