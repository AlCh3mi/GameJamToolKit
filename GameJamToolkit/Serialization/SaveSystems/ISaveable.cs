namespace IceBlink.GameJamToolkit.Serialization.SaveSystems
{
    public interface ISaveable
    {
        string AsJson();

        void Load(string json);
    }
}