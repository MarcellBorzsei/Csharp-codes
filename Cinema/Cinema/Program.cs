using System.Globalization;

namespace Cinema
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MovieTheater pestimozi = new MovieTheater();

            Infile roomfile = new Infile("../../../Rooms.txt");

            while(roomfile.ReadRoom(out Room r))
            {
                pestimozi.rooms.Add(r);
            }

            foreach(Room r in pestimozi.rooms)
            {
                Console.WriteLine(r.shows.Count);
            }

            Infile purchasefile = new Infile("../../../GuestsandPurchases.txt");

            while (purchasefile.ReadPurchase(out Guest g, ref pestimozi)) { }


            /*   Display example datas
             *   
            foreach(Room r in pestimozi.rooms)
            {
                foreach(Show s in r.shows)
                {
                    Console.WriteLine(s.countBoughtTickets());
                    Console.WriteLine(s.countFreeTickets());
                    Console.WriteLine(s.countReservedTickets());
                    Console.WriteLine();

                }
            }*/
        }
    }
}