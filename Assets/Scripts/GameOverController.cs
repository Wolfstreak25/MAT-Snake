 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button Replay;
    public Button QuitButton;
    private void Awake() 
    {
        Replay.onClick.AddListener(ReloadLevel);
        QuitButton.onClick.AddListener(QuitGame);
    }
    
    private void ReloadLevel()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        Scene scene =  SceneManager.GetActiveScene();
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.buildIndex);
    }
    private void QuitGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        Debug.Log("Quit");
        Application.Quit();
    }
}
