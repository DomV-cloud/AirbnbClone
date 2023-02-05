using Airbnb.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Classes
{/// <summary>
/// Třída pro vytvoření Rezervace
/// </summary>
    public class Reservation : IBaseClass
    {
        public int id;
        private int fk_apartment_id;
        private int fk_apartment_costumer_id;
        private DateTime reservation_since;
        private DateTime reservation_to;
        /// <summary>
        ///  Konstruktor pro vytvoření třídy Reservation
        /// </summary>
        /// <param name="fk_apartment_id">ID bytu, který se rezervuje</param>
        /// <param name="fk_apartment_costumer_id">ID uživatele, který si chce byt pronajmout</param>
        /// <param name="reservation_since">Datum rezervace odkdy</param>
        /// <param name="reservation_to">Datum rezervace dokdy</param>
        public Reservation(int fk_apartment_id = 0, int fk_apartment_costumer_id = 0, DateTime reservation_since = default , DateTime reservation_to = default)
        {
            this.id = 0;
            this.Fk_apartment_id = fk_apartment_id;
            this.Fk_apartment_costumer_id = fk_apartment_costumer_id;
            this.Reservation_since = reservation_since;
            this.Reservation_to = reservation_to;
        }

        public int Id { get => id; set => id = value; }
        public int Fk_apartment_id { get => fk_apartment_id; set => fk_apartment_id = value; }
        public int Fk_apartment_costumer_id { get => fk_apartment_costumer_id; set => fk_apartment_costumer_id = value; }
        public DateTime Reservation_since { get => reservation_since; set => reservation_since = value; }
        public DateTime Reservation_to { get => reservation_to; set => reservation_to = value; }
        /// <summary>
        /// Metoda vypisující vlastnosti Reservation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return id + "," + fk_apartment_costumer_id;
        }
    }
}
