using IceBlink.Serialization.SaveSlots;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IceBlink.Serialization.Example.Scripts
{
    public class CreateNewSaveSlot : MonoBehaviour
    {
        [SerializeField] private Button createButton;
        [SerializeField] private TMP_InputField inputField;

        private void OnEnable()
        {
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            createButton.onClick.AddListener(OnCreateButtonClicked);
        }

        private void OnCreateButtonClicked()
        {
            SaveSlotSelector.SetActiveSaveSlot(inputField.text);
            GetComponentInParent<SelectSaveSlot>().Repaint();
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
            createButton.onClick.RemoveListener(OnCreateButtonClicked);
        }

        private void OnInputFieldValueChanged(string value)
        {
            var isValidInput = true; //maybe regex? find out if there is a regex for OS filename restrictions
            createButton.interactable = isValidInput;
        }
    }
}
