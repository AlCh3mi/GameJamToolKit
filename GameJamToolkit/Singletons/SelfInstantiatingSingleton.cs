using UnityEngine;

namespace IceBlink.GameJamToolkit.Singletons
{
    public abstract class SelfInstantiatingSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        var singletonObject = new GameObject(typeof(T) + " (Singleton)");
                        instance = singletonObject.AddComponent<T>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                
                return instance;
            }
        }
    }
}
