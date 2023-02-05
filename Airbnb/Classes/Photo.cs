using Airbnb.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Classes
{
    /// <summary>
    /// Třída pro vytvoření Fotek, pro ukázku vnitřních i vnějších prostorů Bytu
    /// </summary>
    public class Photo : IBaseClass
    {
        public int id;
        private string name_of_photo;
        private int fk_author;
        private DateTime day_of_acqusition;
        private string photo_path;
        /// <summary>
        /// Konstruktor pro vytvoření třídy Photo, hodnoty jsou nastavené na defaultních hodnotách připravené pro DB
        /// </summary>
        /// <param name="name_of_photo">název fotky</param>
        /// <param name="fk_author">ID autora fotky</param>
        /// <param name="day_of_acqusition">Datum, kdy byla fotka pořízena</param>
        public Photo(string name_of_photo = " ", int fk_author = 0, DateTime day_of_acqusition = default, string photo_path = "")
        {
            this.id = 0;
            this.Name_of_photo = name_of_photo;
            this.Fk_author = fk_author;
            this.Day_of_acqusition = day_of_acqusition;
            this.Photo_path = photo_path;
        }

        public int Id { get => id; set => id = value; }
        public string Name_of_photo { get => name_of_photo; set => name_of_photo = value; }
        public int Fk_author { get => fk_author; set => fk_author = value; }
        public DateTime Day_of_acqusition { get => day_of_acqusition; set => day_of_acqusition = value; }
        public string Photo_path { get => photo_path; set => photo_path = value; }

        /// <summary>
        /// Metoda vypisující vlastnosti Photo
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return id + "," + name_of_photo + "," + fk_author + "," + day_of_acqusition;
        }
    }
}
