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
    public PlayerType playerType;
    Animator PlayerAnimator;
    //[SerializeField] private float MovementSpeed = 1.0f;
    public PlayerBodyController PlayerBody;
    private Vector2 PlayerPosition;
    private Vector2 PlayerDirection;
    private float Movetimer;
    private float MovetimerMax;
    private List<PlayerBodyController> BodySegments = new List<PlayerBodyController>();
    private void Awake() {
        PlayerPosition = new Vector2(5,10);
        PlayerDirection = new Vector2(0,-1);
        PlayerAnimator = GetComponent<Animator>();
        BodySegments.Add(gameObject.GetComponent<PlayerBodyController>());
        MovetimerMax = 0.5f;
        Movetimer = MovetimerMax;
    }
 
    void Update()
    {
        Playerinput();
        Playermovement();
    }
    private void Playerinput()
    {
        if (PlayerDirection.x != 0f)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                PlayerDirection = Vector2.up;
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                PlayerDirection = Vector2.down;
            }
        }
        if (PlayerDirection.y != 0f)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                PlayerDirection = Vector2.left;
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                PlayerDirection = Vector2.right;
            }
            
        }
        BodySegments[0].PlayerDirection = PlayerDirection;
    }
    private void Playermovement()
    {
        Movetimer += Time.deltaTime;
        if(Movetimer >= MovetimerMax)
        {
            //Follow Bodysegment to preceeding one
           for (int i = BodySegments.Count - 1; i > 0; i--) 
            {   
                BodySegments[i].transform.position = BodySegments[i - 1].transform.position;
                BodySegments[i].PlayerDirection = BodySegments[i - 1].PlayerDirection;
                
            }

            Movetimer -= MovetimerMax;
            //PlayerPosition += PlayerDirection * MovementSpeed;
            //transform.position = new Vector3(PlayerPosition.x , PlayerPosition.y, 0);
            this.transform.position = new Vector3(Mathf.Round(transform.position.x) + PlayerDirection.x, Mathf.Round(transform.position.y) + PlayerDirection.y, 0.0f);
        }
    }
    public void Recruit()
    {
        Debug.Log("Grow Called");
        //scoreManager.IncreaseScore(10);
        //SoundManager.Instance.Play(Sounds.PickRight);
        var segment = Instantiate(PlayerBody);
        segment.transform.position = BodySegments[BodySegments.Count - 1].transform.position;
        BodySegments.Add(segment);
    }
    public void Regret()
    {
        //if(scoreManager.Score() > 0)
        //{
            //scoreManager.IncreaseScore(-10);
            //SoundManager.Instance.Play(Sounds.PickWrong);
            var Garbagesegment = BodySegments[BodySegments.Count - 1];
            // remove the segment
            BodySegments.RemoveAt(BodySegments.Count - 1);
            Destroy(Garbagesegment.gameObject);
        //}
    }    
}
