Ez .NET 4.0 alatt k�sz�lt.

A program ind�t�sa ut�n a "Bet�lt f�jlb�l" gombbal be lehet olvasni egy f�jlt, ami j�t�kokat tartalmaz.
A zip-ben van egy "Jatek.txt" amivel lehet pr�b�lni.
A j�t�kot a list�b�l egy combo boxszal kell kiv�lasztani.

A program t�bla az eg�rrel m�dos�that�, a bal gomb felhoz egy keypad-ot, amib�l
a kiv�lasztott cell�ba egy adatot lehet kiv�lasztani, vagy �t�rni, vagy -- -vel kit�r�lni.
A jobb eg�rgombn�l csak azok a sz�mok jelennek meg, amelyek az adott cell�ba be�rhat�k.
Az "x" - a cancel gomb.

A "Megold�s" nyom�gombra v�gigsz�molja az algoritmust,
hasonl�an a "Megoldhat�s�g" -ra is.
Ha a t�bla hib�s a hib�s cell�kat piros sy�mmal jelen�ti meg.

A "J�t�k ind�t�sa" jelenleg csak annyit csin�l, hogy a s�rga mez�ben egy m�sodperc sz�ml�l�t ind�t,
amit a "J�t�k le�ll�t�s" -sal lehet le�ll�tan.

A t�bla t�rl�s ki�r�ti a t�bl�t.

A SuSolve.cs tartalmazza a megold�si algoritmust �s a hozz� tartoz� rutinokat.


Constants  - n�hany konstans �s a lehets�ges t�blat�pusok le�r�sa �s a j�t�k le�r� oszt�ly
GameFile   - j�t�k f�jl bet�lt�se �s �tad�sa a t�bl�zatba
GameCheck  - a kit�lt�shez ellen�rz�sek. Ezeket majd helyettes�teni kell a SuSolve hasonl� rutinjaival
GameTable  - a cella oszt�ly �s a cell�kat tartalmat� j�t�kt�bla oszt�ly
NumPad	   - a kit�lt�st seg�t� form oszt�ly
TableQueue - a t�bla ment� LI-FO queue oszt�ly
SuSolve    - a C++ -b�l adapt�lt algoritmus -- a r�gi k�dot csak kikommenteztem.

