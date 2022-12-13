using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public float gravity = 10;
    public bool directionalGravity = true;
    // Start is called before the first frame update
    
    void OnTriggerEnter(Collider collider){
        GravityController gravityController = collider.GetComponent<GravityController>();
        if(gravityController){
            gravityController.orbit = GetComponent<OrbitController>();
        }
    }
}
