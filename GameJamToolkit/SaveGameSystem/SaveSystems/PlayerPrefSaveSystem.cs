using System.Threading.Tasks;
using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.SaveSystems
{
    public class PlayerPrefSaveSystem : ISaveSystem
    {
        public bool SaveExists(string key)
        {
            var slotName = ProfileSelector.ActiveProfile.Name;
            return PlayerPrefs.HasKey($"{slotName}_{key}");
        }

        public async Task SaveData(string key, string json)
        {
            var slotName = ProfileSelector.ActiveProfile.Name;
            PlayerPrefs.SetString($"{slotName}_{key}", json);
            await Task.CompletedTask;
            Debug.Log("Save Completed");
        }

        public async Task<string> LoadData(string key)
        {
            var slotName = ProfileSelector.ActiveProfile.Name;
            var json = PlayerPrefs.GetString($"{slotName}_{key}", string.Empty);
            Debug.Log("Loading Completed : "+json);
            await Task.CompletedTask;
            return json;
        }
    }
}