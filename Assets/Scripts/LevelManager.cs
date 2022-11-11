using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject RedSnake;
    public GameObject GreenSnake;
    public Button Solo;
    //public GameObject LevelSelection;
    public Button Duo;
    private void Awake() 
    {
        Solo.onClick.AddListener(PlaySolo);
        Duo.onClick.AddListener(PlayDuo);
    }
    private void PlaySolo()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        GreenSnake.SetActive(true);
        
        Canvas.SetActive(false);
    }
    private void PlayDuo()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        GreenSnake.SetActive(true);
        RedSnake.SetActive(true);
        Canvas.SetActive(false);
    }

}
