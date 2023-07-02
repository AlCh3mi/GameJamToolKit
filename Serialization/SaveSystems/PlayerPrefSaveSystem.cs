using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace IceBlink.Serialization.SaveSystems
{
    public class PlayerPrefSaveSystem : ISaveSystem
    {
        public async Task SaveData(string slotName, string key, string json)
        {
            PlayerPrefs.SetString($"{slotName}_{key}", json);
            await Task.CompletedTask;
            Debug.Log("Save Completed");
        }

        public async Task<T> LoadData<T>(string slotName, string key)
        {
            var json = PlayerPrefs.GetString($"{slotName}_{key}", string.Empty);
            var result = JsonConvert.DeserializeObject<T>(json);
            await Task.CompletedTask;
            Debug.Log("Loading Completed : "+result);
            return result;
        }
    }
}