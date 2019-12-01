# CSE 3902  
# Team: RiotSquad  
## Members: Chase Colman, Jarred Fink, Steven Neveadomi, Quinn Shaner, Henry Xiong  
  
## GOAL: To Implement the First Dungeon of the Original Legend of Zelda NES Game  
  
To this end, we are using Visual Studio and Monogame to recreate the dungeon using C#.   
  
# Technical Details  
  
## Current Design  
  
Currently, for keyboard controls we are using the Command Design Pattern.  
We are then using a State Machine for the individual enemy groups, in addition to the player.   
  
We have broken up our implementation by __nouns__, such as 'enemies', 'items', 'projectiles'. This was done to seperate the concerns of each of these sprite groups, and to break up the work into distinct pieces.  
  
- As a team, we decided to use the command pattern for all collision effects
- After talking to Dr. Boggus and Ian, we determined that projectiles, while implemented, did not need to have collisions in sprint 3.
- The Enemies have a basic AI implemented
- Used the game mapping program Tiled to create our maps and export them as CSVs
- Large classes were broken into smaller, more manageable classes.
- Implemented basic design for Doors, and Barriers
- Created every room and a few debug rooms
- Dark mode for the game has been implemented. 
- Survival mode for the game has been implemnted, more details down below.

__NEW (and improved):__
ZeldaGame received a major refactor so that it mostly loads assets and defers all stateful game management to GameStateAgent.  
Each state, paused, playing, game over, game win, etc. all have their own worlds that make integrating different keyboard, mouse, update, and draw behaviors more abstract.

ZeldaGame has again received a major refactor, this time involving the camera system. Instead of a stagnant camera that shows the entire room all at once, the camera now focuses on Link to fit in with the much larger survival map.

- Survival Mode! Featuring:
- New camera centered on Link.

- More code cleanup!
Enemy AI has been cleaned up and improved.

A lot of code cleanup as well regarding item and weapon classes.

Finally, the last major source of cleanup was the camera system so that it was edited to focus on Link.

Overall, a lot of cleanup here and there.

- Survival mode has a huge survival map and shop map.

- Survival mode features many waves of enemies, where every wave is progressed after all the enemies are slain.

There are really just three types of waves: Party, Normal and Shop.

Normal - A regular, normal wave. It plays out normally and ends when everything is dead.

Shop - Every few waves the player will be able to go to the shop to buy items/weapons.

Party - The player becomes invincible for the entire wave. A breather wave, if you will.

- Survival dungeon also features pickup items that the player can collect! (More details in the NEW ITEMS section)

- High score system. What's the point of having this mode if there is no high score system?
The player is allowed to input their name, which will automatically be logged into an online database that we have set up. It will be put onto the leaderboard if it makes it on there to be compared against all the other players who have logged their score onto the leaderboard. 

- Mode select menu at the start! Handy.
Now upon starting up the game, the player will be able to select which mode of the game they want to play, from dungeon to survival to dark mode. It will be the NES Zelda main menu screen in style.

- New enemy(s) have been implemented for survival exclusively! Okay just one enemy.
Fygar - A sprite from dig dug that is green. It is a terrifying enemy, able to relentlessly pursue the player and shoot fireballs constantly. It is dangerous and can only be found in survival.

- What's this? Unlockable barriers barring the player from another segment of the survival dungeons unless they have a key?
Barriers have been added. In the shop room, they are barriers that require keys to unlock, while in the dungeon survival room, they are barriers that require rupees to unlock.

- NEW WEAPONS. We have crazy weapons exclusive to only survival!

White Sword - Upgrade to sword for attack damage

Magic Sword - Upgrade to sword for attack damage

Silver Arrow - Upgrade to bow/arrow for attack damage

Firebow - Shoots out three projectiles in a cone. One arrow down the center and two fireballs in a V shape. 

Bomb Launcher - Launches bombs across the room

Alchemy Coin - Shoots out diagonally from the player, bounces at maximum four times and gains rupees for the player when it hits an enemy.

