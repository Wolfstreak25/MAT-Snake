using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType
{
    Speed,
    Shield,
    FoodBuff,
    Count
}

public class PowerUps : MonoBehaviour
{
    public List<PowerUpBase> Powerups = new List<PowerUpBase>();
    public Collider2D gridArea;
    public PowerType currentType = PowerType.Speed;

    private void Start()
    {
        Invoke("RandomizeObject", 10f);
        Debug.Log(Powerups[0].type == PowerType.Speed);
    }

    public void RandomizeObject()
    {
        int type = Random.Range(0, (int)PowerType.Count);// % (int)PowerType.Count;
        var power = Powerups[0];
        currentType = (PowerType)type;
        for (int i = 0; i < Powerups.Count; i++)
        {
            Powerups[i].gameObject.SetActive(false);
            if (Powerups[i].type == (PowerType)type)
            {
                Powerups[i].gameObject.SetActive(true);
                power = Powerups[i];
            };
        }
        power.transform.position = RandomizePosition();
    }
    public Vector2 RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;
        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);
        return new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collided");
        Invoke("RandomizeObject", 30f);
        var obj = other.gameObject.GetComponent<SnakeController>();
            if(obj != null)
            {
                Debug.Log("Picked up");
                obj.Grow();
                    obj.Power(currentType);
            }
    }
}
