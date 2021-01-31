Rules on populating the scene:

1. You can only use a certain Letters and ONLY in CAPS right now

2. You can only put one Letter per Seperator

3. Seperate all letters by a slash "/". If it breaks, you used the wrong slash (NO SPACES, ENTERS OKAY)

4a. To get the Text document to work, place it into the "TextLevels" folder 
4b. Then drag from Resources->Prefabs->WorldHolder.prefab into the scene
4c. In the Inspector there is a field for LevelName, this is case-sensitive, must be the same 


SYMBOLS(Letters) LIST:
/ = Seperator, put one in between every letter unless is the last letter in the line
: = Seperator for additional info (See below)

O = open floor tile
X = no tile, no floor, is a pit, is empty (whatever you wanna classify it as)
W = wall, can change direction of wall with "N,S,E,W" (Cardinal directions) after a ":" (W:N)
	EX) X/W:N/W:N/X
	    W:W/O/O/W:E     <-- Will make a 2x2 area surrounded by walls (cooresponds to where the wall is in notepad if thats easier)
	    W:W/O/O/W:E
	    X/W:S/W:S/X

P* = player (you can put as many as you want since we dont have movement yet)
L* = Lighthouse, denote direction with "N,S,E,W"(Cardinal direction) after a ":" (L:S)
B = Bridge, denote direction by horizontal and vertical (H or V) after a ":" (B:H)


*putting a these puts a floor tile underneath them, dont O/OP/O or O/LO/O