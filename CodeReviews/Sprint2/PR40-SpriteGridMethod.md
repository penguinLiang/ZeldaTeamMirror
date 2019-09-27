__Author:__ Quinn Shaner
__Date of Review:__ 9/25/2019
__Sprint Number:__ 2

## EnemyDamageKill.cs ##

__Name of .cs file:__ EnemyDamageKill.cs
__Author of .cs file:__ Henry, Steven, Jarred
__Minutes to review:__ 1

__Specific Comments (Readability):__
Clear definition for the EnemyKill Command. Renamed for readability. Formerly: EnemyDamagePauseKill

__Specific Comments (Code Quality):__
Simple to read, clearly defines what is shown

__Hypothetical Changes and how current implementation May/Not support change:__
Potentially rename 'random enemy' to 'next enemy'. Would be a very easy fix and would only effect this file.


---
## ZeldaGame.cs ##

__Name of .cs file:__ ZeldaGame.cs
__Author of .cs file:__ Entire team
__Minutes to review:__ 3

<Reviewing the DrawSpriteGrid Method, that was created by Jarred>

__Specific Comments (Readability):__
Jarred removed a lot of repeated code and pulled it into a clear method. Instead of calling multiple foreach loops per sprite array, now we call the method and abstract everything else out. Much easier to use this way.

__Specific Comments (Code Quality):__
If you add more grids to be drawn, you'll need more x and y coordinates. In addition, another grid will add another line of code and each grid has to be initialized in LoadContent. Not too bad, only one file to maintain. 

__Hypothetical Changes and how current implementation May/Not support change:__
None. This is clean and clear, I have no suggestions for this. 