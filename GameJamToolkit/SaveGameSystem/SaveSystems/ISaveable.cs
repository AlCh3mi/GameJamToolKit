namespace IceBlink.GameJamToolkit.SaveGameSystem.SaveSystems
{
    public interface ISaveable
    {
        string AsJson();

        void Load(string json);
    }
}