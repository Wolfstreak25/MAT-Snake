using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        transform.position = new Vector2(x, y);
        if(gameObject.tag == "RedApple")
            SoundManager.Instance.Play(Sounds.SpawnRedFod);
        if(gameObject.tag == "GreenApple")
            SoundManager.Instance.Play(Sounds.SpawnGreenFood);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomizePosition();
        var obj = other.gameObject.GetComponent<SnakeController>();
        if(gameObject.tag == "RedApple")
        {
            if(obj != null)
            {
                if (obj.type == SnakeType.Green)
                {
                    Debug.Log("The Green picked collectible ");
                    obj.Grow();
                }
                else
                {
                    Debug.Log("The Red picked collectible ");
                    obj.Digest();
                }
            }
        }
        if(gameObject.tag == "GreenApple")
        {
            if(obj != null)
            {
                if (obj.type == SnakeType.Green)
                {
                    Debug.Log("The Green picked collectible ");
                    obj.Digest();
                }
                else
                {
                    Debug.Log("The Red picked collectible ");
                    obj.Grow();
                }
            }
        }
    }
}

