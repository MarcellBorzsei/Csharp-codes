using Cinema;
namespace CinemaTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ObjectsSize()
        {
            MovieTheater pestimozi = new MovieTheater();

            VIP r1 = new VIP(2400, 1);
            Large r2 = new Large(2000, 2);

            Show s1 = new Show("15:00-17:00", "Backtothefuture");
            Show s2 = new Show("14:00-16:00", "BacktothefutureII");
            Show s3 = new Show("12:00-14:00", "BacktothefutureIII");


            s1.fillWithFreeTickets(4, 7);

            r1.shows.Add(s1);
            r1.shows.Add(s2);
            r1.shows.Add(s3);


            pestimozi.rooms.Add(r1);
            pestimozi.rooms.Add(r2);


            // Count of rooms
            Assert.AreEqual(pestimozi.rooms.Count, 2);
            // Count of shows
            Assert.AreEqual(pestimozi.rooms[0].shows.Count, 3);
            // Count of free tickets + use of fillWithFreeTickets()
            Assert.AreEqual(s1.countFreeTickets(), 28);
        }

        [TestMethod]
        public void TheaterMethods()
        {
            MovieTheater pestimozi = new MovieTheater();

            VIP r1 = new VIP(2400, 1);
            Large r2 = new Large(2000, 2);

            Show s1 = new Show("15:00-17:00", "Backtothefuture");
            Show s2 = new Show("14:00-16:00", "BacktothefutureII");
            Show s3 = new Show("12:00-14:00", "BacktothefutureIII");


            s1.fillWithFreeTickets(1, 7);
            s2.fillWithFreeTickets(2, 7);
            s3.fillWithFreeTickets(3, 7);
            

            r1.shows.Add(s1);
            r1.shows.Add(s2);
            r2.shows.Add(s3);
            

            pestimozi.rooms.Add(r1);
            pestimozi.rooms.Add(r2);

            Student g1 = new Student("Kovács Béla", pestimozi);
            Old g2 = new Old("Kerényi Gergõ", pestimozi);
            Kid g3 = new Kid("Labanc Máté", pestimozi);
            Frequent g4 = new Frequent("Hal Máté", pestimozi);
            

            g1.buyFree(1, "15:00-17:00");
            g2.reserve(1, "14:00-16:00");
            g3.reserve(2, "12:00-14:00");
            g3.buyReserved(2, "12:00-14:00");
            g4.buyFree(2, "12:00-14:00");



            // use of countBoughtTickets()
            Assert.AreEqual(s1.countBoughtTickets(), 1);
            Assert.AreEqual(s2.countBoughtTickets(), 0);
            Assert.AreEqual(s3.countBoughtTickets(), 2);

            // use of countReservedTickets()
            Assert.AreEqual(s1.countReservedTickets(), 0);
            Assert.AreEqual(s2.countReservedTickets(), 1);
            Assert.AreEqual(s3.countReservedTickets(), 0);

            // use of mostWatchedMovie()
            Assert.AreEqual(pestimozi.mostWatchedMovie(), "BacktothefutureIII");
        }
    }
}