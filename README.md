# IceBlink - Game Jam Tool Kit

Game Jam Tool Kit is a Unity package designed to aid quick prototyping and streamline game development during game jams. 
It provides a collection of useful scripts, components, and prefabs and features to help you focus on creating a fun and engaging game without spending too much time on repetitive tasks.


## Getting Started

Clone or download the GameJamToolKit repository.

Open your Unity project and navigate to the "Assets" menu.
Select "Import Package" > "Custom Package" and choose the downloaded .unitypackage file.
Follow the on-screen instructions to import the package into your project.

## Audio System

The *AudioManager* class serves as a centralized hub for managing audio settings and playback in Unity. 
It provides a convenient way to control various aspects of audio, including master volume, music volume, effects volume, and audio playback. 
The class is designed with flexibility and customization in mind, allowing for seamless integration into different projects.

At its core, the *AudioManager* class offers methods to play, pause, and stop music playback, as well as to play one-shot sound effects. 
It utilizes Unity's AudioSource components for music and sound effects, enabling precise control over audio playback.

The class also interacts with and provides an AudioMixer, which is used to manage the audio parameters for different audio groups, such as master, music, and effects. 
The AudioManager adjusts these parameters to achieve the desired volume levels. The use of logarithmic scaling for volume values ensures smooth and intuitive control over audio levels.

One of the standout features of the AudioManager is its ability to remember player audio settings using PlayerPrefs. 
This means that the audio settings selected by the player persist across sessions, creating a personalized audio experience.

Additionally, the class is designed with the Singleton pattern, ensuring that there is only one instance of the AudioManager throughout the application. 
This centralized management simplifies the process of handling audio settings and playback from various parts of the game.

Overall, the *AudioManager* class provides an organized and efficient solution for managing audio in Unity projects. 
It offers a customizable foundation for implementing audio-related features while maintaining a clean separation of concerns between audio playback and the underlying audio parameters.

## Object Pooling

The provided code snippets are part of a flexible object pooling system designed to optimize memory usage and performance in Unity. 
The pooling system revolves around the concept of reusing objects instead of instantiating and destroying them repeatedly, which can lead to memory fragmentation and performance issues.

### Pool<T> Class:
The *Pool<T>* class serves as the base class for creating object pools. It's generic and requires a prefab of the object type you want to pool (T). 
You can specify the default size, capacity, and whether to prewarm the pool (create and store objects upfront). The class sets up an ObjectPool<T> instance, managing object retrieval and return.

### PoolableObject Class:
The PoolableObject class extends MonoBehaviour and is meant to be added to objects that will be pooled. 
It contains an event (ReturnToPool) that is invoked when the object is disabled. This event is used to return the object to the pool when it's no longer needed.

### PoolableParticleSystem Class:
The PoolableParticleSystem class is an example of a specific pooled object type. It inherits from PoolableObject and adds functionalities specific to a ParticleSystem. 
It overrides OnEnable to start playing the particle system, and when the particle system stops, it deactivates the game object.

In essence, this pooling system promotes efficient memory management by reusing objects and avoiding costly instantiation and destruction. It can be extended and customized for different object types by inheriting from the Pool<T> class and implementing specific behaviors within the poolable object classes. 
This approach is especially useful for optimizing performance in scenarios where objects are frequently created and destroyed, such as particles, bullets, enemies, etc.

## Damage System
[*More detailed documentation here*](GameJamToolkit/DamageSystem/DamageSystemDocumentation.md)
#### (Examples included)

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

## Save Game System
[*More detailed documentation here*](GameJamToolkit/SaveGameSystem/SaveSystemDocumentation.md)
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
The MonoBehaviour State Machine is a framework designed for creating organized and modular behavior patterns within Unity. <br>

Comprising two primary components 
- State and 
- StateRunner 

This system offers a flexible approach to managing and transitioning between various states of a MonoBehaviour object. <br>
States, represented as State<T>, provides a blueprint for different behaviors by implementing methods such as 

- EnterState, 
- Update, 
- ExitState.
 
These states can be specialized for specific actions or behaviors, making it easy to define and maintain various in-game functionalities.

The StateRunner<T> class serves as the orchestrator, responsible for handling state transitions and executing the active state's methods.<br>
It supports dynamic state switching through the SwitchState method and allows for the activation of starting states during initialization.<br>
With the built-in mechanisms, the State Runner seamlessly manages state updates, input gathering, and state change condition checks, 
enabling developers to organize and control their MonoBehaviour components' behavior efficiently.

The system's structure facilitates clean separation of logic, making codebase management and debugging more manageable.<br> 
It empowers developers to create intricate and structured behaviors, ultimately enhancing the maintainability and flexibility of their applications.

## Game Events

The ScriptableObject Event System provides a robust mechanism for facilitating communication and interaction between different components and systems. 
The cornerstone of this system is the GameEvent ScriptableObject, which represents specific game events that can be raised and listened to.
These events are created as assets through the Unity context menu, allowing for easy integration and management.

The GameEvent class features methods for raising events and managing event listeners. 
When an event is raised using the Raise method, all registered GameEventListener instances are notified and their corresponding response actions are triggered. 
This allows for dynamic communication between components without direct references, enhancing modularity and reducing tight coupling.

The GameEventListener MonoBehaviour component serves as a bridge between the GameEvent and the response action that needs to be executed. 
When enabled, the listener automatically registers itself with the specified event and invokes the response action whenever the event is raised. 
The use of UnityEvents for responses provides flexibility in defining behavior, enabling developers to link any required actions, functions, or behaviors to the event.

Overall, the ScriptableObject Event System promotes a more decoupled and organized architecture, enabling efficient communication between components, systems, and objects.

## Asset Management

The StreamingAssetBundleLoader class provides a streamlined and flexible approach to loading Asset Bundles from the Streaming Assets folder within Unity projects. 
Designed with ease of use in mind, this class encapsulates the process of loading Asset Bundles and their associated assets, offering both synchronous and asynchronous loading options.

The class constructor takes the name of the Asset Bundle to be loaded and, optionally, a MonoBehaviour for coroutine handling. 
It constructs the asset bundle path from the streaming assets directory and performs basic error checks, such as verifying the existence of the file. 
If a MonoBehaviour is provided, the class automatically initiates the coroutine to begin loading the Asset Bundle.

The GetAssetBundle coroutine method is responsible for loading the Asset Bundle using UnityWebRequest and handles any errors that may occur during the process. 
Once successfully loaded, the Asset Bundle instance is stored within the class.

The class also offers an asynchronous GetAsset method for loading specific assets from the previously loaded Asset Bundle. 
It requires the name of the asset to be loaded and a callback function to be executed once the asset is loaded. 
This method ensures that the Asset Bundle is loaded prior to asset retrieval and handles potential errors.

Importantly, the class implements the IDisposable interface, enabling efficient resource management. 
The Dispose method unloads the Asset Bundle and releases any associated memory resources. This is especially useful for preventing memory leaks when the Asset Bundle is no longer needed.

Overall, the StreamingAssetBundleLoader class simplifies the process of loading Asset Bundles from the Streaming Assets folder, 
promoting efficient and organized asset management within Unity applications.

## Contributing

Contributions and feedback are welcome!

If you find any issues or have suggestions for new features, please create an issue or submit a pull request. Together, we can make Game Jam Tool Kit even better for game jam enthusiasts.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
