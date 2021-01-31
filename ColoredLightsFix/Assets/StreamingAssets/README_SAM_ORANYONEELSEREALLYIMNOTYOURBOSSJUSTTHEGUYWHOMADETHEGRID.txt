Rules on populating the scene:

1. You can only use a certain Letters and ONLY in CAPS right now

2. Seperate all letters combos by a slash "/". If it breaks, you used the wrong slash (NO SPACES, ENTERS OKAY)
	ex) letter combos = F:G or YL:S or F:YPP

3a. To get the Text document to work, place it into the "TextLevels" folder 
3b. Then drag from Resources->Prefabs->WorldHolder.prefab into the scene
3c. In the Inspector there is a field for LevelName, this is case-sensitive, must be the same name as something in the TextLevels Folder


SYMBOLS(Letters) LIST:
/ = Seperator, put one in between every letter unless is the last letter in the line
: = Seperator for additional info (See below)

F = open floor tile denote type by ":"
	G = Grass
	WP = Walk Path, just another floor tile
	LP = Lighthouse Path, lighthouses can only be pushed and pulled on this terrain
	YPP, BPP, RPP = (Yellow, Blue, Red) Pressure Plates

X = no tile, no floor, is a pit, is empty (whatever you wanna classify it as)
W = wall, can change direction of wall with "N,S,E,W" (Cardinal directions) after a ":" (W:N)

R* = Rock prefab
P* = player (you can put as many as you want since we dont have movement yet)
YL*, BL*, RL* = (Yellow, Blue, Red) Lighthouse, denote direction with "N,S,E,W"(Cardinal direction) after a ":" (YL:S)

B = Bridge, denote direction by horizontal and vertical (H or V) after a ":" (B:H)
CB = Corner Bridge, denote with opens (Up, Down, Left, Right) (UL,UR,DL,DR)
	^^^for these bridges the creation is "Bridge type:color:direction"
	colors: red "R", blue "B", yellow "Y", Purple "P", Orange "O", Green "G"

*putting a these puts a designated floor tile underneath them, dont O/OP/O or O/LO/O
	LightHouses get Lighthouse Path
	Player and Rock get Grass

**for examples, look into the Level1 and Level2 text docs
**also if you want to see a full level, load my scene up and just press play, it should work