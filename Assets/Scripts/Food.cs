using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int GridHeight;
    [SerializeField] private int GridWidth;
    public SnakeType snakeType;

    private void Start()
    {
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        this.transform.position = new Vector3(Mathf.Round(Random.Range(0, GridWidth)), Mathf.Round(Random.Range(0, GridHeight)) , 0);
        //SoundManager.Instance.Play(Sounds.SpawnRedFod);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        var obj = other.gameObject.GetComponent<SnakeController>();
            if(obj != null)
            {
                Debug.Log("Collission occured");
                if(this.snakeType == obj.snakeType)
                {
                    Debug.Log("Segment Gathered");
                    obj.Grow();
                }
                else
                {
                    Debug.Log("Segment Lost");
                    obj.Digest ();
                }
                RandomizePosition();
                Debug.Log("Collision");
            }
    }
}


