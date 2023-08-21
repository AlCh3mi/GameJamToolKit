using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;
using UnityEngine;
using UnityEngine.UI;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem.UI
{
    public class SelectProfile : MonoBehaviour
    {
        [SerializeField] private ProfileView prefab;
        [SerializeField] private Transform parent;
        [SerializeField] private Button selectProfileButton;
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject mainMenu;


        private void OnEnable() => GameManager.Instance.GameState = GameState.InMenu;

        private void Start() => Repaint();

        private void OnDisable() => GameManager.Instance.GameState = GameState.InGame;

        private void Clear()
        {
            foreach (Transform tf in parent)
                Destroy(tf.gameObject);
        }

        public void Repaint()
        {
            Clear();
            
            foreach (var kvp in ProfileSelector.Profiles)
            {
                var instance = Instantiate(prefab, parent);
                instance.Setup(kvp.Value);
                instance.Selected += () =>
                {
                    selectProfileButton.gameObject.SetActive(true);
                    playButton.gameObject.SetActive(true);
                    mainMenu.SetActive(true);
                    gameObject.SetActive(false);
                };
            }
        }
    }
}
