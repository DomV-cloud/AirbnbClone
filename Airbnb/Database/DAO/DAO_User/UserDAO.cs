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
using System.Text.RegularExpressions;
using System.Windows;
using static Airbnb.Classes.User;

namespace Airbnb.Database.DAO
{
    /// <summary>
    /// Třída vytvořená podle návrhového vzoru DAO.
    /// 
    /// Metody Delete(),Save(), GetALL(), GetById() jsou zdokumentované ve složce Interfaces/Repository/IRepository
    /// </summary>
    public class UserDAO : IRepository<User>
    {
        public string query = " ";

        public void Delete(User review)
        {
            query = @"DELETE [User] WHERE id  = @id";
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

        public IEnumerable<User> GetAll()
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
            query = @"SELECT * FROM [User]";
            using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Second_name = reader.GetString(2),
                                Email = reader.GetString(3),
                                Password = reader.GetString(4),
                                User_type = (Type_Of_User)Enum.ToObject(typeof(Type_Of_User), reader.GetInt32(5))
                            };
                            yield return user;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Metoda, která vrátí uživatele na základě jeho emailu
        /// </summary>
        /// <param name="email">email uživatele</param>
        /// <returns>Uživatele</returns>
        public User GetByMail(string email)
        {
            User user = null;
            query = @"SELECT * FROM [User] WHERE email = @email";
            try
            {
                if (string.IsNullOrEmpty(email)|| email.GetType() != typeof(string))
                {
                    throw new InvalidInput("Invalid email input: " + email);
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
                        param.ParameterName = "@email";
                        param.Value = email;

                        command.Parameters.Add(param);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Second_name = reader.GetString(2),
                                Email = reader.GetString(3),
                                Password = reader.GetString(4),
                                User_type = (Type_Of_User)Enum.ToObject(typeof(Type_Of_User), reader.GetInt32(5))
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
            return user;
        }

        public User GetById(int id)
        {
                User user = null;
                query = @"SELECT * FROM [User] WHERE id = @Id";
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
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Second_name = reader.GetString(2),
                                Email = reader.GetString(3),
                                Password = reader.GetString(4),
                                User_type = (Type_Of_User)Enum.ToObject(typeof(Type_Of_User), reader.GetInt32(5))
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
            return user;
        }

        public void Save(User user)
        {
            try
            {
                if (user == null)
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
                       query = "INSERT INTO [User] (name, second_name, email, password,fk_type_of_user_id)" +
                            "VALUES (@name, @second_name, @email, @password, @fk_type_of_user_id);";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = user.Name;
                            command.Parameters.Add("@second_name", SqlDbType.VarChar, 50).Value = user.Second_name;
                            command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = user.Email;
                            command.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = user.Password;
                            command.Parameters.Add("@fk_type_of_user_id", SqlDbType.Int, 50).Value = (int) user.User_type;
                            var returnedValue = command.ExecuteNonQuery();
                            user.Id = Convert.ToInt32(returnedValue);
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
        /// <summary>
        /// Metoda, která zjišťuje, zda zadané heslo v DB existuje. 
        /// </summary>
        /// <param name="password">Heslo uživatele</param>
        /// <returns>true nebo false</returns>
        public bool ExistPassword(string password)
        {
            query = @"SELECT * FROM [User] WHERE password = @password";
            bool passwordInvalid = true;


            using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                    {
                        conn.Open();
                        using (SqlCommand passwordCommand = new SqlCommand(query, conn))
                        {
                            passwordCommand.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = password;
                            SqlDataReader passwordReader = passwordCommand.ExecuteReader();
                            if (!passwordReader.HasRows)
                            {
                                passwordInvalid = false;
                            }
                            passwordReader.Close();
                        }

                    }
                
            return passwordInvalid;

        }
        /// <summary>
        /// Metoda, která zjišťuje, zda zadaný email v DB existuje
        /// </summary>
        /// <param name="email">email uživatele</param>
        /// <returns>true nebo false</returns>
        public bool ExistEmail(string email)
        {
            query = @"SELECT * FROM [User] WHERE email = @email";
            bool emailInvalid = true;
          
               
                using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                {
                    conn.Open();
                    using (SqlCommand emailCommand = new SqlCommand(query, conn))
                    {
                        emailCommand.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                        SqlDataReader emailReader = emailCommand.ExecuteReader();
                        if (!emailReader.HasRows)
                        {
                                emailInvalid = false;
                        }
                        emailReader.Close();
                    }

                }

            return emailInvalid;
        }
        /// <summary>
        ///  Metoda, která zjišťuje, zda daný uživatel skutečně existuje v DB
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>Uživatele nebo NULL</returns>
        public User UserLogin(string email, string password)
        {
            query = @"SELECT * FROM [User] WHERE email = @email AND password = @password";

            User user = null;
            try
            {
                    using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                {
                    if (IsServerConnected())
                    {

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            conn.Open();
                            command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                            command.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = password;
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                user = new User
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Second_name = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Password = reader.GetString(4),
                                    User_type = (Type_Of_User)Enum.ToObject(typeof(Type_Of_User), reader.GetInt32(5))
                                };
                            }
                            reader.Close();
                        }
                    }
                    else
                    {
                        throw new ConnectionException("Server has failed");
                    }
                
                }




            }
            catch (ConnectionException e)
            {

                MessageBox.Show(e.Message);

            }
            return user;
        }
        /// <summary>
        /// Metoda, která aktualizuje atributy uživatele
        /// </summary>
        /// <param name="name">jméno uživatele</param>
        /// <param name="second">příjmení uživatele</param>
        /// <param name="email">email uživatele</param>
        /// <param name="password">heslo uživatele</param>
        public void UpdateUser(string name, string second, string email, string password)
        {

            try
            {
                if (!IsServerConnected())
                {
                    throw new ConnectionException("Server has failed");
                }
                else
                {
                    query = @"UPDATE User SET name = @name, second_name = @second, email = @email, password = @password";

                    using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
                            command.Parameters.Add("@second_name", SqlDbType.VarChar, 50).Value = second;
                            command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                            command.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = password;
                            var returnedValue = command.ExecuteNonQuery();
                            MessageBox.Show("Succesfully updated");
                        }
                    }
                }
            }
            catch (ConnectionException e)
            {

                MessageBox.Show(e.Message);
            }
          
        }

        /// <summary>
        /// Metoda, která kontroluje, zda uživatel zadal dostatečně silné heslo
        /// Podmínky:
        /// 
        ///  Obsahuje alespoň jedno malé písmeno (?=.[a-z])
        ///  Obsahuje alespoň jedno velké písmeno (?=.[A-Z])
        ///  Obsahuje alespoň jednu číslice (?=.\d)
        ///  Obsahuje alespoň jeden speciální znak (?=.[#$^+=!*()@%&])
        ///  Délka hesla je alespoň 8 znaků {8,}
        ///  
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidatePassword(string password)
        {
            string pattern = @"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[#$^+=!*()@%&]).{8,}$";
            if (Regex.IsMatch(password, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Metoda kontroluje, zda emailová adresa skutečně obsahuje znak '@'
        /// </summary>
        /// <param name="email">emailová adresa, která se má zkontrolovat</param>
        /// <returns>true nebo false</returns>
        public bool EmailContains(string email)
        {

            if (email.Contains('@'))
            {
                return true;
            }
            else
            {
                return false;
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
