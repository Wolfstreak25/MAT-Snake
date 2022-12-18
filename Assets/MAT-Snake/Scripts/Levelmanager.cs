using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelmanager : MonoBehaviour
{
    [SerializeField] private int GridHeight;
    [SerializeField] private int GridWidth;
    private Vector3 BodyPosition;
    public GameObject Body;
    private void Start() 
    {
        Debug.Log("BodySpawn Log");
        BodyPosition = new Vector3(Random.Range(-GridWidth, GridWidth), Random.Range(-GridHeight, GridHeight) , 0);
        Instantiate(Body , BodyPosition , Quaternion.identity);
    }
    
} 