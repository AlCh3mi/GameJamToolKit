namespace IceBlink.Serialization.SaveSystems
{
    public interface ISaveable
    {
        string AsJson();

        void Load(string json);
    }
}