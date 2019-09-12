__Author:__ Quinn Shaner
__Date of Review:__ 9/11/2019
__Sprint Number:__ 2

---
## Quit.cs ##

__Name of .cs file:__ Quit.cs
__Author of .cs file:__ Chase
__Minutes to Review:__ 7

__Specific Comments (Readability):__

Line 1: Changed the namespace of the file to Zelda.Commands, which makes it easier to see that this is specifically a command and not part of the game class itself.

Line 3: Changed 'CommandQuit' to 'Quit', as command is now implied by the folder structure

General: Really simple implementation that tells the reader exactly what the Quit command will do, which is exit the game. 


__Specific Comments (Code Quality):__

No code smells

The actual game variable is kept private and hidden. The constructor for Quit is not null, and all names are succinct. 


__Hypothetical Changes and how current implementation May/Not support change:__

If you changed the 'Execute' method to something like 'Kill', then only when you would call the Quit command would you need to change the code to be 'Quit.Kill' instead of 'Quit.Execute'

---
## ControllerKeyboard.cs ##

__Name of .cs file:__ ControllerKeyboard.cs
__Author of .cs file:__ Chase, Steven
__Minutes to Review:__ 3

__Specific Comments (Readability):__

All variables are short and succint. There are no branching loops, or anything other than what is necessary for this PR right now. 


__Specific Comments (Code Quality):__

A lot of old, unused variables were removed. A lot of features that are not in scope of this PR have also been removed. All variable names and class names follow C# Conventions. 


__Hypothetical Changes and how current implementation May/Not support change:__

If you wanted to changed the key to call 'quit', you would just need to change 2 lines to reflect the new key for quit. Very easy to implement. 

---

## ICharacter.cs ##

__Name of .cs file:__ ICharacter.cs
__Author of .cs file:__ Chase
__Minutes to Review:__ 3

__Specific Comments (Readability):__

There are only 3 methods here. The verbs chosen are all self-descriptive (Spawn, Damage, Kill), and will be easy to add to and implement in classes. 


__Specific Comments (Code Quality):__

The interface retains the C# Coding convention of 'IName'. There is no constructor and there are no variables present. 



__Hypothetical Changes and how current implementation May/Not support change:__

If you change one of the method names, then all classes that implement ICharacter will also need to change their implementing method name. This could end up being a lot of files-- it would be harder to maintain if such a change was needed. 


---
## IEnemy.cs ##

__Name of .cs file:__ IEnemy.cs
__Author of .cs file:__ Chase
__Minutes to Review:__ 3

__Specific Comments (Readability):__

IEnemy has 1 method that clearly states what it does (Attack). You do need to go back to ICharacter to understand that IEnemy inherits Spawn, Damage, and Kill, but these are all self-descriptive. 


__Specific Comments (Code Quality):__

Inheriting from multiple interfaces allows us to have similar classes and then add to them. We end up not rewriting the same code over and over by splitting out the methods that would be common to all. 



__Hypothetical Changes and how current implementation May/Not support change:__

If IEnemy was suddenly no longer inheriting ICharacter, then any implementation of IEnemy would also need to be modified. Further, all IEnemy would be able to do is Move and Attack. It would not be able to Spawn, Kill, or Damage. In short, if you take away one of the interfaces IEnemy inherits from, you completely break the implementation. 

---
## IMoveable.cs ##

__Name of .cs file:__ IMoveable.cs
__Author of .cs file:__ Chase
__Minutes to Review:__ 5

__Specific Comments (Readability):__

All methods are clear and self-explanatory. 

__Specific Comments (Code Quality):__

L5-8: I'm not entirely sure why we need the FaceX methods. They seem redundant. Otherwise, everything follows convention. 



__Hypothetical Changes and how current implementation May/Not support change:__

Remove the FaceX methods and instead implement if/else logic in the concrete classes. Presumably, if you are facing down you are also moving downward. 

---
## INonPlayerCharacter.cs ##

__Name of .cs file:__ INonPlayerCharacter.cs
__Author of .cs file:__ Chase
__Minutes to Review:__ 5

__Specific Comments (Readability):__

We have 2 different methods here, one to write the dialogue, and another to de-render or hide the dialogue. Both names explain their use very effectively. 


__Specific Comments (Code Quality):__

Potentially rename these methods to something like "StartDialog" and "EndDialog" but the names are also clear and follow convention. 



__Hypothetical Changes and how current implementation May/Not support change:__

Change either of the method names, and you would need to change the NPC character implementations. Given that there are not many NPC characters, this would be easy to implement. 

---
## IPlayer.cs ##

__Name of .cs file:__ IPlayer.cs
__Author of .cs file:__ Chase
__Minutes to Review:__ 3

__Specific Comments (Readability):__

The player needs to be able to move, to spawn, damage, and be killed. Further, we have augmented the player by allowing them to use a primary item and a secondary item, both of which are self-explanatory. 


__Specific Comments (Code Quality):__

Conventions of C# are followed and everything is very clear. The intergace inheritance is necessary to avoid clutter. 


