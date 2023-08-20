using System;
using System.IO;
using System.Threading.Tasks;
using IceBlink.GameJamToolkit.SaveGameSystem.SaveSystems;
using IceBlink.GameJamToolkit.Singletons;
using Newtonsoft.Json;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem
{
    public class SaveSystem : Singleton<SaveSystem>
    {
        [SerializeField] private SaveSystemType saveType = SaveSystemType.FileSave;
        [field: SerializeField] public SaveSlot ActiveSlot { get; private set; } = SaveSlot.Slot00;
        
        [SerializeField] private string saveDirectory = "SaveGames";
        [SerializeField] private string saveFileExtension = ".sav";
        
        private ISaveSystem saveSystem;
        
        protected override void Awake()
        {
            base.Awake();
            
            switch (saveType)
            {
                default:
                case SaveSystemType.FileSave:
                    saveSystem = new FileSaveSystem();
                    break;
                case SaveSystemType.PlayerPrefs:
                    saveSystem = new PlayerPrefSaveSystem();
                    break;
            }
            Debug.Log("Save System Created: "+saveSystem.GetType().Name);
        }

        public void Save(string key, string json) => saveSystem.SaveData(key, json);

        public async Task<T> Load<T>(string key)
        {
            try
            {
                if (!SaveExists(key))
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

        public bool SaveExists(string key) => saveSystem.SaveExists(key);

        public void SetActiveSlot(SaveSlot slot) => ActiveSlot = slot;

        #region SavePath
        public static string GetProfileFolder(string profileName)
            => Path.Combine(Application.persistentDataPath, Instance.saveDirectory, profileName);

        public static string GetSaveFolder(string profileName, SaveSlot slot)
            => Path.Combine(GetProfileFolder(profileName), slot.ToString());

        public static string GetSaveFolderForCurrentSaveSlot(string profileName)
            => GetSaveFolder(profileName, Instance.ActiveSlot);

        public static string GetSaveFilePath(string profileName, string key)
        {
            var saveDirectory = GetSaveFolderForCurrentSaveSlot(profileName);
            var saveFilePath = Path.Combine(saveDirectory, key + Instance.saveFileExtension);
            return saveFilePath;
        }
        #endregion
    }
}
