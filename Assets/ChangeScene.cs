using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private int nextScene;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<playermove>(out var gamePlayerMove))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
