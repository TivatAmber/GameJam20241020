using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    [SerializeField] private int resetScene;

    public void Restart()
    {
        SceneManager.LoadScene(resetScene);
    }
}
