using System;
using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem.UI
{
    public class ProfileView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text slotNameText;
        [SerializeField] private TMP_Text lastSavedText;
        
        private Profile profile;

        public event Action Selected;

        public void Setup(Profile profile)
        {
            this.profile = profile;
            slotNameText.text = this.profile.Name;
            lastSavedText.text = profile.LastSave.ToLocalTime().ToShortDateString();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ProfileSelector.SetActiveProfile(profile.Name);
            Selected?.Invoke();
        }

        public void DeleteProfileOnClick()
        {
            Debug.Log("Deleting Profile : "+profile.Name);
            ProfileSelector.DeleteProfile(profile);
            GetComponentInParent<SelectProfile>().Repaint();
        }
    }
}
