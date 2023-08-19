using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem.UI
{
    public class CreateNewProfile : MonoBehaviour
    {
        [SerializeField] private Button createButton;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private int minLength = 5;
        private void OnEnable()
        {
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            createButton.onClick.AddListener(OnCreateButtonClicked);
        }

        private void OnCreateButtonClicked()
        {
            ProfileSelector.SetActiveProfile(inputField.text);
            GetComponentInParent<SelectProfile>().Repaint();
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
            createButton.onClick.RemoveListener(OnCreateButtonClicked);
        }

        private void OnInputFieldValueChanged(string value)
        {
            //todo: maybe regex? find out if there is a regex for OS filename restrictions
            var isValidInput = !string.IsNullOrWhiteSpace(value) && value.Trim().Length >= minLength;
            createButton.interactable = isValidInput;
        }
    }
}
