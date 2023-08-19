using System.Collections.Generic;
using IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example
{
    public class WorldObjectsFactory : MonoBehaviour
    {
        [SerializeField] private SomeSaveableObject prefab;

        [Header("Defaults")]
        [SerializeField] private int columns = 8;
        [SerializeField] private int rows = 8;
        
        private int spawnedObjectCount = 0;
        
        public IEnumerable<SomeSaveableObject> SpawnWorldObjects(IEnumerable<SaveableObjectData> objects)
        {
            var spawnedObjects = new List<SomeSaveableObject>();
            
            foreach (var objectData in objects)
            {
                if(objectData.savableType != SavableType.WorldObject)
                    continue;
                
                spawnedObjects.Add(SpawnWorldObject(objectData));
            }

            return spawnedObjects;
        }
        
        public SomeSaveableObject SpawnWorldObject(SaveableObjectData data)
        {
            var instance = Instantiate(prefab, transform);
            ++spawnedObjectCount;
            if(data != null) instance.LoadFromSaveableData(data);
            return instance;
        }

        public void SetupDefaultWorld()
        {
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var instance = SpawnWorldObject(prefab.Defaults);
                    instance.transform.position = new Vector3(x, y, 0f);
                }
            }
        }
    }
}
