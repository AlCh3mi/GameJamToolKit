using IceBlink.GameJamToolkit.DamageSystem;
using IceBlink.GameJamToolkit.DamageSystem.Defenses;
using IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts.SavingSystem;
using IceBlink.GameJamToolkit.SaveGameSystem.SaveSystems;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts
{
    public class SomeSaveableObject : MonoBehaviour, ISaveable
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float radius = 3f;
        [field: SerializeField] public SavableType SavableType { get; private set; }

        [field: SerializeField] public SaveableObjectData Defaults { get; private set; }
        
        public bool isHostile;
        private Damageable damageable;
        private GUID guid;
        
        
        private void Awake()
        {
            damageable = GetComponent<Damageable>();
            guid = GUID.Generate();
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * speed, Space.Self);
            //transform.position = new Vector3(Mathf.Cos(Time.time * speed) * radius, Mathf.Sin(Time.time * speed) * radius, 0f);
        }

        public SaveableObjectData GetSaveableData()
        {
            var position = transform.position;
            var rotation = transform.rotation.eulerAngles;
            return new SaveableObjectData()
            {
                posX = position.x,
                posY = position.y,
                posZ = position.z,
                rotX = rotation.x,
                rotY = rotation.y,
                rotZ = rotation.z,
                health = damageable.Health.Current,
                maxHealth = damageable.Health.Max,
                name = gameObject.name,
                isHostile = true,
                id = guid,
                savableType = SavableType.WorldObject
                
            };
        }
        
        public string AsJson() => JsonConvert.SerializeObject(GetSaveableData());

        public void Load(string json)
        {
            var worldObjectData = JsonConvert.DeserializeObject<SaveableObjectData>(json);
            LoadFromSaveableData(worldObjectData);
        }

        public void LoadFromSaveableData(SaveableObjectData worldObjectData)
        {
            transform.SetPositionAndRotation(
                new Vector3(worldObjectData.posX, worldObjectData.posY, worldObjectData.posZ),
                Quaternion.Euler(new Vector3(worldObjectData.rotX, worldObjectData.rotY, worldObjectData.rotZ)));

            damageable.Health = new Health(worldObjectData.health, worldObjectData.maxHealth);
            guid = worldObjectData.id;
            gameObject.name = worldObjectData.name;
            isHostile = worldObjectData.isHostile;
            SavableType = worldObjectData.savableType;
        }
    }
}
