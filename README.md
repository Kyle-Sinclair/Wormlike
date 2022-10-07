# Wormlike
A third person turn based shooter based on worms 3D for the FGP-22 

The game can be played by loading the start scene into the hierarchy and hitting
play. 
I am aiming for the VG grade.

What follows is
- A list of movement instructions for controlling the characters
- A list of features necessary for G and a description of my implementation of them.
- A list of features necessary for VG and a description of my implementation of them.
# Movement Controls 
WASD - move in the xz-plane.  
Spacebar - Jump  
Left click - Fire weapon (if equipped). Hold to charge on applicable weapon.  
ZX - Alter the angle of an equipped weapon

# G Features
(G) Only play scene is required

The main level is loaded asynchronously by Level loader using information 
passed from a UI script on the start screen.

(G) Basic Unity terrain or primitives will suffice for a level  
The level terrain is built using simple primitives with some additional level design editor tools.

(G) A player only controls one worm  
(G) Use the built in Character Controller. Add jumping.  
(G) Has hit points  
Each worm uses the standard character controller with jumping and movement
input gained from supporting classes.
Each worm handles its own health pool, with a container class
called Team handling the surrender of a team if all its worms are
defeated. 

(G) Focus camera on active player  

Camera movement is implemented using the cinemachine package.

(G) Minimum of two different weapons/attacks, can be of similar functionality, 
can be bound to an individual button, 
like weapon 1 is left mouse button and weapon 2 is right mouse button  

There are four weapons in game with wide variation in behaviour

# VG Features
(VG, large) Support up to 4 players (using the same input device taking turns)  
(VG, medium) A player controls a team of (multiple worms)

The game can support up to four teams, each controlled by a player taking turns, with up to four worms on each team.
Each worm handles its own health pool, with a container class 
called Team handling the surrender of a team if all its worms are
defeated.

A turn manager handles timing and shifting of control between worms,
by enabling and disabling the playerinput component on each worm. 
 The turn manager also has a naive implementation of a turn timer handled by turn manager
to better simulate how turns function for playtesting purposes.

(VG, medium) The two types of weapons/attacks must function differently, I.E a pistol and a hand grenade. The player can switch between the different weapons and using the active weapon on for example left mouse button

By far the most effort was put into the weapon design system, with a 
focus on the tool as a Designer editor tool. Each worm has a weaponry controller,
which handles what is currently equipped and processes inputs related to
weapon use. The qualities of the weapons themselves is determined by a 
ScriptableObject which contains only data about the weapon's model, the
projectiles prefab, the movement of the projectile after firing and
what occurs when the projectile collides with something.

The aim of this system were twofold.

A) Have making a new weapon be a trivial process for a designer, with 
any feature on an existing weapon being easy to recombine into a new 
weapon in seconds.
B) Make the projectiles appearance, movement and final effect be completely 
independent, recombinable and recyclable.

Only four weapons are included in the final product but making a new one
can be done in seconds. The projectile movement or impact effects themselves 
must be developed by a programmer but after they are written they are fully 
deployable into any existing weapon in seconds. I ran out of time to make the
impact effect prefabs also recyclable, but the scaffolding for implementing it
has been put in place. 
