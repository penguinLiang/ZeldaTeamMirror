__Author:__ Henry Xiong
__Date of Review:__ 9/21/2019-9/22/2019, edit on 9/24/2019
__Sprint Number:__ 2

## ItemSpriteFactory.cs ##

__Name of .cs file:__ ItemSpriteFactory.cs
__Author of .cs file:__ Quinn
__Minutes to review:__ 1

__Specific Comments (Readability):__
Very readable, every single function inside of the factory clearly does what a factory is supposed to do, and that is to return an ISprite.

__Specific Comments (Code Quality):__
Simple, and easy to maintain looking. A very good job.

__Hypothetical Changes and how current implementation May/Not support change:__
There is no hypothetical changes for now and the current implementation has very low maintainability, will be easy to update.

---
## ZeldaGame.cs ##

__Name of .cs file:__ ZeldaGame.cs
__Author of .cs file:__ Entire team
__Minutes to review:__ 5

__Specific Comments (Readability):__
In terms of readability, the function trying to be prescribed here is readable. Trying to go through all the sprites.

__Specific Comments (Code Quality):__
Only maintainability comes from adding in more and more ISprites into the giant array, that is about it.

__Hypothetical Changes and how current implementation May/Not support change:__
The implementation is correct, and everything seems to be in order as long the window borders gets checked and none of the sprites overlap any of the window borders. Otherwise, it seems perfectly fine in terms of updating, drawing and formatting the sprites right on the screen.
