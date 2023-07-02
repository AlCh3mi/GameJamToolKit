using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace IceBlink.GameJamToolkit.Serialization.SaveSlots
{
    public static class SaveSlotSelector
    {
        private static SaveSlot activeSaveSlot;
        private static Dictionary<string, SaveSlot> saveSlots;
        private static string saveManifestPath;

        public const string SAVE_DIRECTORY = "SaveGames";
        public const string SAVE_FILE_EXTENSION = ".sav";
        private const string SAVE_MANIFEST_FILE = "SaveManifest.json";
        private const string DEFAULT_SLOT_NAME = "default";

        public static SaveSlot ActiveSaveSlot
        {
            get => activeSaveSlot;
            private set
            {
                activeSaveSlot = value;
                SaveManifest();
            }
        }

        public static Dictionary<string, SaveSlot> SaveSlots
        {
            get
            {
                if (saveSlots == null)
                    LoadManifest();
                return saveSlots;
            }
        }

        static SaveSlotSelector()
        {
            LoadManifest();
            SetActiveSaveSlot(DEFAULT_SLOT_NAME);
        }

        public static void SetActiveSaveSlot(string slotName)
        {
            if (!SaveSlots.ContainsKey(slotName))
                AddToManifest(new SaveSlot { Name = slotName, LastSave = DateTime.Now});

            var slot = SaveSlots[slotName];
            ActiveSaveSlot = slot;

            Debug.Log("Save Slot Activate: " +slotName);
        }
        
        public static void UpdateSaveSlot(SaveSlot saveSlot)
        {
            saveSlot.LastSave = DateTime.Now;
            SaveSlots[saveSlot.Name] = saveSlot;
            SaveManifest();
        }

        public static void AddToManifest(SaveSlot saveSlot)
        {
            if (SaveSlots.ContainsKey(saveSlot.Name)) 
                return;
            
            SaveSlots[saveSlot.Name] = saveSlot;
            SaveManifest();
        }

        public static string GetSaveFolder(string slotName)
        {
            return Path.Combine(Application.persistentDataPath, SAVE_DIRECTORY, slotName);
        }

        public static string GetSaveFilePath(string slotName, string key)
        {
            var saveDirectory = GetSaveFolder(slotName);
            var saveFilePath = Path.Combine(saveDirectory, key + SAVE_FILE_EXTENSION);
            return saveFilePath;
        }

        private static void LoadManifest()
        {
            saveManifestPath = Path.Combine(Application.persistentDataPath, SAVE_MANIFEST_FILE);
            if (File.Exists(saveManifestPath))
            {
                var json = File.ReadAllText(saveManifestPath);
                saveSlots = JsonConvert.DeserializeObject<Dictionary<string, SaveSlot>>(json);
            }
            else saveSlots = new Dictionary<string, SaveSlot>();
        }

        private static void SaveManifest()
        {
            var json = JsonConvert.SerializeObject(saveSlots);
            File.WriteAllText(saveManifestPath, json);
            Debug.Log("SaveSlotManifest saved.");
        }
    }
}