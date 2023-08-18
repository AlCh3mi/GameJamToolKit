using System.Threading.Tasks;

namespace IceBlink.GameJamToolkit.SaveGameSystem.SaveSystems
{
    public interface ISaveSystem
    {
        bool SaveExists(string key); 
        Task SaveData(string key, string json);
        Task<string> LoadData(string key);
    }
}