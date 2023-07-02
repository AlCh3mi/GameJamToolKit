using System;
using IceBlink.GameJamToolkit.Serialization.SaveSlots;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IceBlink.GameJamToolkit.Serialization.Example.Scripts
{
    public class SaveSlotView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text slotNameText;
        [SerializeField] private TMP_Text lastSavedText;
        
        private SaveSlot saveSlot;

        public event Action Selected;

        public void Setup(SaveSlot saveSlot)
        {
            this.saveSlot = saveSlot;
            slotNameText.text = this.saveSlot.Name;
            lastSavedText.text = saveSlot.LastSave.ToLocalTime().ToShortDateString();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            SaveSlotSelector.SetActiveSaveSlot(saveSlot.Name);
            Selected?.Invoke();
        }
    }
}