__Hypothetical Changes and how current implementation May/Not support change:__

If you did not inherit from the Moveable interface, then IPlayer would need to rewrite those methods. This adds more points of failure, which is not ideal in the long run. 


---
## IProjectile.cs 

__Name of .cs file:__ IProjectile.cs
__Author of .cs file:__ Chase
__Minutes to Review:__ 4

__Specific Comments (Readability):__

Appear and Disappear are clear in meaning, but are perhaps not the strongest verbs we could use. 


__Specific Comments (Code Quality):__

To follow with the naming style of other interfaces, change Appear to Spawn and Disappear to Despawn.


__Hypothetical Changes and how current implementation May/Not support change:__

Since nothing is implementing this yet, changing the method names will be easy to implement and very easily supported. 

---
## ISprite.cs ##

__Name of .cs file:__ ISprite.cs
__Author of .cs file:__ Chase
__Minutes to Review:__ 4

__Specific Comments (Readability):__

Update, Draw, and LoadContent are all required for spritework in general. The Show, Hide, PauseAnimation, and PlayAnimation are all short methods that were added so we have control over the sprites. 


__Specific Comments (Code Quality):__

Chase removed some comments that were no longer necessary. All of the methods follow c# naming conventions and are in a logical order. 


__Hypothetical Changes and how current implementation May/Not support change:__

Potentially, all of the animation methods could have a TotalFrameCount parameter. This would make it easier to mathematically use the texture atlases, but it could also make it harder to maintain in the long run. 

---
## SpriteFixedAnimated.cs ##

__Name of .cs file:__ SpriteFixedAnimated.cs
__Author of .cs file:__ Chase, Steven
__Minutes to Review:__ 4

__Specific Comments (Readability):__

Kept much of the boilerplate code from Sprint0. Right now, none of the methods are implemented, but they **do** all let the user know that they **will** be implemented and that the methods can be used. Further, it helps the developers keep track of what has been done and what still needs to be done. 


__Specific Comments (Code Quality):__

The use of the Exceptions is great. You let the user know the error and why it happened, and also create a skeleton for future development. All of these methods need to be implemented due to inheritence from ISprite, so you need to have the methods in place. That said, this also means we don't return a null value, which will make it easier to add to and test in the future. 


__Hypothetical Changes and how current implementation May/Not support change:__

Instead of a generic error, we could throw customized errors that state which method was called. This would probably require a base string statement which you could modify in each of the methods. Would be easy to implement, but not all that important in the long run for us. 

---
## SpriteFixedStatic.cs ##

__Name of .cs file:__ SpriteFixedStatic.cs
__Author of .cs file:__ Chase, Steven
__Minutes to Review:__ 3

__Specific Comments (Readability):__

Kept a lot of code from Sprint0, but added the placeholder methods for the ISprite inherited methods. Everything is clean and clear-cut. 


__Specific Comments (Code Quality):__

See SpriteFixedAnimated.cs


__Hypothetical Changes and how current implementation May/Not support change:__

Again, could potentially change the error messages to be more specific and less generic. Right now, that isn't needed, but potentially in the future would be good to do. 

---
## SpriteMovingAnimated.cs ##

__Name of .cs file:__ SpriteMovingAnimated.cs
__Author of .cs file:__ Chase, Steven
__Minutes to Review:__ 3

__Specific Comments (Readability):__

Clear, you are only inheriting one class, and everything is organized logically. 

__Specific Comments (Code Quality):__

The exceptions let the user know that there was an error instead of just crashing or silently moving forward. They also let the developers know that there is still more to implement, given that they are placeholder exceptions. 

__Hypothetical Changes and how current implementation May/Not support change:__

The problem with sprites that all inherit from the same source is that it feels like you're rewriting a lot of the same code. That said, it could be possible that all of the sprites move and animate differently, but I wonder if these could be pulled out into some form of master class with helper methods? Probably not ideal though. 

---
## SpriteMovingStatic.cs ##

__Name of .cs file:__ SpriteMovingStatic.cs
__Author of .cs file:__ Chase, Steven
__Minutes to Review:__ 3

__Specific Comments (Readability):__

It's clear, it's clean, and everything is organized logically. The exceptions are clear and quick.

__Specific Comments (Code Quality):__

N/A

__Hypothetical Changes and how current implementation May/Not support change:__

N/A


---
## ZeldaGame.cs ##

__Name of .cs file:__ ZeldaGame.cs
__Author of .cs file:__ Chase, Steven
__Minutes to Review:__ 3

__Specific Comments (Readability):__

Everything was kept pretty much the same from Sprint0. The code itself is less than 100 lines, and everything is spaced out neatly and logically. 

__Specific Comments (Code Quality):__

Removed unneeded classes. All private variables are listed at the top of the file, and the constructor is not empty or null. 

__Hypothetical Changes and how current implementation May/Not support change:__

Eventually, we'll need to manage the gametime, the overall map, and if the game is paused or not. Those should all go in the ZeldaGame class. 
