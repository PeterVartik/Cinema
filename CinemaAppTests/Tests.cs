using Microsoft.VisualStudio.TestTools.UnitTesting;
using CinemaApp;

namespace CinemaApp.Tests
{
    [TestClass]
    public class ScreeningTests
    {
        private Screening CreateDefaultScreening() => new Screening("Inception", 5);

        // ---- Constructor ----

        [TestMethod]
        public void Constructor_ValidArguments()
        {
            var screening = new Screening("Inception", 100);
            Assert.AreEqual("Inception", screening.GetTitle());
            Assert.AreEqual(100, screening.GetAvailableSeats());
        }

        [TestMethod]
        public void Constructor_InvalidTitle()
        {
            Assert.ThrowsException<ArgumentException>(() => new Screening(null, 10));
            Assert.ThrowsException<ArgumentException>(() => new Screening("", 10));
            Assert.ThrowsException<ArgumentException>(() => new Screening("   ", 10));
        }

        [TestMethod]
        public void Constructor_InvalidTotalSeats()
        {
            Assert.ThrowsException<ArgumentException>(() => new Screening("Inception", 0));
            Assert.ThrowsException<ArgumentException>(() => new Screening("Inception", -1));
        }

        // ---- BookSeat ----

        [TestMethod]
        public void BookSeat_AvailableSeat()
        {
            var screening = CreateDefaultScreening();
            bool result = screening.BookSeat("Alice");
            Assert.IsTrue(result);
            Assert.AreEqual(4, screening.GetAvailableSeats());
        }
        [TestMethod]
        public void BookSeat_WhenFull_ReturnsFalse()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            bool result = screening.BookSeat("Charlie");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BookSeat_DuplicateBooking_ReturnsFalse()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            bool result = screening.BookSeat("Alice");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BookSeat_MultipleBookings_DecreasesAvailableSeats()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            screening.BookSeat("Charlie");
            Assert.AreEqual(2, screening.GetAvailableSeats());
        }

        // ---- CancelBooking ----

        [TestMethod]
        public void CancelBooking_ExistingBooking()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            bool result = screening.CancelBooking("Alice");
            Assert.IsTrue(result);
            Assert.AreEqual(5, screening.GetAvailableSeats());
        }
        [TestMethod]
        public void CancelBooking_NonExistingBooking_ReturnsFalse()
        {
            var screening = CreateDefaultScreening();
            bool result = screening.CancelBooking("Alice");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CancelBooking_AfterCancellation_UserRemoved()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            screening.CancelBooking("Alice");
            Assert.IsFalse(screening.IsBooked("Alice"));
        }

        // ---- IsBooked ----

        [TestMethod]
        public void IsBooked_AfterBooking()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            Assert.IsTrue(screening.IsBooked("Alice"));
        }
        [TestMethod]
        public void IsBooked_UnbookedUser_ReturnsFalse()
        {
            var screening = CreateDefaultScreening();
            Assert.IsFalse(screening.IsBooked("Alice"));
        }

        [TestMethod]
        public void IsBooked_AfterCancellation_ReturnsFalse()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            screening.CancelBooking("Alice");
            Assert.IsFalse(screening.IsBooked("Alice"));
        }

        // ---- GetAvailableSeats ----

        [TestMethod]
        public void GetAvailableSeats_AfterMultipleBookings()
        {
            var screening = CreateDefaultScreening(); // 5 férőhely
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.AreEqual(3, screening.GetAvailableSeats());
        }
        // TODO: újonnan létrehozott vetítésnél a szabad helyek száma egyenlő a totalSeats értékével
        // TODO: teli vetítésnél GetAvailableSeats() nullát kell visszaadni

        // ---- GetBookedCount ----

        [TestMethod]
        public void GetBookedCount_AfterBookings()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.AreEqual(2, screening.GetBookedCount());
        }
        // TODO: újonnan létrehozott vetítésnél GetBookedCount() nullát kell visszaadni
        // TODO: lemondás után a foglaltak száma helyesen csökken

        // ---- IsHouseFull ----

        [TestMethod]
        public void IsHouseFull_WhenAllSeatsBooked()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.IsTrue(screening.IsHouseFull());
        }
        // TODO: szabad hellyel rendelkező vetítésnél false-t kell visszaadni
        // TODO: lemondás után a vetítés már nem teli, IsHouseFull() false-t ad vissza

        // -------------------------------------------------------
        // EXTRA FELADAT — Várólista tesztek
        // Az alábbi teszteket csak akkor vedd fel,
        // ha az alap feladattal már végzett vagy!
        // -------------------------------------------------------

        // [TestMethod]
        // public void AddToWaitingList_WhenFull()
        // {
        //     ...
        // }
        // TODO (extra): szabad hely esetén várólistára kerülés false-t kell hogy visszaadjon
        // TODO (extra): már foglalással rendelkező személy várólistára kerülése false-t kell hogy visszaadjon
        // TODO (extra): ugyanaz a személy kétszer próbál várólistára kerülni, másodszor false-t kap
        // TODO (extra): lemondás után az első várólistás személy automatikusan foglalást kap
        // TODO (extra): GetWaitingPosition nem létező személyre -1-et kell visszaadni
    }
}
