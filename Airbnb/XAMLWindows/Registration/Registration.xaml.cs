using Airbnb.Classes;
using Airbnb.Database.DAO;
using Airbnb.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Airbnb.XAMLWindows.Registration
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        UserDAO userDAO = new UserDAO();
        private Login.Login parent_Login;
        public Registration(Login.Login login)
        {
            InitializeComponent();
            this.parent_Login = login;
        }
       
       
        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsServerConnected())
                {
                    throw new ConnectionException("Server connection failed");
                }
                if (string.IsNullOrEmpty(NameBox.Text) || string.IsNullOrEmpty(SecondNameBox.Text) || string.IsNullOrEmpty(EmailBox.Text) || string.IsNullOrEmpty(PasswordBox.Password
                    ))
                {
                    throw new InvalidInput("All fields are required.");

                }
                if (userDAO.GetByMail(EmailBox.Text) != null)
                {
                    throw new UserAlreadyExists("User with this email already exists");
                }
                if (userDAO.ValidatePassword(PasswordBox.Password) )
                {
                    throw new WeakPassword("Password does not meet security requirements");
                }
                if (!userDAO.EmailContains(EmailBox.Text))
                {
                    throw new EmailInvalid("Email does not contain '@'");
                }
                userDAO.Save(new User(User.Type_Of_User.Costumer, NameBox.Text, SecondNameBox.Text,EmailBox.Text,PasswordBox.Password));
                this.Hide();
                this.parent_Login.Show();
                
                


            }
            catch (InvalidInput ex)
            {
                validation_Name.Text = ex.Message;
                validation_Name.Visibility = Visibility.Visible;

                validation_SecondName.Text = ex.Message;
                validation_SecondName.Visibility = Visibility.Visible;

                validation_Password.Text = ex.Message;
                validation_Password.Visibility = Visibility.Visible;

                validation_Email.Text = ex.Message;
                validation_Email.Visibility = Visibility.Visible;
            }
            catch (WeakPassword ex) 
            {
                validation_Password.Text = ex.Message;
                validation_Password.Visibility = Visibility.Visible;
            }
            catch (EmailInvalid ex)
            {
                validation_Email.Text = ex.Message;
                validation_Email.Visibility = Visibility.Visible;
            }
            catch (UserAlreadyExists ex)
            {
                validation_Email.Text = ex.Message;
                validation_Email.Visibility = Visibility.Visible;
            }
            catch (ConnectionException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Metoda,  která po kliknutí na tlačítko vratí uživatele na stranu pro Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.parent_Login.Show();
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
