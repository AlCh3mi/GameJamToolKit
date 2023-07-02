using System;
using System.Collections.Generic;
using IceBlink.Serialization.SaveSlots;
using IceBlink.Serialization.SaveSystems;
using UnityEngine;

namespace IceBlink.Serialization
{
    public class SaveSystem : MonoBehaviour
    {
        private ISaveSystem saveSystem;
        
        [SerializeField] private SaveSystemType saveType;
        
        private SaveSlot ActiveSaveSlot => SaveSlotSelector.ActiveSaveSlot;

        private void Awake()
        {
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

        #pragma warning disable CS1998
        [ContextMenu("Save")]
        public async void Save()
        {
            //todo implement what needs to be saved, and how?
            
            //var saveables = FindAllSaveables();
            //await saveSystem.SaveData(activeSaveSlot.Name, "WorldState", saveable.AsJson());
        }

        [ContextMenu("Load")]
        public async void Load()
        {
            //todo, implement Load Game logic here
        }
        #pragma warning restore CS1998
        
        private static ISaveable[] FindAllSaveables()
        {
            var saveables = FindObjectsOfType<GameObject>();

            var result = new List<ISaveable>();

            foreach (var saveable in saveables)
            {
                var components = saveable.GetComponentsInChildren<ISaveable>();

                foreach (var component in components)
                    result.Add(component);
            }
            return result.ToArray();
        }
    }
}
