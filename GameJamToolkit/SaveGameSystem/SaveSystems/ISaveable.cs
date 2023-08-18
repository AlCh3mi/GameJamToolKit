using IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts;
using IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts.SavingSystem;

namespace IceBlink.GameJamToolkit.SaveGameSystem.SaveSystems
{
    public interface ISaveable
    {
        SavableType SavableType { get; }
        
        string AsJson();

        void Load(string json);
    }
}