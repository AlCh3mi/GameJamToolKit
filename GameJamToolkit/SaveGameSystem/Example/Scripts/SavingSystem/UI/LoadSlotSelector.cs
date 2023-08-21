using System;
using IceBlink.GameJamToolkit.SaveGameSystem.SaveSlots;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem.UI
{
    public class LoadSlotSelector : MonoBehaviour
    {
        [SerializeField] private WorldSaveManager worldSaveManager;
        [SerializeField] private SaveGameView prefab;
        [SerializeField] private Transform content;

        private void OnEnable()
        {
            GameManager.Instance.GameState = GameState.InMenu;
            Repaint();
        }

        private void OnDisable() => GameManager.Instance.GameState = GameState.InGame;

        private void Repaint()
        {
            Clear();
            
            foreach (SaveSlotId slotId in Enum.GetValues(typeof(SaveSlotId)))
            {
                if (!SaveSystem.Instance.SaveExists(slotId))
                    continue;
                
                var instance = Instantiate(prefab, content);
                instance.SetSaveSlot(slotId);
                instance.SetHeaderText($"{slotId}");
                instance.SetTimestampText(SaveSystem.Instance.GetLastModified(slotId));
                instance.onClick.AddListener(() =>
                {
                    SaveSystem.Instance.SetActiveSlot(instance.SaveSlot);
                    worldSaveManager.LoadWorld();
                    gameObject.SetActive(false);
                });
            }
        }

        private void Clear()
        {
            foreach (Transform saveGame in content)
                Destroy(saveGame.gameObject);
        }
    }
}
