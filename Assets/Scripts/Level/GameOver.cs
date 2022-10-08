using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject _player = null;

    [SerializeField]
    private int indexLevel = 0;

    private void Update()
    {
        if (_player == null)
            TriggerGameOver();
    }

    void TriggerGameOver()
    {
        //include the namespace  UnityEngine.SceneManagement
        SceneManager.LoadScene(indexLevel);
       // Application.Quit();
    }
}
