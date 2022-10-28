using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button PlayButton;
    //public GameObject LevelSelection;
    public Button QuitButton;
    private void Awake() 
    {
        PlayButton.onClick.AddListener(PlayGame);
        QuitButton.onClick.AddListener(QuitGame);
    }
    private void PlayGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(1);
        //LevelSelection.SetActive(true);
    }
    private void QuitGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        Debug.Log("Quit");
        Application.Quit();
    }

}
