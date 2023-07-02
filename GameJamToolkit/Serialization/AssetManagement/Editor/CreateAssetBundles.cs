using System.IO;
using UnityEditor;

namespace IceBlink.GameJamToolkit.Serialization.AssetManagement.Editor
{
    public static class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        private static void BuildAllAssetBundles()
        {
            const string assetBundleDirectory = "Assets/AssetBundles";
        
            if(!Directory.Exists(assetBundleDirectory))
                Directory.CreateDirectory(assetBundleDirectory);
        
            var target = EditorUserBuildSettings.activeBuildTarget;
            
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, target);
        }
    }
}