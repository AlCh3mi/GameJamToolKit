using System.Threading.Tasks;

namespace IceBlink.GameJamToolkit.Serialization.SaveSystems
{
    public interface ISaveSystem
    {
        Task SaveData(string slotName, string key, string json);
        Task<T> LoadData<T>(string slotName, string key);
    }
}