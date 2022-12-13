using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public PlayerController player;


    void LateUpdate()
    {
        transform.position = player.transform.position - transform.forward*10 + transform.up*5;
        transform.up = player.transform.up;  
    }
}
