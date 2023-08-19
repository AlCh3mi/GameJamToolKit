using IceBlink.GameJamToolkit.DamageSystem;
using IceBlink.GameJamToolkit.DamageSystem.Damage;
using IceBlink.GameJamToolkit.DamageSystem.Defenses;
using IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example
{
    public class SomeSaveableObject : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [field: SerializeField] public SavableType SavableType { get; private set; }

        public bool isHostile;
        private Damageable damageable;
        
        private void Awake()
        {
            damageable = GetComponent<Damageable>();
        }

        public void OnDamaged(DamageReport damageReport)
        {
            var t = damageable.Health.Current / damageable.Health.Max;
            GetComponent<MeshRenderer>().material.SetColor("_Emission", Color.Lerp(Color.green, Color.red, t));
        }
        
        public void LoadFromSaveableData(SaveableObjectData worldObjectData)
        {
            transform.SetPositionAndRotation(
                new Vector3(worldObjectData.posX, worldObjectData.posY, worldObjectData.posZ),
                Quaternion.Euler(new Vector3(worldObjectData.rotX, worldObjectData.rotY, worldObjectData.rotZ)));

            damageable.Health = new Health(worldObjectData.health, worldObjectData.maxHealth);
            gameObject.name = worldObjectData.name;
            isHostile = worldObjectData.isHostile;
            SavableType = worldObjectData.savableType;
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
                savableType = SavableType.WorldObject
            };
        }
    }
}