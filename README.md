# Baba Is You Recreation
A basic recreation of the "Baba Is You" video game based on the original Game Jam version's levels.
Uses the C# Monogame framework and the Monogame.Extended open-source extension.
A Visual Studio solution is included to view the code as well as build and run the game.
It may be required to add the Monogame framework to Visual Studio.

# Controls
WASD - Menu Navigation, Default Movement
Esc - Return to Menu, Pause(in game)
Enter - Select
Z - Undo (default)
R - Reset Level (default)

# Features
Players can edit the in-game keybindings, which will persist to local storage to be used each time the game is launched.
An Undo Move mechanic is in place, allowing players to correct a wrong move.
The level can be reset to its initial state at any time.
Levels are constructed based on parsing an input file rather than hard coded. Additional levels can be added by editing said file.
