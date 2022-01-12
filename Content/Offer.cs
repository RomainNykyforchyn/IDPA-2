using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idpa.Content
{
    public class Offer
    {
        private int id, minimumTime;
        private Double speed, price, price2;
        private String provider, name, location, internet, telephone, addition;

        public Offer(int id, int minimumTime, double speed, double price, double price2, string provider, string name, string location, string internet, string telephone, string addition)
        {
            this.id = id;
            this.minimumTime = minimumTime;
            this.speed = speed;
            this.price = price;
            this.price2 = price2;
            this.provider = provider;
            this.name = name;
            this.location = location;
            this.internet = internet;
            this.telephone = telephone;
            this.addition = addition;
            
    }

        public int Id { get => id; set => id = value; }
        public int MinimumTime { get => minimumTime; set => minimumTime = value; }
        public double Speed { get => speed; set => speed = value; }
        public double Price { get => price; set => price = value; }
        public double Price2 { get => price2; set => price2 = value; }
        public string Provider { get => provider; set => provider = value; }
        public string Name { get => name; set => name = value; }
        public string Location { get => location; set => location = value; }
        public string Internet { get => internet; set => internet = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Addition { get => addition; set => addition = value; }
    }
}