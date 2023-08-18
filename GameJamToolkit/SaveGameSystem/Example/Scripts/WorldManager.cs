using System.Collections.Generic;
using IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts.SavingSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts
{
    public class WorldManager : MonoBehaviour
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
            
            SaveSystem.Instance
                .Save(worldSaveName, 
                    JsonConvert.SerializeObject(worldObjects,
                        Formatting.Indented,
                        new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
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
    }
}
