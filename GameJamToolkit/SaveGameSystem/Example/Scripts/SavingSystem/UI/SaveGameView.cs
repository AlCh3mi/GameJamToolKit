using System;
using IceBlink.GameJamToolkit.SaveGameSystem.SaveSlots;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem.UI
{
    public class SaveGameView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text slotNameText;
        [SerializeField] private TMP_Text timestampText;

        public SaveSlotId SaveSlot { get; private set; }
        
        public UnityEvent onClick;
        public void SetHeaderText(string text) => slotNameText.text = text;

        public void SetTimestampText(string text) => timestampText.text = text;
        
        public void SetTimestampText(DateTime timestamp) => SetTimestampText(timestamp.ToShortDateString());
        
        public void SetSprite(Sprite sprite) => image.sprite = sprite;

        public void SetSaveSlot(SaveSlotId saveSlotId) => SaveSlot = saveSlotId;
        
        public void OnPointerClick(PointerEventData eventData) => onClick?.Invoke();
    }
}
