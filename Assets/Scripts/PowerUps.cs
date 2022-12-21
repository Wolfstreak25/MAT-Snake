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
    [SerializeField] private int GridHeight = 18;
    [SerializeField] private int GridWidth = 34;
    public List<Sprite> Powerups = new List<Sprite>();
    public PowerType powerType;
    private SpriteRenderer changeSprite;
    private void Start()
    {
        changeSprite = this.GetComponent<SpriteRenderer>(); 
        //Invoke("RandomizeObject", 10f);
        RandomizeObject();
        //Debug.Log(Powerups[0].powerType == PowerType.Speed);
    }

    public void RandomizeObject()
    {
        //Random power type
        int type = Random.Range(0, (int)PowerType.Count);
        powerType = (PowerType)type;
        changeSprite.sprite = Powerups[(int)powerType];
        enableThis();
        //Random Position
        this.transform.position = RandomizePosition();
    }
    public Vector2 RandomizePosition()
    {
        return new Vector2(Mathf.Round(Random.Range(0, GridWidth)), Mathf.Round(Random.Range(0, GridHeight)));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {        
        Debug.Log(other.name);
            if(other.TryGetComponent<SnakeController>(out var obj))
            {
                Debug.Log("Collision Power Up");
                obj.PowerUp(powerType);
            }
        //Invoke("disableThis", 10f);
        disableThis();
        
    }
    private void OnEnable() {Invoke("disableThis", 10f);}
    private void OnDisable() {Invoke("RandomizeObject", 10f);}
    private void disableThis(){this.gameObject.SetActive(false);}
    private void enableThis(){this.gameObject.SetActive(true);}
}
