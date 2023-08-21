using System.Threading.Tasks;
using IceBlink.GameJamToolkit.SaveGameSystem.SaveSlots;

namespace IceBlink.GameJamToolkit.SaveGameSystem.SaveSystems
{
    public interface ISaveSystem
    {
        bool SaveExists(SaveSlotId slotId);
        Task SaveData(string key, string json);
        Task<string> LoadData(string key);
        void DeleteSave(string key);
    }
}