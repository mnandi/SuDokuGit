Ez .NET 4.0 alatt készült.

A program indítása után a "Betölt fájlból" gombbal be lehet olvasni egy fájlt, ami játékokat tartalmaz.
A zip-ben van egy "Jatek.txt" amivel lehet próbálni.
A játékot a listából egy combo boxszal kell kiválasztani.

A program tábla az egérrel módosítható, a bal gomb felhoz egy keypad-ot, amibõl
a kiválasztott cellába egy adatot lehet kiválasztani, vagy átírni, vagy -- -vel kitörölni.
A jobb egérgombnál csak azok a számok jelennek meg, amelyek az adott cellába beírhatók.
Az "x" - a cancel gomb.

A "Megoldás" nyomógombra végigszámolja az algoritmust,
hasonlóan a "Megoldhatóság" -ra is.
Ha a tábla hibás a hibás cellákat piros syámmal jeleníti meg.

A "Játék indítása" jelenleg csak annyit csinál, hogy a sárga mezõben egy másodperc számlálót indít,
amit a "Játék leállítás" -sal lehet leállítan.

A tábla törlés kiüríti a táblát.

A SuSolve.cs tartalmazza a megoldási algoritmust és a hozzá tartozó rutinokat.


Constants  - néhany konstans és a lehetséges táblatípusok leírása és a játék leíró osztály
GameFile   - játék fájl betöltése és átadása a táblázatba
GameCheck  - a kitöltéshez ellenõrzések. Ezeket majd helyettesíteni kell a SuSolve hasonló rutinjaival
GameTable  - a cella osztály és a cellákat tartalmató játéktábla osztály
NumPad	   - a kitöltést segítõ form osztály
TableQueue - a tábla mentõ LI-FO queue osztály
SuSolve    - a C++ -ból adaptált algoritmus -- a régi kódot csak kikommenteztem.

