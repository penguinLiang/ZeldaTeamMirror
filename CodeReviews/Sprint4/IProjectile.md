---
__Author:__ Jarred Fink

__Date of Review:__ 11/06/2019

__Sprint Number:__ 4

---
## IProjectile.cs ##

__Name of .cs file:__ IProjectile.cs 

__Author of .cs file:__ Quinn Shaner

__Minutes to review:__ 3

__Specific Comments (Readability):__

This is a very clear, concise interface that extends three other separate interfaces and adds in a public getter-setter bool property called Halted.

__Specific Comments (Code Quality):__

As it is currently used in the project, Halted does not necessarily need a public setter. However, this property is certainly preferable to a public field. In fact, IProjectile may not even need to extend IHaltable.

__Hypothetical Changes and how current implementation May/Not support change:__

As all projectiles seemingly vanish when they are halted both visually and in the code, Halted may be better off as an opposite bool called something like Visible or Exists.