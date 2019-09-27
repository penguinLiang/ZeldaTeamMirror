__Author:__ Jarred Fink
__Date of Review:__ 9/25/2019
__Sprint Number:__ 2

---
## EnemyDamagePauseKill.cs ##

__Name of .cs file:__ EnemyDamagePauseKill.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 4

__Specific Comments (Readability):__

The class clearly takes in the IEnemy array and stores it to iterate over. The randomEnemy variable may be better off with a name like arbitraryEnemy or enemyInList. counter could more obviously be an index variable with a name like index or i.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. The only if statement resets a constantly-incremented index variable back to 0.


__Hypothetical Changes and how current implementation May/Not support change:__

d

---
## EnemyDown.cs ##

__Name of .cs file:__ EnemyDown.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 3

__Specific Comments (Readability):__

The class clearly takes in the IEnemy array and stores it to iterate over. It moves and points each enemy down in Execute(), and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Enemy[Direction] classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Only one enemy (Goriya) faces multiple directions. Maybe the FaceDown method will be irrelevant later on as a result.

---
## EnemyLeft.cs ##

__Name of .cs file:__ EnemyLeft.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in the IEnemy array and stores it to iterate over. It moves and points each enemy left in Execute(), and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Enemy[Direction] classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Only one enemy (Goriya) faces multiple directions. Maybe the FaceDown method will be irrelevant later on as a result.

---
## EnemyRight.cs ##

__Name of .cs file:__ EnemyRight.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in the IEnemy array and stores it to iterate over. It moves and points each enemy right in Execute(), and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Enemy[Direction] classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Only one enemy (Goriya) faces multiple directions. Maybe the FaceDown method will be irrelevant later on as a result.

---
## EnemyUp.cs ##

__Name of .cs file:__ EnemyUp.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in the IEnemy array and stores it to iterate over. It moves and points each enemy up in Execute(), and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Enemy[Direction] classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Only one enemy (Goriya) faces multiple directions. Maybe the FaceDown method will be irrelevant later on as a result.

---
## LinkBombAssign.cs ##

__Name of .cs file:__ LinkBombAssign.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 2

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. Execute() sets the player's secondary item to the bomb and then has the player use it, and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Weapon]Assign classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Because of the LinkSecondaryAction command, it may not be necessary for this command to also use the bomb.

---
## LinkBoomerangAssign.cs ##

__Name of .cs file:__ LinkBoomerangAssign.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. Execute() sets the player's secondary item to the boomerang and then has the player use it, and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Weapon]Assign classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Because of the LinkSecondaryAction command, it may not be necessary for this command to also use the boomerang.

---
## LinkBowAssign.cs ##

__Name of .cs file:__ LinkBowAssign.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. Execute() sets the player's secondary item to the bow and then has the player use it, and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Weapon]Assign classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Because of the LinkSecondaryAction command, it may not be necessary for this command to also use the bow.

---
## LinkDamage.cs ##

__Name of .cs file:__ LinkDamage.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. Execute() has the player take damage, and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion.


__Hypothetical Changes and how current implementation May/Not support change:__

N/A

---
## LinkDown.cs ##

__Name of .cs file:__ LinkDown.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. It moves and points the player down in Execute(), and the ToString() override explains the command's functionality for Link.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Direction] classes.


__Hypothetical Changes and how current implementation May/Not support change:__

N/A

---
## LinkLeft.cs ##

__Name of .cs file:__ LinkLeft.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 2

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. It moves and points the player left in Execute(), and the ToString() override explains the command's functionality for Link.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Direction] classes.


__Hypothetical Changes and how current implementation May/Not support change:__

N/A

---
## LinkMagicSwordAssign.cs ##

__Name of .cs file:__ LinkMagicSwordAssign.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. Execute() sets the player's primary item to the magic sword and then has the player use it, and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Weapon]Assign classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Because of the LinkPrimaryAction command, it may not be necessary for this command to also use the magic sword.

---
## LinkPrimaryAction.cs ##

__Name of .cs file:__ LinkPrimaryAction.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. Execute() has the player use its primary action, and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other LinkSecondaryAction class.


__Hypothetical Changes and how current implementation May/Not support change:__

N/A

---
## LinkRight.cs ##

__Name of .cs file:__ LinkRight.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. It moves and points the player down in Execute(), and the ToString() override explains the command's functionality for Link.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Direction] classes.


