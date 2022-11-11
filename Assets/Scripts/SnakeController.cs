 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SnakeType
{
    Red,
    Green
}

[RequireComponent(typeof(CircleCollider2D))]
public class SnakeController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    public ScoreManager scoreManager;
    public GameOverController gameOverController;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector2 direction = Vector2.right;
    private Vector2 input;
    public int initialSize = 4;
    public Transform Spawn;
    public SnakeType type = SnakeType.Green;
    private void Start()
    {
        ResetState();
        SoundManager.Instance.Play(Sounds.Start);
        gameObject.transform.position = Spawn.position;
    }
    public void Power(PowerType type)
    {
        SoundManager.Instance.Play(Sounds.PoweredUP);
        switch(type)
        { 
            case PowerType.Speed :
                speed = 2;
                break;
            case PowerType.FoodBuff :
                for(int i = 0; i < 5; i++ )
                {
                    Grow();
                }
                break;
            case PowerType.Shield :
                PlayerShield();
                break;

        }
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale < 1.0f)
                Time.timeScale = 1.0f;
            else
                Time.timeScale = 0.0f;
        }

        if (direction.x != 0f)
        {
            if (type == SnakeType.Green)
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                    direction = Vector2.up * speed;
                else if(Input.GetKeyDown(KeyCode.DownArrow))
                    direction = Vector2.down * speed;
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.W))
                    direction = Vector2.up * speed;
                else if(Input.GetKeyDown(KeyCode.S))
                    direction = Vector2.down * speed;
            }
        }
        else if (direction.y != 0f)
        {
            if (type == SnakeType.Green)
            {
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                    direction = Vector2.left * speed;
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                    direction = Vector2.right * speed;
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.A))
                    direction = Vector2.left * speed;
                else if(Input.GetKeyDown(KeyCode.D))
                    direction = Vector2.right * speed;
            }
            
        }      
    }
    private void FixedUpdate() 
    {
        // Set the new direction based on the input
        if (input != Vector2.zero) {
            direction = input;
        }

        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        this.transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y, 0.0f);
            Warping();
    }
    public void Grow()
    {
        scoreManager.IncreaseScore(10);
        SoundManager.Instance.Play(Sounds.PickRight);
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }
    public void Digest()
    {
        if(scoreManager.Score() > 0)
        {
            scoreManager.IncreaseScore(-10);
            SoundManager.Instance.Play(Sounds.PickWrong);
            var Garbagesegment = segments[segments.Count - 1];
            // remove the segment
            segments.RemoveAt(segments.Count - 1);
            Destroy(Garbagesegment.gameObject);
        }
    }
    public void ResetState()
    {
        if (type == SnakeType.Green)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.up;
        }
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);

        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++) {
            //Grow();
            Transform segment = Instantiate(segmentPrefab);
            segment.position = segments[segments.Count - 1].position;
            segments.Add(segment);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("RedApple")) {
        //    Grow();
        //} else 
        if (other.gameObject.CompareTag("Player") ||other.gameObject.CompareTag("RedBody") || other.gameObject.CompareTag("GreenBody") ) 
        {
            gameOverController.GameOver();
            SoundManager.Instance.Play(Sounds.Failed);
        }
    }
    private void Warping()
    {
        if(this.transform.position.x >= 17.5 || this.transform.position.x <= -17.5)
        {
            this.transform.position = new Vector3(
            Mathf.Round(transform.position.x) * -1.0f, Mathf.Round(transform.position.y), 0.0f);
        }
        else if(this.transform.position.y >= 9.5 || this.transform.position.y <= -9.5)
        {
            this.transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y) * -1.0f, 0.0f);
        }
    }
    IEnumerator PlayerShield()
    {
        SoundManager.Instance.Play(Sounds.PickRight);
        Physics2D.IgnoreLayerCollision(3, 3);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(3, 3, false);
    }

}