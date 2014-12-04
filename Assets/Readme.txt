WHERE CAN I FIND __ && README

The three features in my games are as follows!
1) Power-ups, at the start of the second level, interacting with the + box will give a player increased hangtime in the air during their jumps.
2) Multiple levels with enemy types. There are two levels with four different enemies. Normal enemies, flying enemies, shielded enemies, and aggressive enemies.
3) Sound effects and background music. No matter what scene the game is opened on, a music player will spawn and persist over the rest of the game.

I also have restoring life, a menu screen that pauses gameplay when brought up, a block skill, platforms that react to/damage players, and animations implamented.

Please ignore all errors unless they're gamebreaking. If they aren't, I know they're there, and what's causing them, but I haven't gotten around to trying to fix them yet!
All sound effects are created using http://www.drpetter.se/project_sfxr.html
BGM is from 8bitcollective used under http://creativecommons.org/licenses/by-nc-sa/3.0/
all art is made by yours truly




AI
	Enemy movement, taking damage, death, etc.

AIphysics
	
	Enemy physics - copied from control (by maxime)

Attack
	Bullet lifespan.

Cam
	Camera movement.

Character
	Player lives, taking damage, death, respawning + changing scenes.

Control
	Creating + firing bullets (the cannon), character physics (by maxime), some camera repositioning.

Menu
	Everything relating to the main menu and gameover menu.

pauseMenu
	Everything related to the menu that comes up in-game.

Platforms
	Everything relating to the jumping platforms.

PlayerGUI
	Everything relating to the main game's GUI apart from the score because I'm dumb.

playSound
	Sound effects and background music.

PowerUp
	Those little "+" boxes. How they work.

saveGame
	Unimplemented savegame features. Please ignore.

Scores
	Information that's passed on to the gameover screen, and the all important SCOOOREEEEE.

Souls
	Controls all the player and monster stats, hit points, energy. 

StartGame
	All things that happen only once, at the start of each game, that don't belong anywhere else.

Teleport
	Advancing to the next area, resetting player's starting position for respawn.

Timer
	Times stuff! Also used for lerp progress.

