namespace CinemaApp
{
    public class Screening
    {
        private readonly string _title;
        private readonly int _totalSeats;
        private readonly List<string> _bookedNames;

        // title nem lehet null vagy üres, totalSeats >= 1
        public Screening(string title, int totalSeats)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }
            if (totalSeats < 1)
            {
                throw new ArgumentException("Total seats must be at least 1.", nameof(totalSeats));
            }
            _title = title;
            _totalSeats = totalSeats;
            _bookedNames = new List<string>();
        }

        public string GetTitle()
        {
            return _title;
        }

        // Visszatér false ha nincs szabad hely, vagy a személy már foglalt
        public bool BookSeat(string name)
        {
            if (GetAvailableSeats() <= 0)
            {
                return false;
            }
            if (_bookedNames.Contains(name))
            {
                return false;
            }
            _bookedNames.Add(name);
            return true;
        }

        // Visszatér false ha a személy neve nem szerepel a _bookedNames listában
        public bool CancelBooking(string name)
        {
            if (!_bookedNames.Contains(name))
            {
                return false;
            }
            _bookedNames.Remove(name);
            return true;
        }

        public bool IsBooked(string name)
        {
            return _bookedNames.Contains(name);
        }

        // Szabad helyek = _totalSeats - _bookedNames.Count
        public int GetAvailableSeats()
        {
            return _totalSeats - _bookedNames.Count;
        }

        public int GetBookedCount()
        {
            return _bookedNames.Count;
        }

        public bool IsHouseFull()
        {
            return _bookedNames.Count == _totalSeats;
        }

        // -------------------------------------------------------
        // EXTRA FELADAT — Várólista
        // Az alábbi mezőt és metódusokat csak akkor vedd fel,
        // ha az alap feladattal már végzett vagy!
        // -------------------------------------------------------

        // private readonly List<string> _waitingList;

        // public bool AddToWaitingList(string name)
        // {
        //     throw new NotImplementedException();
        // }

        // public bool RemoveFromWaitingList(string name)
        // {
        //     throw new NotImplementedException();
        // }

        // public bool IsOnWaitingList(string name)
        // {
        //     throw new NotImplementedException();
        // }

        // public int GetWaitingListCount()
        // {
        //     throw new NotImplementedException();
        // }

        // public int GetWaitingPosition(string name)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