Around the World Boomerang - Boomerang that goes around the world. It will exit out the edge of the screen and come back on the other edge of the screen, whether horizontally or vertically.

- NEW ITEMS. We have just as crazy but not as crazy items exclusive again to survival!

Bomb Upgrade - Doubles the maximum bomb count

Rupee Upgrade - Changes rupee value from one to three, and from five to ten.

Wallet Upgrade - Doubles the maximum rupee count

Laser beam - Shoots out a gigantic laser beam that either goes vertically or horizontally 

Bait - Attracts enemies closer to the bait

Clock - Freeze time and have enemies completely be stilled for a moment

Star - Makes the player invincible for a small amount of time

- Working shop system in survival, cool huh?
Player will be able to walk over and buy an item as long they have enough rupees. Some items are bought only once while others will keep respawning!

- Enemy related code refactoring for survival! Refactoring is nice. Refactoring is good.
Enemy will now target the player and move towards the player, but only if the player is within a certain range.

- PARTY MODE. All monsters will all dance vividly in style because all these years rotting in the dungeon has ripened their dancing abilities.

- DARK MODE?! We basically mashed together dark souls into the game but not really because you can only see in front of Link directly, which increases the difficulty dramatically.

## Sprint 2 Details  
  
Chase created base interfaces for the rest of us to use-- IMoveable, ISpawnable, IEnemy, ISprite, IPlayer, and INonPlayerCharacter. Chase has also implemented the player class, and refactored the sprite implementation.  
  
Jarred created texture atlases for all sprites, and resolved any major conflicts after everything was implemented. Jarred also learned how to use Git.  
  
Steven implemented NPC classes, Enemy classes and refactored Controllers. Both he and Chase have been leading discussions on design implementations.  
  
Quinn created the Readme, implemented item classes and implemented projectiles.   
  
Henry implemented the Block classes, and implemented the keyboard input and commands. Henry has also provided support for Chase.  
  

## Sprint 3 Details

Chase did a majority of the initial code reviews. He also created the initial design pattern and blueprint for all of us to work off of. It was invaluable to getting the project done. Chase created tilesets, the Dungeon Manager, Scene, and CSV asset pipeline library for MonoGame. In addition, Chase fixed a lot of bugs that came up and integrated the scene controller with everything else. Also designed the collision loop system and implemented item collision handling.

Jarred noticed a couple of visual bugs at the start of the sprint ⁠— items weren't aligned to the center of the box grid, and we were missing a few dungeon tiles. Jarred added an inventory system to Link to manage items. Jarred also worked on the projectile collisions and the behavior for the boomerang when thrown by the player and the Goriya. Jarred discovered a few other minor bugs and provided solutions that could be implemented.

Steven worked mainly on Enemies this sprint. He implemented the death and spawn effects, a basic enemy AI, created an agent for the Stalfos enemy, and ensured that both the player and the enemies could collide and interact with the current room. Steven also designed the Agent pattern. Both Steven and Chase determined how the scene would interact with both the player and enemies.

Quinn designed the command pattern for use with the Collideables, implemented Player Death and health checks, and implemented all item collisions. Quinn also created classes for all of the barriers and helped debug when needed. Quinn split up the sprint into main tasks and created most of the initial cards. Quinn did a lot of regression testing and double checking to ensure they were using the correct patterns.

Henry created classes for doors and stairs, the Room Loader, and the Jump Mini-Map Screen. The Jump Mini-Map Screen was a major component of this sprint, and required a lot of moving parts to be implemented before Henry could work on it. Henry also reviewed a lot of the pull requests as they happened. 

## Sprint 4 Details

We started tracking our bugs as issues instead of using Discord and GitHub issues, as that way they were easier to keep track of and manage.

