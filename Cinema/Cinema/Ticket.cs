using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public abstract class Ticket
    {
        public int row { get; }
        public int column { get; }

        public Guest owner { get; }
        
        public Ticket(int row, int column, Guest owner)
        {
            this.row = row;
            this.column = column;
            this.owner = owner;
        }

        public Ticket(int row, int column)
        {
            this.row = row;
            this.column = column;     
        }

        public abstract bool isFree();
        public abstract bool isReserved();
    }

    public class BoughtTicket : Ticket
    {
        public double price { get; }

        public BoughtTicket(Guest owner, int row, int column, double price) : base(row, column, owner)
        {
            this.price = price;
        }

        public override bool isFree()
        {
            return false;
        }

        public override bool isReserved()
        {
            return false;
        }
    }

    public class ReservedTicket : Ticket
    {
        public ReservedTicket(Guest owner, int row, int column) : base(row, column, owner) { }
        public override bool isFree()
        {
            return false;
        }

        public override bool isReserved()
        {
            return true;
        }

    }

    public class FreeTicket : Ticket
    {

        public FreeTicket(int row, int column) : base(row, column) { }
        public override bool isFree()
        {
            return true;
        }

        public override bool isReserved()
        {
            return false;
        }
    }
}
