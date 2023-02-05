using Airbnb.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Classes
{
    /// <summary>
    /// Třída pro vytvoření Recenze pro ubytovaní
    /// </summary>
    public class Review : IBaseClass
    {
        private int id;
        private double rating;
        private string comment;
        private int fk_user;
        /// <summary>
        /// Konstruktor pro vytvoření třídy Review
        /// </summary>
        /// <param name="rating">Hodnocení(1-5)</param>
        /// <param name="comment">Komentář k ubytovaní</param>
        /// <param name="fk_user">Který uživatel přidal svoji recenzi</param>
        public Review(double rating = 0, string comment = " ", int fk_user = 0)
        {
            this.id = 0;
            this.Rating = rating;
            this.Comment = comment;
            this.Fk_user = fk_user;
        }

        public int Id { get => id; set => id = value; }
        public double Rating { get => rating; set => rating = value; }
        public string Comment { get => comment; set => comment = value; }
        public int Fk_user { get => fk_user; set => fk_user = value; }
        /// <summary>
        /// Metoda vypisující vlastnosti Review
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return id + "," + rating + "," + comment + "," + fk_user;
        }
    }
}