Chase implemented the HUD, the environment and menu music, weapon use sounds, the bombable walls, the old man room puzzle, the room transition animation, and the game state agent. Chase helped Quinn with projectile collision handling and activatable doors. Chase also fixed several bugs, including the _case of the missing sand_ and the _bummed out boomerang_ (it just kind of stopped, but now it keeps going!).

Jarred created the sword projectile with particle effect, the HUD and pause screen sprite factories, the fireball projectile from the old man, the enemy-dropped items and the relative chance to drop specific items, the hurt/death sounds, environment sound effects, and activatable items. Jarred removed the useless doors in the debug rooms. Jarred also removed the items from the starting room, updated the keybindings, and made the damage system more accurate to the original game.

Steven improved the background music's loop and Link's movement to be more accurate to the original game by aligning it on the tile grid. Steven added the old man and the aquamentus' attacks, fixed the missing dialogue in the old man's room, added item pickup sounds, and completely overhauled the enemy AI to be as accurate to the game as possible. Steven also helped Henry with activatable blocks and Quinn with the projectile implementation. 

Quinn established the projectile deployment system and collision handling, exposed links health, setup the entire sound management system, and created the game over and game win menu screens. Quinn also made it so the triforce triggers a win and that bombs/rupees are properly depleted upon use.

Henry made the normal doors actually function like doors-- the doors now teleport you to the right room and exposes the room to the game state, and have a narrower collision field, as well as the activatable puzzle block, the deceptively involved pause inventory screen (selecting an item, keeping track of visited rooms, keeping track of specific items), and the stairs to/from the basement.

## Spring 5 Details

We just went as usual. Sprint 5 seemed more challenging on average because there were a lot of new things we had to implement or change that were based on our older system.

Chase has worked on the high score name input database, which is the ability to save your high score with a specific name that puts all your valuable score in the database. He has also researched and set up a database for high scores, as well as designed a high score system and everything related to high scores. Party mode has also been implemented by Chase in addition to the cone of view for dark mode. Finally he has done a lot of major refactoring in the code base overall just for survival mode. Additionally he has done a ton of SurvivalManager work, implemented shaders and did a lot of bug fixing.

Jarred has handled the survival mode camera, which was centering the camera only on Link. Furthermore, he has worked on the mode select menu at the very start of the game, as well as implementing a high score board GUI. Most of all, Jarred has implemented a TON of new weapons and the graphics related to survival mode relating to that. He is also the one who handled the high score name input menu, versus the high score database, which was set up by Chase. In addition to Quinn, he also implemented a lot of the new items along with the weapon functions. He was also able to do quick PR reviews and bug fixes.

Steven has also done a maximal amount of code refactoring for this sprint, and further optimised a lot of things, such as enemy AI just to handle survival mode well, implementing a new enemy and designing the dungeon for survival mode overall. He has further also integrated dungeon and shop into SurvivalManager and handled a lot of important core files such as SurvivalScene and SurvivalRoom. Steven has also guided what SurvivalManager was supposed to be designed like and lead team discussions on it, and as well as directing Henry on the different aspects of SurvivalManager integration with WaveManager.

Quinn has done shop manager and everything shop related for survival mode, including designing the shop, and handling all the buyable items, which are not just related to items, but also including things such as buyable barriers in Survival Mode, and making sure they are unlockable using different items such as rupees or keys. She has also worked with Jarred for the shop design placement, designed the shop manager, and she also created stubs at the beginning so everybody else could work on the code without blockers.

Henry is one of the contributors to the SurvivalManager file, as well as making the core handler for the wave spawning algorithm, WaveManager. The enemy spawning AI is handled in there. The storage file Wave was also made so that it can be handled by many various files. Changing the parsing tool that Monogame uses around so that it would take in wave text files have also been made by Henry.

## Code Reviews  
  
The team, as a whole, has decided that Code Reviews will be kept in a central branch on each sprint. This central branch will get merged at the end of every sprint.  
Every major code review will be a file, consisting of both a review for readability and maintainability. The code review file will be broken down by file.  
  
~~__Major Code Reviews__ take place on a pull request. This code review will be labeled like so:~~  
  
