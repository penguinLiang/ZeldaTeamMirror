__Author:__ Quinn Shaner
__Date of Review:__ 9/11/2019
__Sprint Number:__ 2

---
## Quit.cs ##

__Name of .cs file:__ Quit.cs
__Author of .cs file:__ Chase
__Minutes to Review:__ 7

__Specific Comments (Readability):__

Line 1: Change the namespace to Zelda.Commands, which makes it easier to see that this is specifically a command and not part of the game class itself.

Line 3: Change 'CommandQuit' to 'Quit', as command is now implied by the folder structure

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
__Minutes to Review:__

__Specific Comments (Readability):__

Appear and Disappear are clear in meaning, but are perhaps not the strongest verbs we could use. 


__Specific Comments (Code Quality):__

To follow with the naming style of other interfaces, change Appear to Spawn and Disappear to Despawn.


__Hypothetical Changes and how current implementation May/Not support change:__

Since nothing is implementing this yet, changing the method names will be easy to implement and very easily supported. 

---
## ISprite.cs ##

__Name of .cs file:__
__Author of .cs file:__
__Minutes to Review:__

__Specific Comments (Readability):__




__Specific Comments (Code Quality):__





__Hypothetical Changes and how current implementation May/Not support change:__

---
## SpriteFixedAnimated.cs ##

__Name of .cs file:__
__Author of .cs file:__
__Minutes to Review:__

__Specific Comments (Readability):__




__Specific Comments (Code Quality):__





__Hypothetical Changes and how current implementation May/Not support change:__

---
## SpriteFixedStatic.cs ##

__Name of .cs file:__
__Author of .cs file:__
__Minutes to Review:__

__Specific Comments (Readability):__




__Specific Comments (Code Quality):__





__Hypothetical Changes and how current implementation May/Not support change:__

---
## SpriteMovingAnimated.cs ##

__Name of .cs file:__
__Author of .cs file:__
__Minutes to Review:__

__Specific Comments (Readability):__




__Specific Comments (Code Quality):__





__Hypothetical Changes and how current implementation May/Not support change:__

---
## SpriteMovingStatic.cs ##

__Name of .cs file:__
__Author of .cs file:__
__Minutes to Review:__

__Specific Comments (Readability):__




__Specific Comments (Code Quality):__





__Hypothetical Changes and how current implementation May/Not support change:__