__Hypothetical Changes and how current implementation May/Not support change:__

N/A

---
## LinkSecondaryAction.cs ##

__Name of .cs file:__ LinkSecondaryAction.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. Execute() has the player use its secondary action, and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other LinkPrimaryAction class.


__Hypothetical Changes and how current implementation May/Not support change:__

N/A

---
## LinkSwordAssign.cs ##

__Name of .cs file:__ LinkSwordAssign.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. Execute() sets the player's primary item to the sword and then has the player use it, and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Weapon]Assign classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Because of the LinkPrimaryAction command, it may not be necessary for this command to also use the sword.

---
## LinkUp.cs ##

__Name of .cs file:__ LinkUp.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. It moves and points the player down in Execute(), and the ToString() override explains the command's functionality for Link.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Direction] classes.


__Hypothetical Changes and how current implementation May/Not support change:__

N/A

---
## LinkWhiteSwordAssign.cs ##

__Name of .cs file:__ LinkWhiteSwordAssign.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The class clearly takes in and stores an IPlayer in its constructor. Execute() sets the player's primary item to the white sword and then has the player use it, and the ToString() override explains the command's functionality.


__Specific Comments (Code Quality):__

The class is very short with few dependencies and high cohesion. It very closely resembles the other Link[Weapon]Assign classes.


__Hypothetical Changes and how current implementation May/Not support change:__

Because of the LinkPrimaryAction command, it may not be necessary for this command to also use the white sword.

---
## ControllerKeyboard.cs ##

__Name of .cs file:__ ControllerKeyboard.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 4

__Specific Comments (Readability):__

Each var has a name that defines it well, and the _keymap Dictionary is written out in a way that is very easy to decipher.


__Specific Comments (Code Quality):__

Lots of vars, but this seems to make sense for a controller class, especially given that almost everything is controlled by the player in this sprint.


__Hypothetical Changes and how current implementation May/Not support change:__

Many of the key mappings will be changed for the next Sprint, and many of the commands will no longer be in the controller once enemies have their own AI. While this will require a lot of lines to be changed, it should not be difficult work.

---
## IEnemy.cs ##

__Name of .cs file:__ IEnemy.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 3

__Specific Comments (Readability):__

The interface is now public.


__Specific Comments (Code Quality):__

Enemies are now publicly accessible to match IMoveable and ISpawnable, which IEnemy extends.


__Hypothetical Changes and how current implementation May/Not support change:__

There are considerations to make the old man an enemy rather than just an NPC because he attacks when provoked. The lack of movement makes the extension of IMoveable irrelevant for that case, however.

---
## IMoveable.cs ##

__Name of .cs file:__ IMoveable.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 2

__Specific Comments (Readability):__

The interface is now public.


__Specific Comments (Code Quality):__

Moveables are now publicly accessible so they can be manipulated by other classes, which reduces the amount of functionality in IMoveable.


__Hypothetical Changes and how current implementation May/Not support change:__

There may be a few enemies or other objects of classes that extend IMoveable that don't actually have movement, which would lead to many no-ops.

---
## IPlayer.cs ##

__Name of .cs file:__ IPlayer.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 2

__Specific Comments (Readability):__

The interface is now public.


__Specific Comments (Code Quality):__

The player is now publicly accessible to match IMoveable and ISpawnable, which IPlayer extends.


__Hypothetical Changes and how current implementation May/Not support change:__

Whereas enemies will spawn based on rooms in the future, the player may have drastically different spawning mechanics, which may change the extension of ISpawnable.

---
## ISpawnable.cs ##

__Name of .cs file:__ ISpawnable.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

The interface is now public.


__Specific Comments (Code Quality):__

Spawnables are now publicly accessible so they can be manipulated by other classes, which reduces the amount of functionality in ISpawnable.


__Hypothetical Changes and how current implementation May/Not support change:__

Handling enemy spawns in each room will be simpler with this interface.

---
## ZeldaGame.cs ##

__Name of .cs file:__ ZeldaGame.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 3

__Specific Comments (Readability):__

The TemporaryLink property and Enemies array were added among and match the other instance variables.


__Specific Comments (Code Quality):__

TemporaryLink makes sense to have as a public property because of how complex Link is. Enemies should be an array in order to show each enemy on screen.


__Hypothetical Changes and how current implementation May/Not support change:__

TemporaryLink is set up to be modified for later sprints. The Enemies array will also be modified when multiple rooms are added.
