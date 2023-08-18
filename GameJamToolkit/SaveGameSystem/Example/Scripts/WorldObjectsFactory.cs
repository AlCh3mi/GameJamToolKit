using System.Collections.Generic;
using IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts.SavingSystem;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts
{
    public class WorldObjectsFactory : MonoBehaviour
    {
        [SerializeField] private SomeSaveableObject prefab;

        [Header("Defaults")]
        [SerializeField] private int amount;
        [SerializeField] private float radius = 3f;
        
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
            var instance = Instantiate(prefab);
            ++spawnedObjectCount;
            if(data != null) instance.LoadFromSaveableData(data);
            return instance;
        }

        public void SetupDefaultWorld()
        {
            for (int i = 0; i < amount; i++)
            {
                var angle = i * (360f / amount + 1);
                var instance = SpawnWorldObject(prefab.Defaults);
                instance.transform.position = new Vector3(
                    transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad), 
                    transform.position.y,
                    transform.position.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad));
            }
        }
    }
}
