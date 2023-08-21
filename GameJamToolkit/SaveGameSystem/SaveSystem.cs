using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;
using IceBlink.GameJamToolkit.SaveGameSystem.SaveSlots;
using IceBlink.GameJamToolkit.SaveGameSystem.SaveSystems;
using IceBlink.GameJamToolkit.Singletons;
using Newtonsoft.Json;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem
{
    public class SaveSystem : Singleton<SaveSystem>
    {
        [field: SerializeField] public SaveSlotId ActiveSlotId { get; private set; } = SaveSlotId.Slot00;
        
        [SerializeField] private string saveDirectory = "SaveGames";
        [SerializeField] private string saveFileExtension = ".sav";
        
        private ISaveSystem saveSystem;
        
        protected override void Awake()
        {
            base.Awake();
            saveSystem = new FileSaveSystem();
        }

        public async Task Save(string key, string json)
            => await saveSystem.SaveData(key, json);

        public async Task<T> Load<T>(string key)
        {
            try
            {
                if (!SaveExists(ActiveSlotId))
                {
                    Debug.LogWarning("SaveSystem: Save file does not exist.");
                    return default;
                } 
                        
                var json = await saveSystem.LoadData(key);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                Debug.LogError("SaveSystem : Something went wrong - "+e);
            }
            return default;
        }

        public bool SaveExists(SaveSlotId slotId)
            => saveSystem.SaveExists(slotId);

        public bool HasSaveGames()
        {
            foreach (SaveSlotId saveSlotId in Enum.GetValues(typeof(SaveSlotId)))
            {
                if (SaveExists(saveSlotId))
                    return true;
            }

            return false;
        }

        #region SaveSlots
        public void SetActiveSlot(SaveSlotId slotId)
            => ActiveSlotId = slotId;
        
        public DateTime GetLastModified(SaveSlotId slotId)
            => Directory.GetLastWriteTime(GetSaveFolder(ProfileSelector.ActiveProfile.Name, slotId));

        public bool SlotIsPopulated(SaveSlotId slotId)
            => Directory.Exists(GetSaveFolder(ProfileSelector.ActiveProfile.Name, slotId));

        public void DeleteSaveSlot(SaveSlotId slotId)
        {
            var directory = GetSaveFolder(ProfileSelector.ActiveProfile.Name, slotId);
            
            if(Directory.Exists(directory))
                Directory.Delete(directory, true);
        }
        #endregion
        
        #region SavePath
        public static string GetProfileFolder(string profileName)
            => Path.Combine(Application.persistentDataPath, Instance.saveDirectory, profileName);

        public static string GetSaveFolder(string profileName, SaveSlotId slotId)
            => Path.Combine(GetProfileFolder(profileName), slotId.ToString());

        public static string GetSaveFolderForCurrentSaveSlot(string profileName)
            => GetSaveFolder(profileName, Instance.ActiveSlotId);

        public static string GetSaveFilePathForCurrentSaveSlot(string profileName, string key)
        {
            var saveDirectory = GetSaveFolderForCurrentSaveSlot(profileName);
            var saveFilePath = Path.Combine(saveDirectory, key + Instance.saveFileExtension);
            return saveFilePath;
        }
        #endregion
    }
}