~~PR#-NameOfPRBranch~~   
  
~~Every code review file will have every file that was in the particular PR listed and detailed, file by file.~~  

For this sprint and the previous sprint, most of our code reviews were done directly on PRs. Instead of doing Major Code Reviews on each PR, each team member did an in-depth review of a single file. These reviews are in the Sprint4 folder.
  
In addition, every Sprint will have it's own subfolder in the CodeReview folder.   
  
Further, the team has agreed that all members of the team should attempt to look at the PR, and one member will be assigned the in depth code review. As a member is looking at the PR, they are able to comment on specific lines of code for clarification, changes, or suggestions. These comments are kept in the PR, and typically short enough that they are not included in the Major Code Review file for that PR unless a change was made.  

## Code Analysis

Code Analysis results before and after fixes for each sprint can be found under the Code Analysis folder.
  
----
# General Information  
### Known bugs, controls, extra processes  
  
## Controls  

__Q__: Quit
__R__: Reset

__Z__: Primary Attack (Sword)  
__X__: Secondary Attack (Bow, Boomerang, Bomb)

__W/UP__: Move Link Up  
__A/LEFT__: Move Link Left  
__S/DOWN__: Move Link Down  
__D/RIGHT__:  Move Link Right  

__SPACE__: Pause the game and bring up the inventory screen

__For debugging__:
__2__: Upgrade to White Sword  
__3__: Upgrade to Magical Sword  
__4__: Assign and Use the Bow as the Secondary Weapon  
__5__: Assign and Use the Boomerang as the Secondary Weapon  
__6__: Assign and Use the Bomb as the Secondary Weapon  

__M__: Open/close the jump map. _Click on a room to teleport there_  

## Cheats

__Activate Party Mode (PARTY HARD)_:_
UP, UP, DOWN, DOWN, LEFT, RIGHT, LEFT, RIGHT, B, A, ENTER
  
## Frame Rates  
__Normal Frame Rate__: Frame rate usually used to cycle between animation frames of a sprite  
__Hurt Frame Rate__: Appears to be twice as fast as the normal frame rate, and used to swap palettes  
  
Individual sprites are aligned at multiples of 8, and most are aligned at multiples of 16.   
Dimensions of sprites are also in multiples of 8 or 16.   
  
Boss (48 X 256), individual frame: 24 X 32  
Doors (160 X 128), individual frame: 16 X 16  
Enemy Goriya (32 X 256), individual frame: 16 X 16  
Enemy Hand (32 X 48), individual frame: 16 X 16  
Enemy Other (32 X 48), individual frame: 16 X 16  
Enemy Skeleton (32 X 64), individual frame: 16 X 16  
Field Weapons (64 X 136), individual frame: 16 X 16, rows 1-4 swords, 7-8 fireballs  
	Boomerang (64 X 136), individual frame: 8 X 8, row 9  
Items (32 X 192), individual frame: 8 X 16 or 16 X 16  
Red/Blue Hearts (32 X 192), individual frame: 8 X 8  
Link No Weapon (32 X 288), individual frame: 16 X 16  
Link Sword (128 X 384), Link facing up/down: 16 X 32, Link facing left/right: 32 X 16  
Link Use Secondary (64 X 64), individual frame: 16 X 16  
Old Man (16 X 64), individual frame: 16 X 16  
Particles (64 X 120), individual frame: 16 X 16  
Tiles (32 X 80), individual frame: 16 X 16  
  
__Note on Sprite Animation__: The Sprite method takes in the Spritesheet, the width and height of the specific sprite, the frame count for animations, the offset from the spritesheet, and optionally takes in a frame delay, palette height, palette count, and palette shift delay.   
Palette shifts most often occur when an entity is damaged, and thus not a required parameter for most sprites. 

## Enemies  
In regard to current design, each monster has its own class which then has its own agent class.   
The agent class is roughly an expanded state machine that includes the drawing logic based on the state.  
   
