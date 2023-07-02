using IceBlink.GameJamToolkit.Serialization.SaveSlots;
using UnityEngine;

namespace IceBlink.GameJamToolkit.Serialization.Example.Scripts
{
    public class SelectSaveSlot : MonoBehaviour
    {
        [SerializeField] private SaveSlotView prefab;
        [SerializeField] private Transform parent;
        
        private void Start()
        {
            Repaint();
        }

        private void Clear()
        {
            foreach (Transform tf in parent)
                Destroy(tf.gameObject);
        }

        public void Repaint()
        {
            Clear();
            
            foreach (var slot in SaveSlotSelector.SaveSlots)
            {
                var instance = Instantiate(prefab, parent);
                instance.Setup(slot.Value);
                instance.Selected += () =>
                {
                    gameObject.SetActive(false);
                };
            }
        }
    }
}
