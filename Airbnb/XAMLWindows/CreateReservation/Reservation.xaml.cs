using Airbnb.Classes;
using Airbnb.Database.DAO.DAO_Reservation;
using Airbnb.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Airbnb.XAMLWindows.CreateReservation
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {
        Apartment parentApartment = null;
        DAOReservation reservationDAO = new DAOReservation();
        public Reservation(Apartment apartment)
        {
            InitializeComponent();
            try
            {
                if (apartment != null)
                {
                    parentApartment = apartment;
                    res_Owner.Text = parentApartment.GetOwner.Name;
                    res_Apartment.Text = parentApartment.Name_apartment;
                }
                else
                {
                    throw new ApartmentDoesNotExists("Apartment does not exists");
                }
            }
            catch (ApartmentDoesNotExists ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        private void btn_MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!IsServerConnected())
                {
                    throw new ConnectionException("Server connection failed");
                }
                if (String.IsNullOrEmpty(res_Name.Text) || String.IsNullOrEmpty(res_SecondName.Text) || String.IsNullOrEmpty(res_ArrivalDate.Text)
                    || String.IsNullOrEmpty(res_DepartureDate.Text) )
                {
                    throw new InvalidInput("All fields are required");
                }
                else
                {
                    reservationDAO.Save(new Classes.Reservation(parentApartment.id,parentApartment.GetOwner.Id,res_ArrivalDate.DisplayDate, res_DepartureDate.DisplayDate));
                    this.Hide();
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
