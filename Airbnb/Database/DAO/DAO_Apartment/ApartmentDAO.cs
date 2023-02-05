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

namespace Airbnb.Database.DAO.DAO_Apartment
{
    public class ApartmentDAO : IRepository<Apartment>
    {
        public string query;
        public void Delete(Apartment apartment)
        {
            query = @"DELETE [Aparment] WHERE id  = @id";
            try
            {
                if (apartment == null)
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
                    command.Parameters.Add("@id", SqlDbType.Int).Value = apartment.Id;
                    conn.Open();
                    command.ExecuteNonQuery();

                }
            }
                }
            catch (InvalidInput e)
            {

                MessageBox.Show(e.Message);
            }
            catch (ConnectionException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public IEnumerable<Apartment> GetAll()
        {

            try
            {
                if (!IsServerConnected())
                {
                    throw new Exception("Server connection failed");
                }
            }
            catch (ConnectionException ex)
            {

                MessageBox.Show(ex.Message);
            }
            query = @"SELECT * FROM [Apartment]";
            using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Apartment apartment = new Apartment
                            {
                                Id = reader.GetInt32(0),
                                Name_apartment = reader.GetString(1),
                                Fk_apartment_photo_id = reader.GetInt32(2),
                                Fk_apartment_location_id = reader.GetInt32(3),
                                Size = (double) reader.GetDecimal(4),
                                Apartment_price = (double)reader.GetDecimal(5),
                                Fk_apartment_review_id = reader.GetInt32(6),
                                Apartment_info = reader.GetString(7),
                                Number_of_guests = reader.GetInt32(8),
                                Number_of_bedroom = reader.GetInt32(9),
                                Number_of_beds = reader.GetInt32(10),
                                Number_of_bathrooms= reader.GetInt32(11),
                                Fk_apartment_user_owner_id = reader.GetInt32(12)

                            };
                            yield return apartment;
                        }
                    }
                }
            }
        }

        

        public Apartment GetById(int id)
        {
            Apartment apartment = null;
            query = @"SELECT * FROM [Apartment] WHERE id = @Id";
            try
            {
                if (id <= 0 || id.GetType() != typeof(int))
                {
                    throw new InvalidInput("Invalid input");
                }


                if (!IsServerConnected())
                {
                    throw new ConnectionException("Server connection failed");
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
                            apartment = new Apartment
                            {
                                Id = reader.GetInt32(0),
                                Name_apartment = reader.GetString(1),
                                Fk_apartment_photo_id = reader.GetInt32(2),
                                Fk_apartment_location_id = reader.GetInt32(3),
                                Size = (double)reader.GetDecimal(4),
                                Apartment_price = (double)reader.GetDecimal(5),
                                Fk_apartment_review_id = reader.GetInt32(6),
                                Apartment_info = reader.GetString(7),
                                Number_of_guests = reader.GetInt32(8),
                                Number_of_bedroom = reader.GetInt32(9),
                                Number_of_beds = reader.GetInt32(10),
                                Number_of_bathrooms = reader.GetInt32(11),
                                Fk_apartment_user_owner_id = reader.GetInt32(12)

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
            return apartment;
        }

        public void Save(Apartment apartment)
        {
            try
            {
                if (apartment == null)
                {
                    throw new InvalidInput("Invalid input");
                }
                if (!IsServerConnected())
                {
                    throw new ConnectionException("Server connection failed");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                    {
                        conn.Open();
                        query = "INSERT INTO [Apartment] (name_apartment,fk_apartment_photo_id,fk_apartment_location_id, size, apartment_price, " +
                            "fk_apartment_review_id, apartment_info, number_of_guests,  number_of_bedroom,number_of_beds, number_of_bathrooms, fk_apartment_user_owner_id    )" +
                             "VALUES (@name_apartment, @fk_apartment_photo, @fk_apartment_location_id, @size, " +
                             "@apartment_price, @fk_apartment_review_id, @apartment_info, @number_of_guests, " +
                             "@number_of_bedroom,@number_of_beds,@number_of_bathrooms, @fk_apartment_user_owner_id );";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.Add("@name_apartment", SqlDbType.VarChar, 50).Value = apartment.Name_apartment;
                            command.Parameters.Add("@fk_apartment_photo", SqlDbType.Int).Value = apartment.Fk_apartment_photo_id;
                            command.Parameters.Add("@fk_apartment_location_id", SqlDbType.Int).Value = apartment.Fk_apartment_location_id;
                            command.Parameters.Add("@size", SqlDbType.Decimal).Value = apartment.Size;
                            command.Parameters.Add("@apartment_price", SqlDbType.Decimal).Value = apartment.Apartment_price;
                            command.Parameters.Add("@fk_apartment_review_id", SqlDbType.Int).Value = apartment.Fk_apartment_review_id;
                            command.Parameters.Add("@apartment_info", SqlDbType.VarChar, 2000).Value = apartment.Apartment_info;
                            command.Parameters.Add("@number_of_guests", SqlDbType.Int).Value = apartment.Number_of_guests;
                            command.Parameters.Add("@number_of_bedroom", SqlDbType.Int).Value = apartment.Number_of_bedroom;
                            command.Parameters.Add("@number_of_beds", SqlDbType.Int).Value = apartment.Number_of_beds;
                            command.Parameters.Add("@number_of_bathrooms", SqlDbType.Int).Value = apartment.Number_of_bathrooms;
                            command.Parameters.Add("@fk_apartment_user_owner_id", SqlDbType.Int).Value = apartment.Fk_apartment_user_owner_id;



                            var returnedValue = command.ExecuteNonQuery();
                            apartment.Id = Convert.ToInt32(returnedValue);
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
            catch (ConnectionException ex)
            {

                MessageBox.Show(ex.Message);
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
        /// <summary>
        /// Metoda, která vrací kolekci všech bytu v dané oblasti
        /// </summary>
        /// <param name="state">stát, ve kterém se byt nachází</param>
        /// <returns>kolekci bytů</returns>
        public IEnumerable<Apartment> GetAllByLocation(string state)
        {

            try
            {
                if (!IsServerConnected())
                {
                    throw new Exception("Server connection failed");
                }
            }
            catch (ConnectionException ex)
            {

                MessageBox.Show(ex.Message);
            }
            query = @"SELECT *
                            FROM Apartment a
                            INNER JOIN [Location] l
                            ON a.fk_apartment_location_id = l.id
                            WHERE l.[state] = @state;";

            using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    command.Parameters.Add("@state", SqlDbType.VarChar).Value = state;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Apartment apartment = new Apartment
                            {
                                Id = reader.GetInt32(0),
                                Name_apartment = reader.GetString(1),
                                Fk_apartment_photo_id = reader.GetInt32(2),
                                Fk_apartment_location_id = reader.GetInt32(3),
                                Size = (double)reader.GetDecimal(4),
                                Apartment_price = (double)reader.GetDecimal(5),
                                Fk_apartment_review_id = reader.GetInt32(6),
                                Apartment_info = reader.GetString(7),
                                Number_of_guests = reader.GetInt32(8),
                                Number_of_bedroom = reader.GetInt32(9),
                                Number_of_beds = reader.GetInt32(10),
                                Number_of_bathrooms = reader.GetInt32(11),
                                Fk_apartment_user_owner_id = reader.GetInt32(12)

                            };
                            yield return apartment;
                        }
                    }
                }
            }
        }




    }
}
