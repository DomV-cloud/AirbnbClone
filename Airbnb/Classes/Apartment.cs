using Airbnb.Database.DAO;
using Airbnb.Database.DAO.DAO_Apartment;
using Airbnb.Database.DAO.DAO_Location;
using Airbnb.Database.DAO.DAO_Review;
using Airbnb.Database.Singleton;
using Airbnb.Exceptions;
using Airbnb.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace Airbnb.Classes
{
    /// <summary>
    /// Třída pro vytvoření Bytu
    /// </summary>
    public class Apartment : IBaseClass
    {
        LocationDAO locationDAO = new LocationDAO();
        UserDAO userDAO = new UserDAO();
        ApartmentDAO apartmentDAO = new ApartmentDAO();
        DAOReview reviewDAO = new DAOReview();

        public int id;
        private string name_apartment;
        private int fk_apartment_photo_id;
        private int fk_apartment_location_id;
        private double size;
        private double apartment_price;
        private int fk_apartment_review_id;
        private string apartment_info;
        private int number_of_guests;
        private int number_of_bedroom;
        private int number_of_beds;
        private int number_of_bathrooms;
        private int fk_apartment_user_owner_id;
        

        /// <summary>
        /// Konstruktor pro vytvoření třídy Apartment, hodnoty jsou nastavené na defaultních hodnotách připravené pro DB
        /// </summary>
        /// <param name="name_apartment">Název Apartmentu</param>
        /// <param name="fk_apartment_photo_id">ID třídy Photo, které je součástí třídy Apartment</param>
        /// <param name="fk_apartment_location_id">ID třídy Location, které je součástí třídy Apartment</param>
        /// <param name="size">Velikost Bytu(v m2)</param>
        /// <param name="apartment_price">Cena Bytu(v eurech)</param>
        /// <param name="fk_apartment_review_id">ID třídy Review, které je součástí třídy Apartment</param>
        /// <param name="apartment_info">informace o Bytu</param>
        /// <param name="number_of_guests">Maximální počet hostů k ubytování</param>
        /// <param name="number_of_bedroom"> počet ložnic </param>
        /// <param name="number_of_beds"> počet postelí</param>
        /// <param name="number_of_bathrooms"> počet koupelen/param>
        /// <param name="fk_apartment_user_owner_id">ID třídy User, které je součástí třídy Apartment. Jde o majitele apartmentu</param>
        public Apartment( string name_apartment = "", int fk_apartment_photo_id = 0, 
            int fk_apartment_location_id = 0 , double size = 0, double apartment_price = 0, int fk_apartment_review_id = 0, 
            string apartment_info = "", int number_of_guests = 0, int number_of_bedroom = 0, int number_of_beds = 0, int number_of_bathrooms = 0, int fk_apartment_user_owner_id = 0)
        {
            this.id = 0;
            this.Name_apartment = name_apartment;
            this.Fk_apartment_photo_id = fk_apartment_photo_id;
            this.Fk_apartment_location_id = fk_apartment_location_id;
            this.Size = size;
            this.Apartment_price = apartment_price;
            this.Fk_apartment_review_id = fk_apartment_review_id;
            this.Apartment_info = apartment_info;
            this.Number_of_guests = number_of_guests;
            this.Number_of_bedroom = number_of_bedroom;
            this.Number_of_beds = number_of_beds;
            this.Number_of_bathrooms = number_of_bathrooms;
            this.Fk_apartment_user_owner_id = fk_apartment_user_owner_id;
           
        }
        /// <summary>
        /// Metoda vypisující vlastnosti třídy Apartment
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return id  + "," + Name_apartment;
        }
        /// <summary>
        /// Metoda, která vrací cestu k uloženému obrázku
        /// </summary>
        /// <param name="id">id fotografie patřící k bytu</param>
        /// <returns>cestu k obrázku</returns>
        public string getPhoto(int id)

        {
            string photo_path = "";
            try
            {
                if (id <= 0  || id.GetType() != typeof(int))
                {
                    throw new InvalidInput("Invalid input");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("SELECT photo_path FROM Photo WHERE id = @id;", conn))
                        {
                            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                photo_path = reader["photo_path"].ToString();
                            }

                        }
                        conn.Close();
                    }
                }
            }
            catch (InvalidInput ex)
            {

                MessageBox.Show(ex.Message);
            }
            return photo_path;

}

        public decimal getRating(int id)
        {
            decimal rating = 0;
            try
            {
                if (id <= 0 || id.GetType() != typeof(int))
                {
                    throw new InvalidInput("Invalid input");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseSingleton.connString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("SELECT rating FROM Review WHERE id = @id;", conn))
                        {
                            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                rating = (decimal) reader["rating"];
                            }

                        }
                        conn.Close();
                    }
                }
            }
            catch (InvalidInput ex)
            {

                MessageBox.Show(ex.Message);
            }
            return rating;

        }


        


        public int Id { get => id; set => id = value; }
        public string Name_apartment { get => name_apartment; set => name_apartment = value; }
        public int Fk_apartment_photo_id { get => fk_apartment_photo_id; set => fk_apartment_photo_id = value; }
        public int Fk_apartment_location_id { get => fk_apartment_location_id; set => fk_apartment_location_id = value; }
        public double Size { get => size; set => size = value; }
        public double Apartment_price { get => apartment_price; set => apartment_price = value; }
        public int Fk_apartment_review_id { get => fk_apartment_review_id; set => fk_apartment_review_id = value; }
        public string Apartment_info { get => apartment_info; set => apartment_info = value; }
        public int Number_of_guests { get => number_of_guests; set => number_of_guests = value; }
        public int Number_of_bedroom { get => number_of_bedroom; set => number_of_bedroom = value; }
        public int Number_of_beds { get => number_of_beds; set => number_of_beds = value; }
        public int Number_of_bathrooms { get => number_of_bathrooms; set => number_of_bathrooms = value; }
        public int Fk_apartment_user_owner_id { get => fk_apartment_user_owner_id; set => fk_apartment_user_owner_id = value; }

        public String Apartment_Photo { get => getPhoto(this.Fk_apartment_photo_id); }
        public Decimal Apartment_Rating { get => getRating(this.Fk_apartment_photo_id); }

        public Location GetLocation { get => locationDAO.GetById(Fk_apartment_location_id); }
        public User GetOwner { get => userDAO.GetById(Fk_apartment_user_owner_id); }
        public IEnumerable<Review> GetComments { get => reviewDAO.GetAllReviews(Fk_apartment_review_id); }
        public Double AvgRating { get => reviewDAO.GetAverageRating(Fk_apartment_review_id); }
    }
}
