---
__Author:__  Quinn Shaner

__Date of Review:__ 11/10/2019

__Sprint Number:__ 4

---
## GameStateAgent.cs  ##

__Name of .cs file:__ GameStateAgent.cs

__Author of .cs file:__ Chase

__Minutes to review:__ 

__Specific Comments (Readability):__

Right off the bat, GameStateAgent keeps to the naming conventions that we've stablished over the semester. All of the methods are short and clear, and the file itself is only 152 lines long, despite having a lot of functionality.
A lot of the functions were split into their own classes and abstracted away, so the reader only needs to see the bare minimum of what each method does to understand how it slots into the gamestateagent as a whole.

__Specific Comments (Code Quality):__

Some high coupling is introduced in this file, but that's to be expected, given the sheer scope of items the gamestateagent needs to effect. Cyclomatic complexity rarely gets deeper than one level of foreach loop, which makes the code easier to maintain. The amount of dependencies will make changing this agent more difficult.


__Hypothetical Changes and how current implementation May/Not support change:__

I don't have any changes to suggest. If need be, we could create more 'world states' so that we could manage the different states tighter. Splitting up a world state would simply involve adding another world, removing the functionality from the original world, and then adding a new file for the new world.
