using Airbnb.Classes;
using Airbnb.Database.DAO;
using Airbnb.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Airbnb.XAMLWindows;
using Airbnb.XAMLWindows.Registration;
using System.Net.NetworkInformation;

namespace Airbnb.XAMLWindows.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        UserDAO userDAO = new UserDAO();
        public Login()
        {
            InitializeComponent();
        }
       
        
        /// <summary>
        /// Metoda, která kontroluje Login uživatele, pokud jsou údajé nesprávné nebo uživatel neexistuje, je informován
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsServerConnected())
                {
                    throw new ConnectionException("Server connection failed");
                }
                if (!userDAO.ExistEmail(EmailBox.Text) || !userDAO.ExistPassword(PasswordBox.Password))
                {
                    throw new UserInvalid("Invalid Email or Password");
                }
                if (userDAO.UserLogin(EmailBox.Text, PasswordBox.Password) == null)
                {
                    throw new UserDoesNotExist("Login failed");
                    EmailBox.Text = "";
                    PasswordBox.Password = "";
                    validation_Email.Visibility = Visibility.Hidden;
                    validation_Password.Visibility = Visibility.Hidden;
                }
                else
                {
                    MainWindow mainWindow = new MainWindow(userDAO.GetByMail(EmailBox.Text));
                    mainWindow.Show();
                    this.Hide();
                    MessageBox.Show("Welcome" + " " + userDAO.GetByMail(EmailBox.Text).Name );
                }
            }
            catch (UserInvalid ex)
            {
                if (!userDAO.ExistEmail(EmailBox.Text))
                {
                    validation_Email.Text = "* Your email is incorrect";
                    validation_Email.Visibility = Visibility.Visible;
                }
                if (!userDAO.ExistPassword(PasswordBox.Password))
                {
                    validation_Password.Text = "* Your password is incorrect";
                    validation_Password.Visibility = Visibility.Visible;
                }
            }
            catch (UserDoesNotExist ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ConnectionException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            Registration.Registration registration = new Registration.Registration(this);
            registration.Show();
            this.Hide();
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
