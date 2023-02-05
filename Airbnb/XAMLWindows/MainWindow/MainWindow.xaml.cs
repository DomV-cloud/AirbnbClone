using Airbnb.Classes;
using Airbnb.Database.DAO;
using Airbnb.Database.DAO.DAO_Apartment;
using Airbnb.Database.DAO.DAO_Location;
using Airbnb.Database.DAO.DAO_Photo;
using Airbnb.Database.DAO.DAO_Reservation;
using Airbnb.Database.DAO.DAO_Review;
using Airbnb.Database.Singleton;
using Airbnb.Exceptions;
using Airbnb.XAMLWindows.MoreInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Airbnb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database.DAO.DAO_Apartment.ApartmentDAO apartmentDAO = new Database.DAO.DAO_Apartment.ApartmentDAO();
        UserDAO userDAO = new UserDAO();
        User parentUser = null;
        public MainWindow(User user)
        {
            InitializeComponent();
            try
            {
                if (user != null)
                {
                    parentUser = user;
                    txt_Hello_User.Text = user.Name;
                    LoadApartments();
                }
                else
                {
                    throw new UserDoesNotExist("Authentification of the user failed");
                }
            }
            catch (UserDoesNotExist ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        private async void LoadApartments()
        {
           var apartments = await Task.Run(() => apartmentDAO.GetAll());
           Apartments.ItemsSource = apartments;
        }

        private async void LoadApartmentsByLocation(string state)
        {
            var apartments = await Task.Run(() => apartmentDAO.GetAllByLocation(state));
            Apartments.ItemsSource = apartments;
        }

        
        private void profile_Image_Click(object sender, RoutedEventArgs e)
        {
            profile_Image.Content = " ";
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                string destinationPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Sources/images");
                if (!Directory.Exists(destinationPath))
                    Directory.CreateDirectory(destinationPath);
                File.Copy(dlg.FileName, System.IO.Path.Combine(destinationPath, dlg.SafeFileName));
                ImageBrush brush1 = new ImageBrush();
                brush1.ImageSource = new BitmapImage(new Uri(filename));
                profile_Image.Background = brush1;
            }
        }

        private void btn_More_Click(object sender, RoutedEventArgs e)
        {
            // Získání stisknutého tlačítka
            Button btnMore = sender as Button;

            // Získání informací o apartmánu, který je zdrojem tlačítka
            if (btnMore != null)
            {
                Apartment selectedApartment = btnMore.DataContext as Apartment;

                // Zobrazení informací o apartmánu v novém okně
                if (selectedApartment != null)
                {
                    InformativeWindow informativeWindow = new InformativeWindow(selectedApartment, parentUser);
                    informativeWindow.Show();
                }
            }
        }

       
        /// <summary>
        /// Metoda, která přesměruje uživatele na stranu 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Profile_Click(object sender, RoutedEventArgs e)
        {
            pages.SelectedIndex = 1;
        }
        /// <summary>
        /// Metoda, která aktualizuje informace o uživateli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(SecondNameTextBox.Text) || string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password))
                {
                    throw new InvalidInput("All fields are required.");
                }
                if (userDAO.GetByMail(EmailTextBox.Text) != null)
                {
                    throw new UserAlreadyExists("User with this email already exists");
                }
                if (userDAO.ValidatePassword(PasswordTextBox.Password))
                {
                    throw new WeakPassword("Password does not meet security requirements");
                }
                if (!userDAO.EmailContains(EmailTextBox.Text))
                {
                    throw new EmailInvalid("Email does not contain '@'");
                }
                userDAO.UpdateUser(NameTextBox.Text, SecondNameTextBox.Text, EmailTextBox.Text, PasswordTextBox.Password);
            }
            catch (InvalidInput ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (WeakPassword ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (EmailInvalid ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (UserAlreadyExists ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Metoda, která přesměruje uživatele na stranu 0(domovská stránka)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            pages.SelectedIndex = 0;
        }
        /// <summary>
        /// Metoda, která mění karty bytů podle výběru uživatele z comboboxu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)comboBox.SelectedItem;
            string value = typeItem.Content.ToString();
            LoadApartmentsByLocation(value);
            if (value.Equals("All"))
            {
                LoadApartments();
            }
        }

      
    }
}
