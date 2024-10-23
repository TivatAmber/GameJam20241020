using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools
{
    public class BaseManager<T> : Singleton<T>
        where T : MonoBehaviour
    {
        [SerializeField] private SceneAsset nextScene;

        public virtual void EndGame()
        {
            if (nextScene)
                SceneManager.LoadScene(nextScene.name);
        }
    }
}