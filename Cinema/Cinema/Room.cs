using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public abstract class Room
    {
        
        protected int simplePrice;
        public int id { get; }
        public List<Show> shows { get; }

        public Room( int simplePrice, int id)
        {
            this.simplePrice = simplePrice;
            this.id = id;
            shows = new List<Show>();
        }

        public abstract double discountedPrice(Guest g);

    }

    public class VIP : Room
    {
        public VIP(int simplePrice, int id) : base(simplePrice, id) { }

        public override double discountedPrice(Guest g)
        {
            return (simplePrice * (100 - g.Discount(this) / 100));
        }

    }

    public class Large : Room
    {
        public Large(int simplePrice, int id) : base(simplePrice, id) { }

        public override double discountedPrice(Guest g)
        {
            return (simplePrice * (100 - g.Discount(this) / 100));
        }

    }

    public class Medium : Room
    {
        public Medium(int simplePrice, int id) : base(simplePrice, id) { }

        public override double discountedPrice(Guest g)
        {
            return (simplePrice * (100 - g.Discount(this) / 100));
        }

    }
}
