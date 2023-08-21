using System;
using IceBlink.GameJamToolkit.SaveGameSystem.SaveSlots;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem.UI
{
    public class SaveSlotSelector : MonoBehaviour
    {
        [SerializeField] private SaveGameView prefab;
        [SerializeField] private Transform content;

        private void Start()
        {
            Repaint();
        }

        [ContextMenu("Repaint")]
        public void Repaint()
        {
            Clear();
            
            foreach (SaveSlotId slotId in Enum.GetValues(typeof(SaveSlotId)))
            {
                if (!SaveSystem.Instance.SaveExists(slotId))
                {
                    var empty = Instantiate(prefab, content);
                    empty.SetHeaderText($"{slotId} - Empty Slot");
                    empty.SetTimestampText(string.Empty);
                    continue;
                }
                
                var instance = Instantiate(prefab, content);
                instance.SetHeaderText($"{slotId}");
                instance.SetTimestampText(SaveSystem.Instance.GetLastModified(slotId));
            }
        }

        private void Clear()
        {
            foreach (Transform saveGame in content)
                Destroy(saveGame.gameObject);
        }
    }
}
