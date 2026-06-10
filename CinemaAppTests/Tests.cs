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
        [TestMethod]
        public void GetAvailableSeats_NewScreening_EqualsTotalSeats()
        {
            var screening = CreateDefaultScreening();
            Assert.AreEqual(5, screening.GetAvailableSeats());
        }

        [TestMethod]
        public void GetAvailableSeats_FullScreening_EqualsZero()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.AreEqual(0, screening.GetAvailableSeats());
        }

        // ---- GetBookedCount ----

        [TestMethod]
        public void GetBookedCount_AfterBookings()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.AreEqual(2, screening.GetBookedCount());
        }

        [TestMethod]
        public void GetBookedCount_NewScreening_EqualsZero()
        {
            var screening = CreateDefaultScreening();
            Assert.AreEqual(0, screening.GetBookedCount());
        }

        [TestMethod]
        public void GetBookedCount_AfterCancellation_Decreases()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            screening.CancelBooking("Alice");
            Assert.AreEqual(1, screening.GetBookedCount());
        }

        // ---- IsHouseFull ----

        [TestMethod]
        public void IsHouseFull_WhenAllSeatsBooked()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.IsTrue(screening.IsHouseFull());
        }

        [TestMethod]
        public void IsHouseFull_WithAvailableSeats_ReturnsFalse()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            Assert.IsFalse(screening.IsHouseFull());
        }

        [TestMethod]
        public void IsHouseFull_AfterCancellation_ReturnsFalse()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            screening.CancelBooking("Alice");
            Assert.IsFalse(screening.IsHouseFull());
        }

        // -------------------------------------------------------
        // EXTRA FELADAT — Várólista tesztek
        // Az alábbi teszteket csak akkor vedd fel,
        // ha az alap feladattal már végzett vagy!
        // -------------------------------------------------------

        [TestMethod]
        public void AddToWaitingList_WhenFull_ReturnsTrue()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            bool result = screening.AddToWaitingList("Charlie");
            Assert.IsTrue(result);
            Assert.AreEqual(1, screening.GetWaitingListCount());
        }

        [TestMethod]
        public void AddToWaitingList_WhenHasAvailableSeats_ReturnsFalse()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            bool result = screening.AddToWaitingList("Bob");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddToWaitingList_AlreadyBooked_ReturnsFalse()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            bool result = screening.AddToWaitingList("Alice");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddToWaitingList_Duplicate_ReturnsFalse()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            screening.AddToWaitingList("Charlie");
            bool result = screening.AddToWaitingList("Charlie");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CancelBooking_PromotesFirstWaitingUser()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            screening.AddToWaitingList("Charlie");
            screening.AddToWaitingList("David");

            bool cancelResult = screening.CancelBooking("Alice");
            Assert.IsTrue(cancelResult);
            Assert.IsTrue(screening.IsBooked("Charlie"));
            Assert.IsFalse(screening.IsOnWaitingList("Charlie"));
            Assert.IsTrue(screening.IsOnWaitingList("David"));
            Assert.AreEqual(1, screening.GetWaitingPosition("David"));
            Assert.AreEqual(1, screening.GetWaitingListCount());
        }

        [TestMethod]
        public void GetWaitingPosition_NonExistingUser_ReturnsMinusOne()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            screening.AddToWaitingList("Charlie");
            Assert.AreEqual(-1, screening.GetWaitingPosition("David"));
        }

        [TestMethod]
        public void RemoveFromWaitingList_ExistingUser_ReturnsTrue()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            screening.AddToWaitingList("Charlie");
            
            bool removeResult = screening.RemoveFromWaitingList("Charlie");
            Assert.IsTrue(removeResult);
            Assert.IsFalse(screening.IsOnWaitingList("Charlie"));
            Assert.AreEqual(0, screening.GetWaitingListCount());
        }

        [TestMethod]
        public void RemoveFromWaitingList_NonExistingUser_ReturnsFalse()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            
            bool removeResult = screening.RemoveFromWaitingList("Charlie");
            Assert.IsFalse(removeResult);
        }
    }
}
