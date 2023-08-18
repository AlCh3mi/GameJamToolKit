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

        public static void SetActiveProfile(string profileName)
        {
            if (!Profiles.ContainsKey(profileName))
                AddToManifest(new Profile { Name = profileName, LastSave = DateTime.Now});

            var slot = Profiles[profileName];
            ActiveProfile = slot;

            Debug.Log("Save Slot Activated: " +profileName);
        }
        
        public static void UpdateProfile(Profile profile)
        {
            profile.LastSave = DateTime.Now;
            Profiles[profile.Name] = profile;
            SaveManifest();
        }
        
        public static void DeleteProfile(Profile profile)
        {
            var directory = SaveSystem.GetSaveFolder(profile.Name);
            
            if(!Directory.Exists(directory))
                return;
            
            Directory.Delete(directory, true);
        }

        #region Manifest
        public static void AddToManifest(Profile profile)
        {
            if (Profiles.ContainsKey(profile.Name)) 
                return;
            
            Profiles[profile.Name] = profile;
            SaveManifest();
        }
        
        private static void SaveManifest()
        {
            var json = JsonConvert.SerializeObject(_profiles);
            File.WriteAllText(profileManifestPath, json);
            Debug.Log("SaveSlotManifest saved.");
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
        #endregion
    }
}