using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;
using UnityEditor;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Editor
{
    public static class Extras
    {
        [MenuItem("IceBlink/SaveSystem/Show Save Folder")]
        public static void ShowSaveFolder()
        {
            var path = "file:///" + SaveSystem.GetSaveFolderForCurrentSaveSlot(ProfileSelector.ActiveProfile.Name);
            Debug.Log("Opening path: "+path);
            Application.OpenURL(path);
        }    
    }
}