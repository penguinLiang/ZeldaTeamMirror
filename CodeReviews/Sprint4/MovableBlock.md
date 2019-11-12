---
__Author:__ Chase

__Date of Review:__ 11/10/19

__Sprint Number:__ 4

---
## NAME OF FILE  ##

__Name of .cs file:__ Blocks/MovableBlock.cs

__Author of .cs file:__ Henry

__Minutes to review:__ 3

__Specific Comments (Readability):__

Everything is named in a manner that makes it easy to understand. Matches conventions established in the rest of the project.

__Specific Comments (Code Quality):__

Cyclomatic complexity is high in Update, but breaking it down into helper methods would likely make it less readable. The rest of the file is fairly straight forward.

__Hypothetical Changes and how current implementation May/Not support change:__

Currently moves as an animation, rather than as an interaction with a collision. Easy to change, just move the update logic to the collision logic.