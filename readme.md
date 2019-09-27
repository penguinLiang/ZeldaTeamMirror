# CSE 3902  
# Team: RiotSquad  
## Members: Chase Colman, Jarred Fink, Steven Neveadomi, Quinn Shaner, Henry Xiong  
  
## GOAL: To Implement the Fist Dungeon of the Original Legend of Zelda NES Game  
  
To this end, we are using Visual Studio and Monogame to recreate the dungeon using C#.   
  
# Technical Details  
  
## Current Design  
  
Currently, for keyboard controls we are using the Command Design Pattern.  
We are then using a State Machine for the individual enemy groups, in addition to the player.   
  
We have broken up our implementation by __nouns__, such as 'enemies', 'items', 'projectiles'. This was done to seperate the concerns of each of these sprite groups, and to break up the work into distinct pieces.  
  
## Sprint 2 Details  
  
Chase created base interfaces for the rest of us to use-- IMoveable, ISpawnable, IEnemy, ISprite, IPlayer, and INonPlayerCharacter. Chase has also implemented the player class, and refactored the sprite implementation.  
  
Jarred created texture atlases for all sprites, and resolved any major conflicts after everything was implemented. Jarred also learned how to use Git.  
  
Steven implemented NPC classes, Enemy classes and refactored Controllers. Both he and Chase have been leading discussions on design implementations.  
  
Quinn created the Readme, implemented item classes and implemented projectiles.   
  
Henry implemented the Block classes, and implemented the keyboard input and commands. Henry has also provided support for Chase.  
  
## Code Reviews  
  
The team, as a whole, has decided that Code Reviews will be kept in a central branch on each sprint. This central branch will get merged at the end of every sprint.  
Every major code review will be a file, consisting of both a review for readability and maintainability. The code review file will be broken down by file.  
  
__Major Code Reviews__ take place on a pull request. This code review will be labeled like so:  
  
PR#-NameOfPRBranch  
  
Every code review file will have every file that was in the particular PR listed and detailed, file by file.   
  
In addition, every Sprint will have it's own subfolder in the CodeReview folder.   
  
__Further__ the team has agreed that all members of the team should attempt to look at the PR, and one member will be assigned the in depth code review. As a member is looking at the PR, they are able to comment on specific lines of code for clarification, changes, or suggestions. These comments are kept in the PR, and typically short enough that they are not included in the Major Code Review file for that PR unless a change was made.  
  
----
# General Information  
### Known bugs, controls, extra processes  
  
## Controls  

__N__: Primary Attack (Sword) 
__Z__: Primary Attack (Sword)
__1__: Assign and Use Sword as Primary Weapon
__2__: Assign and Use the White Sword as the Primary Weapon
__3__: Assign and Use the Magic Sword as the Primary Weapon
__4__: Assign and Use the Bow as the Secondary Weapon
__5__: Assign and Use the Boomerang as the Secondary Weapon
__6__: Assign and Use the Bomb as the Secondary Weapon
__W/UP__: Move Link Up
__A/LEFT__: Move Link Left
__S/DOWN__: Move Link Down
__D/RIGHT__:  Move Link Right
__E__: Apply Damage
__U__: Move all enemies up 
__H__: Move all enemies left
__J__: Move all enemies down
__K__: Move all enemies right
__T__: Spawn all enemies.
__Y__: Damage* all enemies.
__I__: Kill all enemies.
  
  
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
  
  ### Note on Sprite Animation: The Sprite method takes in the Spritesheet, the width and height of the specific sprite, the frame count for animations, the offset from the spritesheet, and optionally takes in a frame delay, palette height, palette count, and palette shift delay.   
  Palette shifts most often occur when an entity is damaged, and thus not a required parameter for most sprites. 

## Enemies  
All enemies were implemented as closely to the source as possibly.   
In regard to current design, each monster has its own class which then has its own "agent" class.   
The agent class is roughly an expanded state machine geared towards AI / logic, and a few of the monsters share very similar and redundant agent code (e.g. KeeseAgent.cs and GelAgent.cs).  
This is purposeful and temporary as going forward there will be logic / AI implemented within the agent which will be specific to each monster and uses the current quasi skeleton code.   
It would be a waste to make a BasicAgent.cs only to delete it for the next sprint albeit there will be redundant code temporarily.  
__This was approved by the grader.__  
   
The following are all the monsters currently implemented with their behavior explained.  
__Stalfos__: The skeleton. Has a damaged sprite. Constant animation.  
__Keese__: The bat. Dies instantly, no damaged sprite. Constant animation.  
__WallMaster__: The hand. Has a damaged sprite. Constant animation.  
__Goriya__: The goblin. Has four different sprites for each direction and constant animation. Has a damaged sprite.  
__Trap__: The blue cross. Has no health and can't be damaged, still sprite.  
__Aquamentus__: The dragon. Faces only one direction in the game.  
__Gel__: The gel drop. Dies instantly, no damaged sprite. Constant animation.  
__Old_Man__: The old man. No animation, but a damaged sprite. Won't die from damage.  
  
## Bugs
Please check out our [bug report](bugs.md)
    
  
## Sprite Resources  
  
__Link__: https://www.spriters-resource.com/nes/legendofzelda/sheet/8366/  
__Dungeon Enemies__:  https://www.spriters-resource.com/nes/legendofzelda/sheet/31806/  
__Items and Weapons__: https://www.spriters-resource.com/nes/legendofzelda/sheet/54720/  
__NPCs__: https://www.spriters-resource.com/nes/legendofzelda/sheet/21189/  
__Bosses__: https://www.spriters-resource.com/nes/legendofzelda/sheet/36632/  
__Dungeon Tileset__: https://www.spriters-resource.com/nes/legendofzelda/sheet/8376/  
  
    
## Extra Processes  
  
Our method for pull requests and reviews have been expanded from the default of the class.   
We are using an expanded form of the State Machine.  
We are using Git, and all of the tools associated with Git.   
All spritesheets have been configured to be texture atlasses.   
We are meeting outside of class at least 2 times a week, for around an hour per meeting.  
We have included more items and enemies than the default.  
