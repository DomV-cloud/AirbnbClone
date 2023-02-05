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

namespace Airbnb.Database.DAO.DAO_Reservation
{
    public class DAOReservation : IRepository<Reservation>
    {
        public string query;
        public void Delete(Reservation reservation)
        {
            query = @"DELETE [Reservation] WHERE id  = @id";
            try
            {
                if (reservation == null)
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
                    command.Parameters.Add("@id", SqlDbType.Int).Value = reservation.Id;
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

        public IEnumerable<Reservation> GetAll()
        {
            query = @"SELECT * FROM [Reservation]";
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
            using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reservation reservation = new Reservation
                            {
                                Id = reader.GetInt32(0),
                                Fk_apartment_id = reader.GetInt32(1),
                                Fk_apartment_costumer_id = reader.GetInt32(2),
                                Reservation_since = reader.GetDateTime(3),
                                Reservation_to = reader.GetDateTime(4)
                               

                            };
                            yield return reservation;
                        }
                    }
                }
            }
        }

        public Reservation GetById(int id)
        {
            Reservation reservation = null;
            query = @"SELECT * FROM [Reservation] WHERE id = @Id";
            try
            {
                if (id <= 0 || id.GetType() != typeof(int))
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
                            reservation = new Reservation
                            {
                                Id = reader.GetInt32(0),
                                Fk_apartment_id = reader.GetInt32(1),
                                Fk_apartment_costumer_id = reader.GetInt32(2),
                                Reservation_since = reader.GetDateTime(3),
                                Reservation_to = reader.GetDateTime(4)

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
            return reservation;
        }

        public void Save(Reservation reservation)
        {
            try
            {
                if (reservation == null)
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
                        query = "INSERT INTO [Reservation] (fk_apartment_id, fk_apartment_costumer_id, reservation_since, reservation_to) " +
                            "VALUES(@fk_apartment_id,@fk_apartment_costumer_id, @reservation_since,@reservation_to  )";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.Add("@fk_apartment_id", SqlDbType.Int).Value = reservation.Fk_apartment_id;
                            command.Parameters.Add("@fk_apartment_costumer_id", SqlDbType.Int).Value = reservation.Fk_apartment_id;
                            command.Parameters.Add("@reservation_since", SqlDbType.Date).Value = reservation.Reservation_since;
                            command.Parameters.Add("@reservation_to", SqlDbType.Date).Value = reservation.Reservation_to;
                            var returnedValue = command.ExecuteNonQuery();
                            reservation.Id = Convert.ToInt32(returnedValue);
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
    }
}
