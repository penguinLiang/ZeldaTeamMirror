__Author:__ Chase Colman
__Date of Review:__ 9/19/2019
__Sprint Number:__ 2

---
## Quit.cs ##

__Name of .cs file:__ BlockSpriteFactory.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 2

__Specific Comments (Readability):__

Everything is well and consistently named. For a factory, it makes perfect sense.

__Specific Comments (Code Quality):__

Makes good use of singleton and factory patterns dicussed in class.

__Hypothetical Changes and how current implementation May/Not support change:__

 Steven noted in Discord that we can further shorten the singleton pattern to a single line like:  
`public static BlockSpriteFactory Instance { get; } = new BlockSpriteFactory()`

---
## LinkSecondaryAction.cs ##

__Name of .cs file:__ LinkSecondaryAction.cs
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

Matches the rest of commands.

__Specific Comments (Code Quality):__

This was added to fix an error in another unrelated branch that broke the build.

__Hypothetical Changes and how current implementation May/Not support change:__

Future fixes could be made in the original branch in which the error was made or in a quick fix branch. This keeps a clean separation of concerns.

---
## Zelda.csproj ##

__Name of .cs file:__ Zelda.csproj
__Author of .cs file:__ Henry
__Minutes to Review:__ 1

__Specific Comments (Readability):__

N/A. Project file.

__Specific Comments (Code Quality):__

This was added to fix an error in another unrelated branch that broke the build.

__Hypothetical Changes and how current implementation May/Not support change:__

Future fixes could be made in the original branch in which the error was made or in a quick fix branch. This keeps a clean separation of concerns.