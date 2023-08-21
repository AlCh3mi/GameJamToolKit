using System;
using System.IO;
using System.Threading.Tasks;
using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;
using IceBlink.GameJamToolkit.SaveGameSystem.SaveSlots;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.SaveSystems
{
    public class FileSaveSystem : ISaveSystem
    {
        public async Task SaveData(string key, string json)
        {
            var profileName = ProfileSelector.ActiveProfile.Name;
            var saveDirectory = SaveSystem.GetSaveFolderForCurrentSaveSlot(profileName);
            var saveFilePath = SaveSystem.GetSaveFilePathForCurrentSaveSlot(profileName, key);

            try
            {
                if (!Directory.Exists(saveDirectory))
                    Directory.CreateDirectory(saveDirectory);

                await using var sw = new StreamWriter(saveFilePath);
                await sw.WriteAsync(json);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async Task<string> LoadData(string key)
        {
            var profileName = ProfileSelector.ActiveProfile.Name;
            var saveFilePath = SaveSystem.GetSaveFilePathForCurrentSaveSlot(profileName, key);
            
            try
            {
                using var sr = new StreamReader(saveFilePath);
                var json = await sr.ReadToEndAsync();
                Debug.Log($"LoadedData {key} \n{json}");
                return json;
            }
            catch (FileNotFoundException e)
            {
                Debug.LogError($"FileSaveSystem: Unable to find file ({saveFilePath}): {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return string.Empty;
        }
        
        public bool SaveExists(SaveSlotId slotId)
        {
            var profileName = ProfileSelector.ActiveProfile.Name;
            var saveFilePath = SaveSystem.GetSaveFolder(profileName, slotId);
            return Directory.Exists(saveFilePath);
        }
        
        public void DeleteSave(string key)
        {
            var profileName = ProfileSelector.ActiveProfile.Name;
            var saveFilePath = SaveSystem.GetSaveFilePathForCurrentSaveSlot(profileName, key);
            
            if(File.Exists(saveFilePath))
                File.Delete(saveFilePath);
        }
    }
}
