using System.Globalization;
using UnityEngine;

namespace IceBlink.GameJamToolkit.Diagnostics
{
    public class ShowFPS : MonoBehaviour
    {
        [Header("Screen Settings")]
        [SerializeField] private Vector2 screenPosition;
        [SerializeField] private Vector2 dimensions;
        [SerializeField] private GUIStyle guiStyle;
    
        private float deltaTime;
        private float fps;
        
        private bool _show;
        public bool ShowOnScreen
        {
            get => _show;
            private set
            {
                PlayerPrefs.SetInt("ShowFPS", value ? 1 : 0);
                _show = value;
            } 
        }

        public void Show(bool show) => ShowOnScreen = show;

        private void Awake()
        {
            ShowOnScreen = PlayerPrefs.GetInt("ShowFPS", 0) != 0;
        }

        private void Update () 
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            fps = 1.0f / deltaTime;
        }

        private void OnGUI()
        {
            if(!ShowOnScreen)
                return;
        
            var rect = new Rect(screenPosition, dimensions);
            GUI.Label(rect, $"FPS: {Mathf.Ceil(fps).ToString(CultureInfo.InvariantCulture)}", guiStyle);
        }
    }
}