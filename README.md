# BullectEcho2D

A 2D top-down stealth and shooting game prototype built in Unity. 

## Overview
In this game, you control a player navigating an industrial environment. You must avoid or eliminate patrolling enemies while collecting objectives (cash) scattered throughout the level. The game ends in victory when all objectives are collected and all enemies are defeated. If the player's health reaches zero, the game is lost.

## Features
- **Player Movement & Input**: Responsive WASD/Arrow key movement using the modern Unity Input System.
- **Enemy AI**: Fixed-route patrol paths with line-of-sight detection and range-based shooting.
- **Combat**: Tactical contact combat and cover mechanics using walls to block enemy fire.
- **Dynamic HUD**: Real-time tracking of health, objectives, and enemies remaining.

## Requirements
- **Unity Version**: Ensure you use the appropriate Unity version (2022 LTS or newer recommended).
- **Packages**: Dependencies include `com.unity.inputsystem`, `com.unity.ugui`, and `com.unity.render-pipelines.universal`. 

## How to Play
1. Clone the repository.
2. Open the project in Unity.
3. Open `Assets/Scenes/MainScene.unity`.
4. Press **Play**.
5. Move with `WASD` or Arrow Keys.
6. Touch enemies directly from behind cover to defeat them.
7. Collect all cash tokens to win!

## Documentation
For more detailed information on game flow, component configuration, and Inspector setup, please check `PROJECT_WORKING_GUIDE.md` and `PROJECT_OVERVIEW.md` inside `Assets/Documentation/`.
