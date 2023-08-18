using System;
using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example.Scripts.SavingSystem.UI
{
    //this should probably just use a button, but here we are
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
            ProfileSelector.DeleteProfile(profile);
        }
    }
}
