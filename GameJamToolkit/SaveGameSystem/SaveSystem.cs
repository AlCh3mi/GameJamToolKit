using System;
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
        
        private ISaveSystem saveSystem;
        
        protected override void Awake()
        {
            base.Awake();
            
            switch (saveType)
            {
                default:
                case SaveSystemType.PlayerPrefs:
                    saveSystem = new PlayerPrefSaveSystem();
                    break;
                case SaveSystemType.FileSave:
                    saveSystem = new FileSaveSystem();
                    break;
            }
            Debug.Log("Save System Created: "+saveSystem.GetType().Name);
        }

        public void Save(string key, string json)
        {
            saveSystem.SaveData(key, json);
        }
        
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

        public bool SaveExists(string key)
        {
            return saveSystem.SaveExists(key);
        }
    }
}
