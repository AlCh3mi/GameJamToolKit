using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace IceBlink.GameJamToolkit.AssetManagement
{
    public class StreamingAssetBundleLoader : IDisposable
    {
        public string AssetBundleName { get; }

        public string AssetBundlePath { get; }

        public bool IsLoaded => assetBundle != null;
        
        public IEnumerable<string> GetAssetList => assetBundle != null
            ? assetBundle.GetAllAssetNames()
            : Array.Empty<string>();

        private AssetBundle assetBundle;
        
        /// <summary>
        /// A class to load an Asset Bundle from the Streaming Assets folder
        /// </summary>
        /// <param name="assetBundleName">The name of the Asset Bundle to be loaded from the StreamingAssets Folder</param>
        /// <param name="monoBehaviour">MonoBehaviour to run the UnityWebRequest. If omitted, you will need to manually start the Coroutine GetAssetBundle()</param>
        public StreamingAssetBundleLoader(string assetBundleName, MonoBehaviour monoBehaviour = null)
        {
            var path = Path.Combine(Application.streamingAssetsPath, assetBundleName);
            
            if (string.IsNullOrEmpty(assetBundleName))
            {
                Debug.LogError("StreamingAssetBundleLoader : Invalid assetBundleName");
                return;
            }
            
            if(!File.Exists(path))
            {
                Debug.LogError("StreamingAssetBundleLoader : File not found - " + path);
                return;
            }

            AssetBundleName = assetBundleName;
            AssetBundlePath = path;
            
            if(monoBehaviour == null)
                return;

            monoBehaviour.StartCoroutine(GetAssetBundle());
        }
        
        public IEnumerator GetAssetBundle()
        {
            if (IsLoaded)
            {
                Debug.LogWarning($"AssetBundle {AssetBundleName} has already been loaded, use GetAsset<T>");
                yield break;
            }
            
            using var www = UnityWebRequestAssetBundle.GetAssetBundle(AssetBundlePath);
            yield return www.SendWebRequest();

            if (www.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error downloading asset bundle({AssetBundleName}): {www.error}");
                yield break;
            }

            assetBundle = DownloadHandlerAssetBundle.GetContent(www);
        }
        
        public IEnumerator GetAsset<T>(string assetName, Action<T> onAssetLoadedCallBack) where T : Object
        {
            if (!IsLoaded)
            {
                Debug.LogError("AssetBundle has not been loaded yet. Either start Coroutine GetAssetBundle() first, or when constructing an instance of this class, pass in a MonoBehaviour");
                yield break;
            }
            
            var request = assetBundle.LoadAssetAsync<T>(assetName);
            yield return request;

            if (request.asset != null)
            {
                var loadedAsset = (T)request.asset;
                onAssetLoadedCallBack.Invoke(loadedAsset);
            }
            else Debug.LogError("Failed to load asset from the bundle");
        }

        public void Dispose()
        {
            if (!IsLoaded)
                return;
            
            assetBundle.Unload(false);
            Debug.Log($"Asset Bundle Unloaded : {AssetBundleName}");
        }
    }
}