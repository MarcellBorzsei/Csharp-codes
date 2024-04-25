using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace Cinema
{
    public class Infile
    {
        private TextFileReader reader;
        public Infile(string filename)
        {
            reader = new TextFileReader(filename);
        }

        public bool ReadRoom(out Room r)
        {
            r = null;
            bool l = reader.ReadLine(out string line);

            if(l)
            {
                char[] separators = { ',' };
                string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string type = tokens[0];

                switch(type)
                {
                    case "Medium":
                        r = new Medium(int.Parse(tokens[1]), int.Parse(tokens[2]));
                        break;
                    case "Large":
                        r = new Large(int.Parse(tokens[1]), int.Parse(tokens[2]));
                        break;
                    case "VIP":
                        r = new VIP(int.Parse(tokens[1]), int.Parse(tokens[2]));
                        break;
                }

                for(int i = 3; i < tokens.Length; i += 2)
                {
                    Show s = new Show(tokens[i], tokens[i + 1]);
                    s.fillWithFreeTickets(5, 7);
                    r.shows.Add(s);
                }
            }

            return l;
        }

        public bool ReadPurchase(out Guest g, ref MovieTheater theater)
        {
            g = null;
            bool l = reader.ReadLine(out string line);
            
            if(l)
            {
                char[] separators = { ',' };
                string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string type = tokens[0];
                string name = tokens[1];


                // in case of "R"
                if(theater.reservedGuests.Contains(name))
                {
                    theater.Locate(name, int.Parse(tokens[3]), tokens[4], out Guest guest);
                    guest.buyReserved(int.Parse(tokens[3]), tokens[4]);
                }
                else
                {
                    switch (type)
                    {
                        case "Adult":
                            g = new Adult(tokens[1], theater);
                            break;
                        case "Kid":
                            g = new Kid(tokens[1], theater);
                            break;
                        case "Student":
                            g = new Student(tokens[1], theater);
                            break;
                        case "Old":
                            g = new Old(tokens[1], theater);
                            break;
                        case "Frequent":
                            g = new Frequent(tokens[1], theater);
                            break;
                    }

                    string type2 = tokens[2];

                    switch (type2)
                    {
                        case "BF":
                            g.buyFree(int.Parse(tokens[3]), tokens[4]);
                            break;
                        case "R":
                            theater.reservedGuests.Add(name);
                            g.reserve(int.Parse(tokens[3]), tokens[4]);
                            break;
                    }
                }

                
            }

            return l;
        }
    }
}
