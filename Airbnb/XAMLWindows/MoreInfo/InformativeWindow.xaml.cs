using Airbnb.Classes;
using Airbnb.Database.DAO.DAO_Review;
using Airbnb.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Airbnb.XAMLWindows.MoreInfo
{
    /// <summary>
    /// Interaction logic for InformativeWindow.xaml
    /// </summary>
    public partial class InformativeWindow : Window
    {
        DAOReview dAOReview = new DAOReview();
        Apartment parentApartment = null;
        User parentUser = null;
        public InformativeWindow(Apartment apartment, User user)
        {
            InitializeComponent();
           
            try
            {
                if (apartment != null || user != null)
                {
                    LoadReviews();
                    parentApartment = apartment;
                    parentUser = user;
                    this.DataContext = parentApartment;
                }
                else
                {
                    throw new InvalidInput("Apartment does not exists");
                }
            }
            catch (InvalidInput ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Metoda, která vypíná aplikaci po stisknutí tlačítka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Metoda, která minimalizuje aplikaci po stisknutí tlačítka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimaze_Click(object sender, RoutedEventArgs e)
        {
            Window window = (Window)Application.Current.MainWindow;
            window.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// Metoda, která maximalizuje aplikaci po stisknutí tlačítka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maximaze_Click(object sender, RoutedEventArgs e)
        {
            Window window = (Window)Application.Current.MainWindow;
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        private async void LoadReviews()
        {
            var reviews = await Task.Run(() => dAOReview.GetAll());
            ReviewList.ItemsSource = reviews;
            /*
            var apartments = await Task.Run(() => {
                try
                {
                    if (parentApartment == null)
                    {
                        throw new NullReferenceException("parentApartment.GetComments returned null");
                    }
                    return parentApartment.GetComments;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return Enumerable.Empty<Review>();
                }
            });
            ReviewList.ItemsSource = apartments;
            */
        }

        private void btn_SendReview_Click(object sender, RoutedEventArgs e)
        {
           

            try
            {
                if (!IsServerConnected())
                {
                    throw new ConnectionException("Server connection failed");
                }
                if (string.IsNullOrEmpty(box_Comment.Text) || box_Comment.Text.Length > 250  )
                {
                    throw new InvalidInput("Comment cannot be empty or greater than 250 words");
                }
                if (ratingBar.Value <= 0)
                {
                    throw new InvalidInput("You cannot rate with 0");
                }
                else
                {
                    dAOReview.Save(new Review(ratingBar.Value,box_Comment.Text, parentUser.Id));
                    LoadReviews();
                    box_Comment.Text = "";
                }
            }
            catch (InvalidInput ex)
            {

                MessageBox.Show(ex.Message);
            }
            catch (ConnectionException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Create_Reservation_Click(object sender, RoutedEventArgs e)
        {
            XAMLWindows.CreateReservation.Reservation reservation = new CreateReservation.Reservation(parentApartment);
            reservation.Show();
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

