using Airbnb.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Classes
{/// <summary>
/// Třída pro vytvoření Uživatele. Uživatel může být zákazník(costumer), majitel(owner) nebo zároveň obojí
/// </summary>
   public class User : IBaseClass
    {
        private int id;
        private Type_Of_User user_type;
        private string name;
        private string second_name;
        private string email;
        private string password;

        public int Id { get => id; set => id = value; }
        public Type_Of_User User_type { get => user_type; set => user_type = value; }
        public string Name { get => name; set => name = value; }
        public string Second_name { get => second_name; set => second_name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public override string ToString()
        {
            return "id:" + id + "," + "type:" + user_type.ToString() + "," + "name:" + name + "," + 
                "second_name:" + second_name  + "email:" + email + "," + "password:" + password;
        }
        /// <summary>
        /// Kontruktor pro vytvoření Uživatele
        /// </summary>
        /// <param name="user_type">role uživatele</param>
        /// <param name="name">jméno uživatele</param>
        /// <param name="second_name">příjmení uživatele</param>
        /// <param name="email">email uživatele</param>
        /// <param name="password">heslo uživatele</param>
        public User( Type_Of_User user_type = default, string name = " ", string second_name = " ", string email = " ", string password = " ")
        {
            this.Id = 0;
            this.User_type = user_type;
            this.Name = name;
            this.Second_name = second_name;
            this.Email = email;
            this.Password = password;
        }
        /// <summary>
        /// Enum s rolemi
        /// </summary>
        public enum Type_Of_User
        {
            Owner = 1,
            Costumer = 2
        }
    }
}
