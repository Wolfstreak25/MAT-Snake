using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BodyController : MonoBehaviour
{
    public PlayerType BodyType;
    [SerializeField] private int GridHeight;
    [SerializeField] private int GridWidth;
    private Vector3 BodyPosition;
    public GameObject Body;
    public void SpawnBody()
    {
        Debug.Log("SpawnBody Called");
        BodyPosition = new Vector3(Mathf.Round(Random.Range(-GridWidth, GridWidth)), Mathf.Round(Random.Range(-GridHeight, GridHeight)) , 0);
        this.transform.position = BodyPosition;
        SpawnAgain();
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
                obj.Recruit();
            }
            else
            {
                Debug.Log("Ally Lost");
                obj.Regret();
            }
            SpawnBody();
        }
    }
    IEnumerator SpawnAgain()
    {
        yield return new WaitForSeconds(5.0f);
        SpawnBody();
    }
}
