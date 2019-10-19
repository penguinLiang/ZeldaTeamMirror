# Bugs
## Where we put all of the current bugs, roughly when they were patched or how we plan on addressing them in the future.  

## Enemies
__1__: The Goriya sprite will sometimes freeze when moving two perpendicular directions (e.g. left ^ up) at the same time over a long distance. This will not be an issue going forward as Goriya cannot move diagonally in the game and will not be changing sprites that fast with the AI implemented. This is simply an issue with the Goriya being keyboard controlled.  
  (Sprint 2 Bug) (Patched by Chase on 9/27)

__2__: The Damage command can be a bit touchy. Try not to hold the key if you are trying to observe the hurt animation.  
(Sprint 2 Bug)

__3__: Goriya sprite freezes during diagonal movement at the point where players controlled enemy movement.
(Sprint 3 Bug) (Patched by Steven on 10/13)

__4__: If Goriya is facing a wall, it may shoot his boomerang backwards and/or may walk backwards away from the wall.
(Sprint 3 Bug) (Patched by Jarred and Chase on 10/18)


## Sprites
 __1__: The Rupee sprite has been flipped. From the original game, the blue rupee was solid and the red/yellow rupee should flash between colors.  
 (Sprint 2 Bug) (Patched by Quinn on 9/27)

 __2__: Some enemy sprites are supposed to alternate between 4 different palettes when damaged. Currently only alternating between 2.  
 (Sprint 2 Bug) (Patched by Steven on 9/27)

 __3__: Link's death sprite was removed at some point, so the program would crash when Link reached 0 health.
 (Sprint 3 Bug) (Patched by Chase on 10/16)


## Link
 __1__: When Link would use a boomerang or a bomb, because the sprites were not 16x16, they appeared off-center relative to Link.
 (Sprint 3 Bug) (Patched by Jarred on 10/04)

 __2__: Link's movement was too slow after the game was scaled up to be easier for players to view.
 (Sprint 3 Bug) (Patched by Steven on 10/14)

 __3__: When Link uses his sword or a secondary item, he gets frozen in place and his sprite remains that of the sword/item usage.
 (Sprint 3 Bug) (Patched by Chase on 10/14)


## Collision
 __1__: Because Link's bounds are only 8 pixels tall, he was able to "float" off the "ground" in the special Bow room instead of being restricted to having no vertical movement off the gray brick floor.
 (Sprint 3 Bug) (Patched by Chase on 10/16)

__2__: Enemy collisions are very sensitive. They need a timer for invulnerability, lest they be instantly killed.
(Sprint 3 Bug) (Patched by Chase on 10/16)

__3__: Gel's hitbox is too large.
(Sprint 3 Bug) (Patched by Steven on 10/17)

__4__: If an enemy pins Link against a wall while getting knocked back by the door, Link can glitch outside of the map.
(Sprint 3 Bug) (Patched by Chase on 10/18)


## Miscellaneous
__1__: Clicking on the jump map can occasionally stop working, seemingly when it is opened right after the game starts.
(Sprint 3 Bug) (Patched as best as possible (bug exists in Monogame) by Chase on 10/17)

