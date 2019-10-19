---
__Author:__ Quinn Shaner

__Date of Review:__ 10/18/2019

__Sprint Number:__ 3

---
## Inventory.cs  ##

__Name of .cs file:__ Inventory.cs 

__Author of .cs file:__ Jarred Fink

__Minutes to review:__ 3

__Specific Comments (Readability):__

First, all of the variable names are self-descriptive. All of the methods are small and clean, where pretty much the entirety of the method is described by the method name. 

__Specific Comments (Code Quality):__

From a maintainence point of view, Inventory needs to know about Items, and the Player. It doesn't need to know about a lot of the player or item classes, and thus has both low coupling and high cohesion.

__Hypothetical Changes and how current implementation May/Not support change:__

Hypothetical Change-- we could add in several different wallet sizes, and allow link to have a new maximum rupee count. You'd have to change 'MaxRupeeCount' to have a get and set method, and then add in a method to upgrade the wallet. Potentially through collision with a specific item. All in all, not hard or complicated to implement. 