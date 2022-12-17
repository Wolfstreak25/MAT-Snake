using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public enum PlayerType
{
    Boy,
    Girl
}
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;
    private Vector2Int PlayerPosition;
    private Vector2Int PlayerDirection;
    private float Movetimer;
    private float MovetimerMax;
    private void Awake() {
        PlayerPosition = new Vector2Int(5,10);
        PlayerDirection = new Vector2Int(0,-1);
        MovetimerMax = 1f;
        Movetimer = MovetimerMax;
    }
 
    void Update()
    {
        Playerinput();
        Playermovement();
        
    }
    private void Playerinput()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(PlayerDirection.y != -1)
            {
                PlayerDirection.x = 0;
                PlayerDirection.y = 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(PlayerDirection.y != 1)
            {
                PlayerDirection.x = 0;
                PlayerDirection.y = -1;
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(PlayerDirection.x != 1)
            {
                PlayerDirection.x = -1;
                PlayerDirection.y = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(PlayerDirection.x != -1)
            {
                PlayerDirection.x = 1;
                PlayerDirection.y = 0;
            }
        }
    }
    private void Playermovement()
    {
        Movetimer += Time.deltaTime;
        if(Movetimer >= MovetimerMax)
        {
            Movetimer -= MovetimerMax;
            PlayerPosition += PlayerDirection;
            transform.position = new Vector3(PlayerPosition.x , PlayerPosition.y, 0);
        }
    }
}
