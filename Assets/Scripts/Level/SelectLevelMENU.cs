using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelMENU : MonoBehaviour
{
    // Start is called before the first frame update
 

    [SerializeField]
    private Button _playTutorial = null;


    [SerializeField]
    private Button _playLevel1 = null;

    [SerializeField]
    private Button _playLevel2 = null;

    [SerializeField]
    private Button _backButton = null;

    [SerializeField]
    private int _tutorialIndex = 0;

    [SerializeField]
    private int _level1Index = 0;

    [SerializeField]
    private int _level2Index = 0;

    [SerializeField]
    private int _mainMenuIndex = 0;



    void Start()
    {
        _playTutorial.onClick.AddListener(delegate { LoadScene(_tutorialIndex); });
        _playLevel1.onClick.AddListener(delegate { LoadScene(_level1Index); });
        _playLevel2.onClick.AddListener(delegate { LoadScene(_level2Index); });
        _backButton.onClick.AddListener(delegate { LoadScene(_mainMenuIndex); });
    }



    public void LoadScene(int index)
    {

        SceneManager.LoadScene(index);
    }

}
