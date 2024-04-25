using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public abstract class Guest
    {
        public string name { get; }

        public MovieTheater theater { get;}

        public Guest(string name, MovieTheater theater)
        {
            this.name = name;
            this.theater = theater;
        }

        public abstract int Discount(Medium k);
        public abstract int Discount(Large k);
        public abstract int Discount(VIP k);

        public void reserve(int requestedRoomId, string requestedInterval)
        {
            theater.reserveTicket(requestedRoomId, requestedInterval, this);
        }

        public void buyFree(int requestedRoomId, string requestedInterval)
        {
            theater.buyFreeTicket(requestedRoomId, requestedInterval, this);
        }

        public void buyReserved(int requestedRoomId, string requestedInterval)
        {
            theater.buyReservedTicket(requestedRoomId, requestedInterval, this);
        }
    }


    public class Kid : Guest
    {
        public Kid(string name, MovieTheater theater) : base(name, theater) { }
        public override int Discount(Medium k)
        {
            return 40;
        }
        public override int Discount(Large k)
        {
            return 40;
        }

        public override int Discount(VIP k)
        {
            return 0;
        }
    }

    public class Student : Guest
    {
        public Student(string name, MovieTheater theater) : base(name, theater) { }
        public override int Discount(Medium k)
        {
            return 30;
        }
        public override int Discount(Large k)
        {
            return 20;
        }

        public override int Discount(VIP k)
        {
            return 0;
        }
    }

    public class Adult : Guest
    {
        public Adult(string name, MovieTheater theater) : base(name, theater) { }
        public override int Discount(Medium k)
        {
            return 10;
        }
        public override int Discount(Large k)
        {
            return 0;
        }

        public override int Discount(VIP k)
        {
            return 0;
        }
    }

    public class Old : Guest
    {
        public Old(string name, MovieTheater theater) : base(name, theater) { }
        public override int Discount(Medium k)
        {
            return 30;
        }
        public override int Discount(Large k)
        {
            return 20;
        }

        public override int Discount(VIP k)
        {
            return 0;
        }
    }

    public class Frequent : Guest
    {
        public Frequent(string name, MovieTheater theater) : base(name, theater) { }
        public override int Discount(Medium k)
        {
            return 30;
        }
        public override int Discount(Large k)
        {
            return 30;
        }

        public override int Discount(VIP k)
        {
            return 0;
        }
    }
}
