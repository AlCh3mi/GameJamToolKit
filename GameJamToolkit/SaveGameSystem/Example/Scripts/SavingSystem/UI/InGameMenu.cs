using UnityEngine;
using UnityEngine.UI;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem.UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject escapeMenu;
        
        [Header("Saving")]
        [SerializeField] private Button saveMenuButton;
        [SerializeField] private SaveSlotSelector saveSlotSelector;
        
        [Header("Loading")]
        [SerializeField] private Button loadMenuButton;
        [SerializeField] private LoadSlotSelector loadSlotSelector;

        private void OnEnable()
        {
            DisableAllMenus();
            
            saveMenuButton.onClick.AddListener(SaveMenuOnClick);
            loadMenuButton.onClick.AddListener(LoadMenuOnClick);
        }
        
        private void Update()
        {
            if(!Input.GetKeyDown(KeyCode.Escape))
                return;

            ToggleMenu();
        }

        private void OnDisable()
        {
            saveMenuButton.onClick.RemoveListener(SaveMenuOnClick);
            loadMenuButton.onClick.RemoveListener(LoadMenuOnClick);
        }
        
        private void ToggleMenu()
        {
            escapeMenu.SetActive(!escapeMenu.activeSelf);
            
            GameManager.Instance.GameState = escapeMenu.activeSelf 
                ? GameState.InMenu
                : GameState.InGame;
        }
        
        private void SaveMenuOnClick()
        {
            DisableAllMenus();
            saveSlotSelector.gameObject.SetActive(true);
        }

        private void LoadMenuOnClick()
        {
            DisableAllMenus();
            loadSlotSelector.gameObject.SetActive(true);
        }

        private void DisableAllMenus()
        {
            saveSlotSelector.gameObject.SetActive(false);
            loadSlotSelector.gameObject.SetActive(false);
        }
    }
}
