using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public OrbitController orbit;
    Rigidbody playerRigidbody;
    public float rotationSpeed = 10;

    void Start(){
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        if(orbit){
            Vector3 gravityUp = Vector3.zero;
            if(orbit.directionalGravity){
                gravityUp = -orbit.transform.forward;
            }
            else{
                gravityUp = (transform.position - orbit.transform.position).normalized;
            }

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;

            transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed*Time.deltaTime);
            playerRigidbody.AddForce((-gravityUp*orbit.gravity)*playerRigidbody.mass);
        }
    }
}
