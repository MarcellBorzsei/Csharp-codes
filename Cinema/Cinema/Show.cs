using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class Show
    {
        public string interval { get; }
        public string movie { get; }
        public List<Ticket> tickets { get; }


        public Show(string interval, string movie)
        {
            this.interval = interval;
            this.movie = movie;
            tickets = new List<Ticket>();
        }

        public int countFreeTickets()
        {
            int counter = 0;
            foreach (Ticket t in tickets)
            {
                if(t.isFree())
                {
                    counter += 1;
                }
            }
            return counter;
        }

        public int countReservedTickets()
        {
            int counter = 0;
            foreach (Ticket t in tickets)
            {
                if (t.isReserved())
                {
                    counter += 1;
                }
            }
            return counter;
        }

        public int countBoughtTickets()
        {
            int counter = 0;
            foreach (Ticket t in tickets)
            {
                if (!t.isFree() && !t.isReserved())
                {
                    counter += 1;
                }
            }
            return counter;
        }

        public void fillWithFreeTickets(int rowNum, int colNum)
        {
            for(int i = 0; i < rowNum; i++)
            {
                for(int j = 0; j < colNum; j++)
                {
                    tickets.Add(new FreeTicket(i, j));
                }
            }
        }



    }
}
