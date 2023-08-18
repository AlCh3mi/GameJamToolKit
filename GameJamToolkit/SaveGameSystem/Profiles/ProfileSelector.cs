using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Profiles
{
    public static class ProfileSelector
    {
        private static Profile _activeProfile;
        private static Dictionary<string, Profile> _profiles;
        private static string profileManifestPath;

        public const string SAVE_DIRECTORY = "SaveGames";
        public const string SAVE_FILE_EXTENSION = ".sav";
        private const string PROFILE_MANIFEST_FILE = "Manifest.json";
        private const string DEFAULT_SLOT_NAME = "default";

        public static Profile ActiveProfile
        {
            get => _activeProfile;
            private set
            {
                _activeProfile = value;
                SaveManifest();
            }
        }

        public static Dictionary<string, Profile> Profiles
        {
            get
            {
                if (_profiles == null)
                    LoadManifest();
                return _profiles;
            }
        }

        static ProfileSelector()
        {
            LoadManifest();
            SetActiveProfile(DEFAULT_SLOT_NAME);
        }

        public static void SetActiveProfile(string slotName)
        {
            if (!Profiles.ContainsKey(slotName))
                AddToManifest(new Profile { Name = slotName, LastSave = DateTime.Now});

            var slot = Profiles[slotName];
            ActiveProfile = slot;

            Debug.Log("Save Slot Activated: " +slotName);
        }
        
        public static void UpdateProfile(Profile profile)
        {
            profile.LastSave = DateTime.Now;
            Profiles[profile.Name] = profile;
            SaveManifest();
        }

        public static void AddToManifest(Profile profile)
        {
            if (Profiles.ContainsKey(profile.Name)) 
                return;
            
            Profiles[profile.Name] = profile;
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
            profileManifestPath = Path.Combine(Application.persistentDataPath, PROFILE_MANIFEST_FILE);
            if (File.Exists(profileManifestPath))
            {
                var json = File.ReadAllText(profileManifestPath);
                _profiles = JsonConvert.DeserializeObject<Dictionary<string, Profile>>(json);
            }
            else _profiles = new Dictionary<string, Profile>();
        }

        private static void SaveManifest()
        {
            var json = JsonConvert.SerializeObject(_profiles);
            File.WriteAllText(profileManifestPath, json);
            Debug.Log("SaveSlotManifest saved.");
        }
    }
}