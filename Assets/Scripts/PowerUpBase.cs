using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    public PowerType type;
    private LevelManager levelManager;
    private void OnEnable() 
    {
        Invoke ("DisableThis", 20.0f);
    }
    private void DisableThis()
    {
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {        
        var obj = other.gameObject.GetComponent<SnakeController>();
            if(obj != null)
            {
                switch(type)
                {
                    case PowerType.Speed:
                        Debug.Log("Speed Picked up");
                        break;
                    case PowerType.Shield:
                        Debug.Log("Shield Picked up");
                        break;
                    case PowerType.FoodBuff:
                        Debug.Log("FoodBuff Picked up");
                        break;
                }
                Debug.Log("Picked up");
                //obj.Grow();
                //obj.Power(currentType);
            }
        //Invoke("RandomizeObject", 30f);
    }
}
