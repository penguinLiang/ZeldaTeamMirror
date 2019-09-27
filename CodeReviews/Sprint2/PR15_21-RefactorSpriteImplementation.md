Pull Requests: #15, #21

---
__Author:__ Steven Neveadomi

__Date of Review:__ 9/26/2019

__Sprint Number:__ 2

---
## ISprite.cs ##

__Name of .cs file:__ ISprite.cs 

__Author of .cs file:__ Chase

__Minutes to review:__ 0

Simply removed the LoadContent() method header as it was deprecated by design choices.

---
## Sprite.cs ##

__Name of .cs file:__ Sprite.cs

__Author of .cs file:__ Chase

__Minutes to review:__ 5


__Specific Comments (Readability):__

Although complicated at first glance, the code itself after looking at it is very well thought out and readable. Variables are very clearly named and the intentions are clear.

__Specific Comments (Code Quality):__

Very high quality code. Functional, efficient, usable, low coupling, high cohesion, *insert buzz word*. Overall, the code works properly and minimizes the amount of classes we will use later on.

__Hypothetical Changes and how current implementation May/Not support change:__

Perhaps a bit of a documentation write up on the proper use of the constructor and the parameters, but besides that nothing.


This file was the big chunk of the commit and the major design component.

---
## ZeldaGame.cs ##

The only changes in this file were the removal of the old Sprite implementation and one line of code to implement the new Sprite implementation. Nothing to review as it is temporary code and will be overwritten by later PRs.


---
## Zelda.csproj ##

This file was changed automatically to adjust for the addition of Sprite.cs and the removal of the four deleted classes mentioned later in this file.

---
## Deleted Files ##
__SpriteFixedAnimated.cs__

__SpriteFixedStatic.cs__

__SpriteMovingAnimated.cs__

__SpriteMovingStatic.cs__

The four aforementioned files were deleted due to being deprecated by the addition of the Sprite.cs file.



