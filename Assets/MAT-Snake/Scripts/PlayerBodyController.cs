using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour
{
    public PlayerType BodyType;
    public Vector2 PlayerDirection;
    Animator PlayerBodyAnimator;
    private void Start() 
    {
        PlayerBodyAnimator = GetComponent<Animator>();
    }
    private void Update() 
    {
        PlayerBodyAnimator.SetFloat("x_dir",PlayerDirection.x);
        PlayerBodyAnimator.SetFloat("y_dir",PlayerDirection.y);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        var obj = other.gameObject.GetComponent<CharacterController>();
        if(obj != null)
        {
            Debug.Log("Collission occured");
            if(this.BodyType == obj.playerType)
            {
                Debug.Log("Ally Gathered");
                //obj.Recruit();
            }
            else
            {
                Debug.Log("Ally Lost");
                //obj.Regret();
            }
        }
    }
}
