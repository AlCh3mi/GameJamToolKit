using System;
using System.IO;
using System.Threading.Tasks;
using IceBlink.GameJamToolkit.Serialization.SaveSlots;
using UnityEngine;

namespace IceBlink.GameJamToolkit.Serialization.SaveSystems
{
    public class FileSaveSystem : ISaveSystem
    {
        
        public async Task SaveData(string slotName, string key, string json)
        {
            var slotDirectory = SaveSlotSelector.GetSaveFolder(slotName);
            var saveFilePath = SaveSlotSelector.GetSaveFilePath(slotName, key);

            try
            {
                if (!Directory.Exists(slotDirectory))
                    Directory.CreateDirectory(slotDirectory);

                await using var sw = new StreamWriter(saveFilePath);
                await sw.WriteAsync(json);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async Task<T> LoadData<T>(string slotName, string key)
        {
            var saveFilePath = SaveSlotSelector.GetSaveFilePath(slotName, key);

            try
            {
                using var sr = new StreamReader(saveFilePath);
                var json = await sr.ReadToEndAsync();
                return JsonUtility.FromJson<T>(json);
            }
            catch (FileNotFoundException e)
            {
                Debug.LogError($"File Save System: Unable to find file ({saveFilePath}): {e.Message}");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return default;
        }
    }
}