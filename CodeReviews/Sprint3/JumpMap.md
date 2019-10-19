---
__Author:__ Jarred Fink

__Date of Review:__ 10/18/2019

__Sprint Number:__ 3

---
## JumpMap.cs ##

__Name of .cs file:__ JumpMap.cs 

__Author of .cs file:__ Henry Xiong

__Minutes to review:__ 3

__Specific Comments (Readability):__

High-quality readibility. Fields and methods are all organized nicely and named appropriately.

__Specific Comments (Code Quality):__

The public bool Visible may be better off as a public property with { get; set } for consistency.

__Hypothetical Changes and how current implementation May/Not support change:__

If we change the dungeon layout in sprint 5, or if debug rooms are added/removed, the map layout will need to be changed.