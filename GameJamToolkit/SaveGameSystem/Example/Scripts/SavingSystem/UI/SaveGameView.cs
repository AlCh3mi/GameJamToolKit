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

        public void SetSprite(Sprite sprite) => image.sprite = sprite;

        public void SetHeaderText(string text) => slotNameText.text = text;

        public void SetTimeStampText(DateTime timestamp) => timestampText.text = timestamp.ToShortDateString();

        public void SetSaveGame()
        {
            //todo
        }
    }
}
