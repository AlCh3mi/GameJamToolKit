# IceBlink - Game Jam Tool Kit

Game Jam Tool Kit is a Unity package designed to aid quick prototyping and streamline game development during game jams. It provides a collection of useful scripts, components, and prefabs and features to help you focus on creating a fun and engaging game without spending too much time on repetitive tasks.

*Documentation and more sample projects to be released soon*

## Getting Started

Clone or download the GameJamToolKit repository.

Open your Unity project and navigate to the "Assets" menu.

Select "Import Package" > "Custom Package" and choose the downloaded .unitypackage file.

Follow the on-screen instructions to import the package into your project.

## [Damage System](GameJamToolkit/DamageSystem/DamageSystemDocumentation.md)

The DamageSystem is a comprehensive framework that facilitates the implementation of health and damage mechanics. 
It encompasses a set of classes, structures, and interfaces designed to handle:
- damage instances, 
- damage over time, 
- resistance calculations. 
- critical hits, 
- health management, 
- health regeneration, 
- damage indicators. 

With an emphasis on modularity and extensibility, the DamageSystem streamlines the process of integrating complex health and damage interactions

Documentation and Tutorials: Comprehensive documentation and example to guide you through the package and its functionalities.

## Object Pooling

## [Save Game System](GameJamToolkit/SaveGameSystem/SaveSystemDocumentation.md) 
#### (Examples included)

The SaveGame System offers a versatile framework for implementing a robust game save and load system.
This system is structured around three core components: 
- Profiles, 
- Save Slots, 
- and the Save System itself. 

Profiles allow users to create distinct save data sets, each with its own last save timestamp. 

Save Slots enable users to manage multiple save points within a single profile, offering flexibility in managing progress. 
The Save System orchestrates the actual saving and loading of data, supporting various implementations such as 
- PlayerPrefs, 
- local file storage(included), 
- or cloud-based services like Firebase. 

This system empowers developers to seamlessly integrate save and load capabilities into their projects, 
enhancing user experiences by enabling progress persistence and data retention across sessions.

## MonoBehaviour State Machine

## Audio System

## Game Events

## Asset Management

## Contributing

Contributions and feedback are welcome!

If you find any issues or have suggestions for new features, please create an issue or submit a pull request. Together, we can make Game Jam Tool Kit even better for game jam enthusiasts.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
