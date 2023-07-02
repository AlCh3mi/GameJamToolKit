using UnityEngine;

namespace IceBlink.DamageSystem.Indicators
{
    [RequireComponent(typeof(Canvas))]
    public class BillboardCanvas : MonoBehaviour
    {
        private Camera mainCamera;

        private void Awake()
        {
            // Get the reference to the main camera
            mainCamera = Camera.main;
            GetComponent<Canvas>().worldCamera = mainCamera;
        }

        private void LateUpdate()
        {
            // Rotate the canvas to face the camera
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }
    }
}
