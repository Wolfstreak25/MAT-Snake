using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class RedSnakeController : MonoBehaviour
{   public ScoreManager scoreManager;
    public GameOverController gameOverController;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector2 direction = Vector2.right;
    private Vector2 input;
    public int initialSize = 4;
    public Transform Spawn;
    private void Start()
    {
        ResetState();
        gameObject.transform.position = Spawn.position;
    }
    
    private void Update() 
    {
        if (direction.x != 0f)
        {
            if(Input.GetKeyDown(KeyCode.W))
                direction = Vector2.up;
            else if(Input.GetKeyDown(KeyCode.S))
                direction = Vector2.down;
        }
        else if (direction.y != 0f)
        {
            if(Input.GetKeyDown(KeyCode.A))
                direction = Vector2.left;
            else if(Input.GetKeyDown(KeyCode.D))
                direction = Vector2.right;
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
        this.transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0.0f
        );
    }
    public void Grow()
    {
        scoreManager.IncreaseScore(10);
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
        
    }
    public void Digest()
    {
        scoreManager.IncreaseScore(-10);
        SoundManager.Instance.Play(Sounds.PickWrong);
    }
    public void ResetState()
    {
        direction = Vector2.up;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);

        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++) 
        {
            //Grow();
            Transform segment = Instantiate(segmentPrefab);
            segment.position = segments[segments.Count - 1].position;
            segments.Add(segment);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("GreenApple")) {
        //    Grow();
        //}
        if (other.gameObject.CompareTag("Player") ||other.gameObject.CompareTag("RedBody") || other.gameObject.CompareTag("GreenBody") ||other.gameObject.CompareTag("Wall") ) 
        {
            gameOverController.GameOver();
        }
        
    }
}