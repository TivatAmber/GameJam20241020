using UnityEngine;

namespace Tools
{
    public class Singleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        public bool global = false;
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }
        private void Awake()
        {
            if (global)
            {
                DontDestroyOnLoad(gameObject);
            }
            OnStart();
        }
        protected virtual void OnStart()
        {

        }
    }
}