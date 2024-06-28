# Unity AR Game

Welcome to the Unity AR Game project! This project is a thrilling augmented reality game built with Unity, designed to bring immersive gameplay directly to your mobile device. Dive into a world where the digital and real merge, as you interact with dynamic enemies, navigate through challenges, and strive for the highest score.

## Features

- **Augmented Reality Gameplay:** Experience the blend of physical and digital worlds through your device's camera.
- **Dynamic Enemy Interactions:** Engage with enemies that react to your movements, thanks to our sophisticated `Enemy Movement` and `Enemy Pooling` systems.
- **Intuitive Touch Controls:** Seamlessly interact with the game world using our custom `Player Input` system designed for touchscreens.
- **Real-time Health and Score Tracking:** Keep an eye on your performance with an integrated UI that updates your health and score in real-time.
- **Responsive Game Over and Restart Mechanics:** Encounter a smooth transition between game over and restart states, ensuring you're quickly back in action.

## Key Scripts

### [Enemy Movement.cs](#enemy-movement.cs-context)

This script is responsible for the AI of the enemies in the game. It controls how enemies move towards the player, attack, and react to being hit. Features include pathfinding, attack animations, and health management.

### [Enemy Pooling.cs](#enemy-pooling.cs-context)

To optimize performance, `Enemy Pooling.cs` manages the instantiation and reuse of enemy objects. It handles spawning enemies around the player at runtime, ensuring a consistent flow of challenges.

### [Player Input.cs](#player-input.cs-context)

`Player Input.cs` is at the heart of the game's control system. It captures and processes touch inputs, allowing players to interact with the game world in a natural and intuitive way.

### [Player.cs](#player.cs-context)

The `Player.cs` script defines the player's attributes, such as health and score, and includes methods for taking damage, scoring points, and interacting with the game environment through attacks.

### [UI.cs](#ui.cs-context)

`UI.cs` manages the game's user interface, updating the player's health and score in real-time. It also controls the visibility of the game over panel and handles the game restart functionality.

## Getting Started

To get started with the Unity AR Game project, clone this repository and open it in Unity 2022 or later. Ensure you have the Unity AR Foundation package installed for AR functionality.

## Contributing

We welcome contributions to the Unity AR Game project! Whether it's adding new features, fixing bugs, or improving documentation, your help is appreciated. Please feel free to fork the repository and submit a pull request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Unity Technologies for providing the engine and AR Foundation toolkit.
- All contributors who have helped shape this project.

Thank you for exploring the Unity AR Game project. We hope you enjoy playing and developing it as much as we do!
