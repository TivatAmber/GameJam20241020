using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools
{
    public class BaseManager<T> : Singleton<T>
        where T : MonoBehaviour
    {
        [SerializeField] private int nextScene = -1;

        public virtual void EndGame()
        {
            if (nextScene != -1)
                SceneManager.LoadScene(nextScene);
        }
    }
}