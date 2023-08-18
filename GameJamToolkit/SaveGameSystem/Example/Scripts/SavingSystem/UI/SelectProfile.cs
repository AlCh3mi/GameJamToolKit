using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts.SavingSystem.UI
{
    public class SelectProfile : MonoBehaviour
    {
        [SerializeField] private ProfileView prefab;
        [SerializeField] private Transform parent;
        
        private void Start()
        {
            Repaint();
        }

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
                    gameObject.SetActive(false);
                };
            }
        }
    }
}