The following are all the monsters currently implemented with their behavior explained:  
 - __Stalfos__: The skeleton. Takes 2 hits and dies. Stunned by boomerang.
 - __Keese__: The bat. Dies instantly.
 - __Wall Master__: The hand. Takes 2 hits and dies. Stunned by boomerang. 
 - __Goriya__: The goblin. Takes 3 hits and dies. Can throw a boomerang. Stunned by boomerang.
 - __Trap__: The blue cross. Has no health and can't be damaged.
 - __Aquamentus__: The dragon. Faces only one direction in the game. Takes six hits and dies. Boomerang does nothing. 
 - __Gel__: The gel drop. Dies instantly.
 - __Old Man__: The old man. Takes damage but is immortal.

  
## Bugs
~~Please check out our [bug report](bugs.md)~~
__NEW:__
We moved to tracking our bugs using the issue tracker on GitHub.
  
## Sprite Resources  
  
- __Link__: https://www.spriters-resource.com/nes/legendofzelda/sheet/8366/  
- __Dungeon Enemies__:  https://www.spriters-resource.com/nes/legendofzelda/sheet/31806/  
- __Items and Weapons__: https://www.spriters-resource.com/nes/legendofzelda/sheet/54720/  
- __NPCs__: https://www.spriters-resource.com/nes/legendofzelda/sheet/21189/  
- __Bosses__: https://www.spriters-resource.com/nes/legendofzelda/sheet/36632/  
- __Dungeon Tileset__: https://www.spriters-resource.com/nes/legendofzelda/sheet/8376/  
- __HUD & Pause Screen__: https://www.spriters-resource.com/nes/legendofzelda/sheet/119278/  
- __Alchemy Coin__: https://www.spriters-resource.com/pc_computer/shovelknight/sheet/67118/
- __Laser Beam__: https://steredenn-game.tumblr.com/post/98397504410/steredenn-making-an-expandable-laser
- __Fygar__: https://www.spriters-resource.com/nes/digdug/


## Sound Resources
- __Background Music__: https://downloads.khinsider.com/game-soundtracks/album/the-legend-of-zelda-nes
- __Sprite Sound Effects__: http://noproblo.dayjo.org/ZeldaSounds/LOZ/index.html
- __Laser Beam Sound Effect__: https://www.myinstants.com/instant/mega-man-x-charge-buster-shot/
  
## Debug Rooms
There are four debug rooms, each with their own element of the game to test.
- __3-5__: Enemies
- __4-0__: Items
- __4-5__: Movable Blocks
- __5-5__: Locked Doors

## Format of Survival Mode CSV Files:
Every single line is one unique wave.
Should be formatted every line as: "[Wave Type],[Enemy Type]:[Number of that enemy],...,[Enemy Type]:[Number of that enemy]"
Enemy Types must be in lowercase form and they are the names of the enemies. Not all enemies are in, as some enemies are left out of the survival mode.
The wave types are:
"D" - Default, normal wave of monsters
"S" - Shop wave
"P" - Party mode
    
## Extra Processes  
  
- Our method for pull requests and reviews have been expanded from the default of the class.   
- We are using an expanded form of the State Machine.  
- We are using Git, and all of the tools associated with Git.   
- All spritesheets have been configured to be texture atlasses.   
- We are meeting outside of class at least 2 times a week, for around an hour per meeting.  
- We have included more items and enemies than the default. 
- We have augmented the state machines further with agents
- We are using the command pattern for collision effects
- We have a Dungeon Room loader, Manager, and Scene to implement the game world
- We broke down the Sprint into goals, then tasks, and then assigned point values to each task and ensured that the points were roughly evenly distributed  
- Interfaces were broken down by usage rather than by type ⁠— allowing shared utility to be implemented in separate concrete classes.  
- We continued the use of agents and expanded agents for use in the game state
- We have multiple states for the game world
- We have implemented a set of rules/guidelines for both meetings and scheduling 
