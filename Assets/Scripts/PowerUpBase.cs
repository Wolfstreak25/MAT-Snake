using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    public PowerType type = PowerType.Speed;
    public int timeLapse = 15;
    private void OnEnable() 
    {
        Invoke ("DisableThis", timeLapse);

    }
    private void DisableThis()
    {
        this.gameObject.SetActive(false);
    }
}
