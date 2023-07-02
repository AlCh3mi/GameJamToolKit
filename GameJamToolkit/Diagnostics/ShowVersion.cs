using UnityEngine;

namespace Plugins.IceBlink.GameJamToolkit.Diagnostics
{
    public class ShowVersion : MonoBehaviour
    {
        [Header("Screen Settings")]
        [SerializeField] private Vector2 screenPosition;
        [SerializeField] private Vector2 dimensions;
        [SerializeField] private GUIStyle guiStyle;

        private bool _show;
        public bool ShowOnScreen
        {
            get => _show;
            private set
            {
                PlayerPrefs.SetInt("ShowVersion", value ? 1 : 0);
                _show = value;
            } 
        }
        
        public void Show(bool show) => ShowOnScreen = show;

        private void Awake()
        {
            ShowOnScreen = PlayerPrefs.GetInt("ShowVersion", 0) != 0;
        }
        
        private void OnGUI()
        {
            if(!ShowOnScreen)
                return;
            
            var rect = new Rect(screenPosition, dimensions);
            GUI.Label(rect, $"Version: {Application.version}", guiStyle);
        }
    }
}
