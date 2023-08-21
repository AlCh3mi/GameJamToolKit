using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem.UI
{
    public class SaveGameView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text slotNameText;
        [SerializeField] private TMP_Text timestampText;

        public void SetHeaderText(string text) => slotNameText.text = text;

        public void SetTimestampText(string text) => timestampText.text = text;
        
        public void SetTimestampText(DateTime timestamp) => SetTimestampText(timestamp.ToShortDateString());
        
        public void SetSprite(Sprite sprite) => image.sprite = sprite;
    }
}
