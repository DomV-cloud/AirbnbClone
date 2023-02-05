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

namespace Airbnb.Database.DAO.DAO_Location
{
    public class LocationDAO : IRepository<Location>
    {
        public string query;
        public void Delete(Location location)
        {
            query = @"DELETE [Location] WHERE id  = @id";
            try
            {
                if (location == null)
                {
                    throw new InvalidInput("Invalid input");
                }
                if (!IsServerConnected())
                {
                    throw new Exception("Server connection failed");
                }
                using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = location.Id;
                    conn.Open();
                    command.ExecuteNonQuery();

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

        public IEnumerable<Location> GetAll()
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
            query = @"SELECT * FROM [Location]";
            using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Location location = new Location
                            {
                                Id = reader.GetInt32(0),
                                Name_of_location = reader.GetString(1),
                                City = reader.GetString(2),
                                State = reader.GetString(3),
                                Info = reader.GetString(4),


                            };
                            yield return location;
                        }
                    }
                }
            }
        }

        public Location GetById(int id)
        {
            Location location = null;
            query = @"SELECT * FROM [Location] WHERE id = @Id";
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
                            location = new Location
                            {
                                Id = reader.GetInt32(0),
                                Name_of_location = reader.GetString(1),
                                City = reader.GetString(2),
                                State = reader.GetString(3),
                                Info = reader.GetString(4),
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
            return location;
        }

        public void Save(Location location)
        {
            try
            {
                if (location == null)
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
                        query = "INSERT INTO [Location] (name_of_location, city,state, info)" +
                             "VALUES (@name_of_location, @city, @state, @info);";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.Add("@name_of_location", SqlDbType.VarChar, 50).Value = location.Name_of_location;
                            command.Parameters.Add("@city", SqlDbType.VarChar, 50).Value = location.City;
                            command.Parameters.Add("@state", SqlDbType.VarChar, 50).Value = location.State;
                            command.Parameters.Add("@info", SqlDbType.VarChar, 250).Value = location.Info;
                            var returnedValue = command.ExecuteNonQuery();
                            location.Id = Convert.ToInt32(returnedValue);
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
