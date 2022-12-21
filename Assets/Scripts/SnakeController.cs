 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SnakeType
{
    Red,
    Green
}
public class SnakeController : MonoBehaviour
{
    //[SerializeField] private float speed = 1.0f;
    [SerializeField] private int GridHeight;
    [SerializeField] private int GridWidth;
    public ScoreManager scoreManager;
    //public GameOverController gameOverController;
    public LevelManager levelManager;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector2 direction = Vector2.right;
    public int initialSize = 4;
    public SnakeType snakeType;
    private float Movetimer;
    private float MovetimerMax;
    private void Start()
    {
        this.GetComponent<SnakeController>();
        //SoundManager.Instance.Play(Sounds.Start);
        MovetimerMax = 0.15f;
        Movetimer = MovetimerMax;
        ResetState();
        //gameObject.transform.position = Spawn.position;
    }
    private void Update() 
    {
        Playerinput();
        SnakeMovement();
        //Warping(); 
    }
    public void Grow()
    {
        scoreManager.IncreaseScore(10);
        //SoundManager.Instance.Play(Sounds.PickRight);
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }
    public void Digest()
    {
        if(scoreManager.Score() > 0)
        {
            scoreManager.IncreaseScore(-10);
            //SoundManager.Instance.Play(Sounds.PickWrong);
            var Garbagesegment = segments[segments.Count - 1];
            // remove the segment
            segments.RemoveAt(segments.Count - 1);
            Destroy(Garbagesegment.gameObject);
        }
    }
    public void ResetState()
    {
        transform.position = Vector3.zero;
        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }
        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);
        if(this.snakeType == SnakeType.Green){segments[0].transform.position = levelManager.SpawnGreen.position;}
        if(this.snakeType == SnakeType.Red){segments[0].transform.position = levelManager.SpawnRed.position;}
        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++) {
            //Grow();
            Transform segment = Instantiate(segmentPrefab);
            segment.position = segments[segments.Count - 1].position - new Vector3(1,1,0);
            segments.Add(segment);
        }
    }
    //colliding to die
    private void OnTriggerEnter2D(Collider2D other) 
    { 
        if (other.gameObject.CompareTag("SnakeBody")) 
        {
            levelManager.GameOver();
            Debug.Log("Bodies Collided");
            //SoundManager.Instance.Play(Sounds.Failed);
        }
        
    }
    
    // }
    public void PowerUp(PowerType powerType)
    {
        Debug.Log("Power Up Picked up");
        switch(powerType)
                {
                    case PowerType.Speed:
                        StartCoroutine(SuperSpeed());
                        Debug.Log("Speed Picked up");
                        break;
                    case PowerType.Shield:
                        StartCoroutine(PowerShield());
                        Debug.Log("Shield Picked up");
                        break;
                    case PowerType.FoodBuff:
                        ScoreBuff();
                        Debug.Log("FoodBuff Picked up");
                        break;
                }
    }
    IEnumerator PowerShield()
    {
        //SoundManager.Instance.Play(Sounds.PickRight);
            Physics2D.IgnoreLayerCollision(3, 3);
        yield return new WaitForSeconds(5.0f);
            Physics2D.IgnoreLayerCollision(3, 3, false);
        yield return null;
    }
    IEnumerator SuperSpeed()
    {
        //SoundManager.Instance.Play(Sounds.PickRight);
            MovetimerMax = 0.05f;
        yield return new WaitForSeconds(5.0f);
            MovetimerMax = 0.15f;
        yield return null;
    }
    void ScoreBuff()
    {
        //SoundManager.Instance.Play(Sounds.PickRight);
        for (int i = 0; i < 5; i++) {Grow();}
    }
    private void Playerinput()
    {
        if(snakeType == SnakeType.Green)
        {
            if (direction.x != 0f)
            {
                if(Input.GetKeyDown(KeyCode.W))
                {
                    direction = Vector2.up;
                }
                if(Input.GetKeyDown(KeyCode.S))
                {
                    direction = Vector2.down;
                }
            }
            if (direction.y != 0f)
            {
                if(Input.GetKeyDown(KeyCode.A))
                {
                    direction = Vector2.left;
                }
                if(Input.GetKeyDown(KeyCode.D))
                {
                    direction = Vector2.right;
                } 
            }
        }
        if(snakeType == SnakeType.Red)
        {
            if (direction.x != 0f)
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    direction = Vector2.up;
                }
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    direction = Vector2.down;
                }
            }
            if (direction.y != 0f)
            {
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    direction = Vector2.left;
                }
                if(Input.GetKeyDown(KeyCode.RightArrow ))
                {
                    direction = Vector2.right;
                } 
            }
        }
        

    }
    private void SnakeMovement()
    {
        Movetimer += Time.deltaTime;
        if(Movetimer >= MovetimerMax)
        {
            //Follow Bodysegment to preceeding one
           for (int i = segments.Count - 1; i > 0; i--) 
            {   
                segments[i].transform.position = segments[i - 1].transform.position;
                segments[i].transform.rotation = segments[i - 1].transform.rotation;   
            }
            Movetimer -= MovetimerMax;
            this.transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y, 0.0f);
            //Rotating Sprite in the movement direction
            transform.eulerAngles = new Vector3(0, 0 , GetAngle(direction)-90);
            //Checking Screen wrapping
            transform.position = Warp(this.transform.position);
        }
    }
    //geting angle based on player direction for sprite rotation
    private float GetAngle(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
        if( n < 0 )
            n += 360;
        return n;
    }
    //Screen wrapping
    private Vector2 Warp(Vector2 Position)
    {
        if(transform.position.x < 0)
        {
            Position.x = GridWidth;
        }
        if(transform.position.x > GridWidth)
        {
            Position.x = 0;
        }
        if(transform.position.y < 0)
        {
            Position.y = GridHeight;
        }
        if(transform.position.y > GridHeight)
        {
            Position.y = 0;
        }
        return Position;
    }
}