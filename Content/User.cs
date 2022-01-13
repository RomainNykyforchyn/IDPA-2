using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idpa.Content
{
    public class User
    {
        private int id, amount, admin, volume;
        private String name, password, provider;
        private Boolean international;

        public int Id { get => id; set => id = value; }
        public int Amount { get => amount; set => amount = value; }
        public int Admin { get => admin; set => admin = value; }
        public int Volume { get => volume; set => volume = value; }
        public string Provider { get => provider; set => provider = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public bool International { get => international; set => international = value; }

        public User(int id, int amount, int admin, int volume, string name, string password, string provider, bool international)
        {
            this.id = id;
            this.amount = amount;
            this.admin = admin;
            this.volume = volume;
            this.name = name;
            this.password = password;
            this.provider = provider;
            this.international = international;
        }

        public Boolean IsAdmin()
        {
            if (this.admin.Equals(1)){
                return true;
            }
            return false;
        }
    }
}