using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{   
    [SerializeField] private int GridHeight;
    [SerializeField] private int GridWidth;
    [SerializeField] private GameObject ScorePanelRed;
    [SerializeField] private GameObject ScorePanelGreen;
    [SerializeField] private GameObject SelectType;
    [SerializeField] private GameObject GameOverpanel;
    //public GameObject Canvas;
    public GameObject RedSnake;
    public GameObject GreenSnake;
    public PowerUps powerUps;
    public Food GreenApple;
    public Food RedApple; 
    private Vector3 Position;
    public Transform SpawnRed;
    public Transform SpawnGreen;
    private void Awake() 
    {
        //Solo.onClick.AddListener(PlaySolo);
        //Duo.onClick.AddListener(PlayDuo);
        //SpawnSnake(GreenSnake);
        SelectType.SetActive(true);
        SpawnFood();
        //SpawnPowerups();
    }
    private void Update() 
    {
        //Invoke("SpawnPowerups",10.0f);
    }
    public void PlaySolo()
    {
        ScorePanelGreen.SetActive(true);
        SelectType.SetActive(false);   
        SpawnSnake(GreenSnake.GetComponent<SnakeController>());
    }
    public void PlayDuo()
    {
        SelectType.SetActive(false);
        SpawnSnake(GreenSnake.GetComponent<SnakeController>());
        SpawnSnake(RedSnake.GetComponent<SnakeController>());
    }
    public void SpawnSnake(SnakeController Snake)
    {   
        if (Snake.snakeType == SnakeType.Green)
        {
            //Position = SpawnGreen.transform.position;
            GreenSnake.SetActive(true);
            //GreenSnake.transform.position = SpawnGreen.transform.position;
            ScorePanelGreen.SetActive(true);
        }
        if (Snake.snakeType == SnakeType.Red)
        {
            //Position = SpawnRed.transform.position;
            RedSnake.SetActive(true);
            //RedSnake.transform.position = SpawnRed.transform.position;
            ScorePanelRed.SetActive(true);
            
        }
    }
    public void SpawnFood()
    {
        Position = SpawnPosition();
        Instantiate(GreenApple , Position , Quaternion.identity);
        Position = SpawnPosition();
        Instantiate(RedApple , Position , Quaternion.identity);
    }
    public void SpawnPowerups()
    {
        Position = SpawnPosition();
        Instantiate(powerUps , Position , Quaternion.identity);   
    }
    public Vector3 SpawnPosition()
    {
        return new Vector3(Mathf.Round(Random.Range(-GridWidth, GridWidth)), Mathf.Round(Random.Range(0, GridHeight)) , 0);
    }
    public void GameOver()
    {
        //SoundManager.Instance.PauseMusic();
        //SoundManager.Instance.Play(Sounds.Failed);
        Time.timeScale = 0f;
        GameOverpanel.SetActive(true); 
    }
    public void ReloadLevel()
    {
        //SoundManager.Instance.Play(Sounds.ButtonClick);
        Scene scene =  SceneManager.GetActiveScene();
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.buildIndex);
    }
    public void QuitGame()
    {
        //SoundManager.Instance.Play(Sounds.ButtonClick);
        Debug.Log("Quit");
        Application.Quit();
    }
} 
