﻿using System.Collections.Generic;
using System.Linq;
using IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example
{
    public class WorldSaveManager : MonoBehaviour
    {
        [SerializeField] private string worldSaveName = "world";
        [SerializeField] private WorldObjectsFactory factory;
        
        private List<SaveableObjectData> worldObjects = new ();

        private void Awake()
        {
            if (!factory) factory = GetComponent<WorldObjectsFactory>();
        }
        

        [ContextMenu("Save World")]
        public void SaveWorld()
        {
            worldObjects = new List<SaveableObjectData>(FindAllSaveables());
            
            var json = JsonConvert.SerializeObject(worldObjects, Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            
            Debug.Log("World Object Count: "+worldObjects.Count +"\nJson : "+json);
            SaveSystem.Instance.Save(worldSaveName, json);
        }

        [ContextMenu("Load World")]
        public async void LoadWorld()
        {
            if(!SaveSystem.Instance.SaveExists(worldSaveName))
            {
                Debug.Log("No Save found. Setting up default world.");
                factory.SetupDefaultWorld();
                return;
            }
            
            var result = await SaveSystem.Instance.Load<List<SaveableObjectData>>(worldSaveName);
                
            if(result.Count <= 0)
            {
                worldObjects = new List<SaveableObjectData>();
                return;
            }

            worldObjects = result;

            factory.SpawnWorldObjects(worldObjects);
        }
        
        private IEnumerable<SaveableObjectData> FindAllSaveables()
        {
            var saveables = FindObjectsOfType<SomeSaveableObject>();

            var result = new List<SaveableObjectData>();

            foreach (var saveable in saveables)
                if(saveable.SavableType == SavableType.WorldObject)
                    result.Add(saveable.GetSaveableData());
            
            return result;
        }
        
        private static void DestroyAllWorldObjects()
        {
            var saveables = FindObjectsOfType<SomeSaveableObject>();

            foreach (var saveable in saveables.Where(x => x.SavableType == SavableType.WorldObject))
                Destroy(saveable.gameObject);
        }

        #region GUI
        [SerializeField] private bool showGUIButtons = true;
        
        public void ShowGUIButtons(bool enable) => showGUIButtons = enable;
        
        private void OnGUI()
        {
            if(!showGUIButtons)
                return;
            
            if (GUI.Button(new Rect(15, 15, 150, 50), "Save"))
            {
                SaveWorld();
            }

            if (GUI.Button(new Rect(15, 85, 150, 50), "Load"))
            {
                DestroyAllWorldObjects();
                LoadWorld();
            }

            if (GUI.Button(new Rect(15, 155, 150, 50), "New"))
            {
                DestroyAllWorldObjects();
                factory.SetupDefaultWorld();
            }
        }
        #endregion
    }
}