using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class RoomNotFoundException : Exception { }
    public class ShowNotFoundException : Exception { }
    public class FullHouseException : Exception { }
    public class NoReservationException : Exception { }
    public class MovieTheater
    {
        public List<Room> rooms { get; }
        public List<string> reservedGuests { get; set; }

        public MovieTheater()
        {
            rooms = new List<Room>();
            reservedGuests = new List<string>();
        }


        public void Locate(string name, int id, string interval, out Guest g)
        {
            g = null;
            foreach (Room r in rooms)
            {
                if(r.id == id)
                {
                    foreach(Show s in r.shows)
                    {
                        if(s.interval == interval)
                        {
                            foreach(Ticket t in s.tickets)
                            {
                                if(t.isReserved())
                                {
                                    if (t.owner.name == name)
                                    {
                                        g = t.owner;
                                        break;
                                    }
                                }
                                
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }

        public void reserveTicket(int requestedRoomId, string requestedInterval, Guest g)
        {
            try
            {
                bool lRoom = false;
                Room requestedRoom = null;
                foreach (Room r in rooms)
                {
                    if (requestedRoomId == r.id)
                    {
                        lRoom = true;
                        requestedRoom = r;
                        break;
                    }

                }
                if (lRoom == false)
                {
                    throw new RoomNotFoundException { };
                }


                bool lShow = false;
                Show requestedShow = null;
                foreach (Show s in requestedRoom.shows)
                {
                    if (requestedInterval == s.interval)
                    {
                        requestedShow = s;
                        lShow = true;
                        break;
                    }
                }
                if (lShow == false)
                {
                    throw new ShowNotFoundException { };
                }


                bool lTicket = false;
                foreach (Ticket t in requestedShow.tickets)
                {
                    if (t.isFree())
                    {
                        lTicket = true;
                        requestedShow.tickets.Add(new ReservedTicket(g, t.row, t.column));
                        requestedShow.tickets.Remove(t);
                        break;
                    }
                }
                if (lTicket == false)
                {
                    throw new FullHouseException { };
                }

            }
            catch(RoomNotFoundException)
            {
                Console.WriteLine("Cannot find room.");
            }
            catch (ShowNotFoundException)
            {
                Console.WriteLine("Cannot find show.");
            }
            catch (FullHouseException)
            {
                Console.WriteLine("There is no available seats.");
            }

        }

        public void buyFreeTicket(int requestedRoomId, string requestedInterval, Guest g)
        {
            try
            {

                bool lRoom = false;
                Room foundRoom = null;
                foreach (Room r in rooms)
                {
                    if (requestedRoomId == r.id)
                    {
                        foundRoom = r;
                        lRoom = true;
                        break;
                    }

                }
                if (lRoom == false)
                {
                    throw new RoomNotFoundException { };
                }


                bool lShow = false;
                Show requestedShow = null;
                foreach (Show s in foundRoom.shows)
                {
                    if (requestedInterval == s.interval)
                    {
                        requestedShow = s;
                        lShow = true;
                        break;
                    }
                }
                if (lShow == false)
                {
                    throw new ShowNotFoundException { };
                }


                bool lTicket = false;
                foreach (Ticket t in requestedShow.tickets)
                {
                    if (t.isFree())
                    {
                        lTicket = true;
                        requestedShow.tickets.Add(new BoughtTicket(g, t.row, t.column, foundRoom.discountedPrice(g)));
                        requestedShow.tickets.Remove(t);
                        break;
                    }
                }
                if (lTicket == false)
                {
                    throw new FullHouseException { };
                }

            }
            catch (RoomNotFoundException)
            {
                Console.WriteLine("Cannot find room.");
            }
            catch (ShowNotFoundException)
            {
                Console.WriteLine("Cannot find show.");
            }
            catch (FullHouseException)
            {
                Console.WriteLine("There is no available seats.");
            }

        }

        public void buyReservedTicket(int requestedRoomId, string requestedInterval, Guest g)
        {
            try
            {

                bool lRoom = false;
                Room foundRoom = null;
                foreach (Room r in rooms)
                {
                    if (requestedRoomId == r.id)
                    {
                        foundRoom = r;
                        lRoom = true;
                        break;
                    }

                }
                if (lRoom == false)
                {
                    throw new RoomNotFoundException { };
                }


                bool lShow = false;
                Show requestedShow = null;
                foreach (Show s in foundRoom.shows)
                {
                    if (requestedInterval == s.interval)
                    {
                        requestedShow = s;
                        lShow = true;
                        break;
                    }
                }
                if (lShow == false)
                {
                    throw new ShowNotFoundException { };
                }


                bool lTicket = false;
                foreach (Ticket t in requestedShow.tickets)
                {
                    if (t.isReserved())
                    {
                        if(t.owner == g)
                        {
                            lTicket = true;
                            requestedShow.tickets.Add(new BoughtTicket(g, t.row, t.column, foundRoom.discountedPrice(g)));
                            requestedShow.tickets.Remove(t);
                            break;
                        }
                        
                    }
                }
                if (lTicket == false)
                {
                    throw new NoReservationException { };
                }

            }
            catch (RoomNotFoundException)
            {
                Console.WriteLine("Cannot find room.");
            }
            catch (ShowNotFoundException)
            {
                Console.WriteLine("Cannot find show.");
            }
            catch (NoReservationException)
            {
                Console.WriteLine("The reservation is invalid");
            }

        }

        public struct movieAndViewers
        {
            public string movie { get; }

            public int viewers { get; }
            public movieAndViewers(string m, int v)
            {
                movie = m;
                viewers = v;
            }

            
        }

        public string mostWatchedMovie()
        {
            
            List<movieAndViewers> moviesNumbers = new List <movieAndViewers> ();
            List<string> movies = new List<string>();
            foreach(Room r in rooms)
            {
                foreach(Show s in r.shows)
                {
                    moviesNumbers.Add(new movieAndViewers(s.movie, s.countBoughtTickets()));
                    if (!movies.Contains(s.movie))
                    {
                        movies.Add(s.movie);
                    }
                }
                
            }

            int counter = 0;
            int mostWatchedNumber = 0; 
            string mostWatched = "";
            for (int i = 0; i < movies.Count(); i++)
            {
                for(int j = 0; j < moviesNumbers.Count(); j++)
                {
                    if (moviesNumbers[j].movie == movies[i])
                    {
                        counter += moviesNumbers[j].viewers;
                    }
                }
                if(counter > mostWatchedNumber)
                {
                    mostWatchedNumber = counter;
                    mostWatched = movies[i];
                }
                counter = 0;
            }

            return mostWatched;
        }


    }
}
