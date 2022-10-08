using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MENU : MonoBehaviour
{
    // Start is called before the first frame update
 

    [SerializeField]
    private Button _playButton = null;



    [SerializeField]
    private Button _selectLevel = null;


    [SerializeField]
    private Button _exitButton = null;

    [SerializeField]
    private int _firstSceneIndex = 0;

    [SerializeField]
    private int _levelSelectIndex = 0;



    void Start()
    {
        _selectLevel.onClick.AddListener(SelectLevel);
        _playButton.onClick.AddListener(PlayGame);
        _exitButton.onClick.AddListener(ExitGame);
    }


    public void ExitGame()
    {
     
        Application.Quit();
    }


    public void PlayGame()
    {
   

        SceneManager.LoadScene(_firstSceneIndex);
    }
    public void SelectLevel()
    {

        SceneManager.LoadScene(_levelSelectIndex);
    }

}
