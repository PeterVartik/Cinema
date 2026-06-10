namespace CinemaApp
{
    public class Screening
    {
        private readonly string _title;
        private readonly int _totalSeats;
        private readonly List<string> _bookedNames;
        private readonly List<string> _waitingList;

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
            _waitingList = new List<string>();
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

            if (_waitingList.Count > 0)
            {
                string promotedUser = _waitingList[0];
                _waitingList.RemoveAt(0);
                _bookedNames.Add(promotedUser);
            }
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

        public bool AddToWaitingList(string name)
        {
            if (!IsHouseFull())
            {
                return false;
            }
            if (IsBooked(name))
            {
                return false;
            }
            if (IsOnWaitingList(name))
            {
                return false;
            }
            _waitingList.Add(name);
            return true;
        }

        public bool RemoveFromWaitingList(string name)
        {
            if (!IsOnWaitingList(name))
            {
                return false;
            }
            _waitingList.Remove(name);
            return true;
        }

        public bool IsOnWaitingList(string name)
        {
            return _waitingList.Contains(name);
        }

        public int GetWaitingListCount()
        {
            return _waitingList.Count;
        }

        public int GetWaitingPosition(string name)
        {
            int index = _waitingList.IndexOf(name);
            if (index == -1)
            {
                return -1;
            }
            return index + 1;
        }
    }
}
