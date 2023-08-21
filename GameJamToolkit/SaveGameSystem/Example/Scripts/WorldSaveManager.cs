using System.Collections.Generic;
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
        [SerializeField] private bool showGUIButtons = true;
        
        private List<SaveableObjectData> worldObjects = new ();

        private void Awake()
        {
            if (!factory) 
                factory = GetComponent<WorldObjectsFactory>();
        }

        public void NewWorld()
        {
            DestroyAllWorldObjects();
            factory.SetupDefaultWorld();
        }

        public async void SaveWorld()
        {
            worldObjects = new List<SaveableObjectData>(FindAllSaveables());
            
            var json = JsonConvert.SerializeObject(worldObjects, Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            
            Debug.Log("World Object Count: "+worldObjects.Count +"\nJson : "+json);
            await SaveSystem.Instance.Save(worldSaveName, json);
        }

        public async void LoadWorld()
        {
            DestroyAllWorldObjects();

            if(!SaveSystem.Instance.SaveExists(SaveSystem.Instance.ActiveSlotId))
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

            foreach (var saveable in saveables.Where(x => x.SavableType == SavableType.WorldObject))
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
        public void ShowDebugGUIButtons(bool enable) => showGUIButtons = enable;
        
        private void OnGUI()
        {
            if(!showGUIButtons)
                return;
            
            if (GUI.Button(new Rect(15, 15, 150, 50), "Save"))
                SaveWorld();

            if (GUI.Button(new Rect(15, 85, 150, 50), "Load"))
            {
                DestroyAllWorldObjects();
                LoadWorld();
            }

            if (GUI.Button(new Rect(15, 155, 150, 50), "New"))
            {
                NewWorld();
            }
            
            if (GUI.Button(new Rect(15, 225, 150, 50), "Delete Current Save"))
                SaveSystem.Instance.DeleteSaveSlot(SaveSystem.Instance.ActiveSlotId);
        }
        #endregion
    }
}