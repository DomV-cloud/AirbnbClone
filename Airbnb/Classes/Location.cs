using Airbnb.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Classes
{
    /// <summary>
    /// Třída pro vytvoření Lokace, ve které je Apartment
    /// </summary>
    public class Location : IBaseClass
    {
        public int id;
        private string name_of_location;
        private string city;
        private string state;
        private string info;
        /// <summary>
        /// Konstruktor pro vytvoření třídy Location, hodnoty jsou nastavené na defaultních hodnotách připravené pro DB
        /// </summary>
        /// <param name="name_of_location">Název lokace</param>
        /// <param name="city">Název města, ve které se lokace nachází</param>
        /// <param name="state">Název státu, ve kterém se lokace nachází</param>
        /// <param name="info">Inoformace o lokaci</param>
        public Location( string name_of_location = "", string city = "", string state = "" , string info = "")
        {
            this.id = 0;
            this.Name_of_location = name_of_location;
            this.City = city;
            this.State = state;
            this.Info = info;
        }

        public int Id { get => id; set => id = value; }
        public string Name_of_location { get => name_of_location; set => name_of_location = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string Info { get => info; set => info = value; }
        /// <summary>
        /// Metoda vypisující vlastnosti Location
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return id + "," + name_of_location + "," + city + "," + state + "," + info;
        }
    }
}
