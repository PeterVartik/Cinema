# Mozi-vetítés (CinemaApp)

## Áttekintés
C# mozi-vetítés projekt, megírt tesztekkel és implementált logikával.

---

## Fontos szabályok
- `BookSeat()` — foglalás csak akkor sikeres, ha van még szabad hely és a személy még nem foglalt.
- `CancelBooking()` — lemondás után az első várólistás személy automatikusan megkapja a helyet és kikerül a várólistáról.
- `GetAvailableSeats()` — szabad helyek száma a vetítés összes férőhelye mínusz a foglalások száma.
- `IsHouseFull()` — akkor igaz, ha a foglalások száma megegyezik az összes férőhelyével.

---

## Projektstruktúra
- `CinemaApp/` (fő kód)
- `CinemaAppTests/` (tesztek)

---

## Lépések
1. **Klónozás** megvolt
2. **Kezdeti tesztek futtatása**
3. **Hiányzó tesztek** megírva
4. **Osztályok implementálása** megírva
5. **Minden teszt zöld**
