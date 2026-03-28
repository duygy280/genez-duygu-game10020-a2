GAME10020 – Assignment 2

Event-Driven Gameplay System
Duygu Genez

Introduction

The goal of this project was to create an event-driven game play mechanic for Unity. This is accomplished through a class template that emphasizes how to use Unity events to create systems for: combat, inventory and user interface (UI) feedback.

List of Systems

Combat: A player can attack an enemy which will inflict damage, and either kill the enemy or it will remain alive if it has not died of damage inflicted on it.

Health: The player has three health points; and damage taken will fire and cause an update of the UI to reflect this as well as end the game if the player dies.

Inventory: The user can collect items and see what they collected in their inventory UI.

UI: Displays the user's health level, the contents of their inventory and the Game Over screen.

Restart: The player can restart the game once they die.

Event Flow

Enemy → OnHit → UI Feedback
Enemy → OnDeath → Attached Gameplay Response
Character → OnHealthChanged → Update UI with Health
Character → OnPlayerDeath → Produce Game Over Screen
