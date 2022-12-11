using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public PlayerController player;

    private Quaternion groundOrientation;
    bool rotate = false;
    float rotSpeed = 0.01f;
    float rotTimeCount = 0.0f;

    void Start()
    {
        RotateCam();
        player.changeGround.AddListener(StartRotation);
    }


    void LateUpdate()
    {
        transform.position = player.transform.position - transform.forward*20 + transform.up*5;    
        if(rotate){
            SmoothRotation();
        }    
    }
    void StartRotation(){
        rotate = true;
        groundOrientation = Quaternion.LookRotation(Vector3.Cross(player.gravityOrientation, Vector3.right), -player.gravityOrientation);
    }

    void RotateCam(){
        transform.rotation = Quaternion.LookRotation(Vector3.Cross(player.gravityOrientation, Vector3.right), -player.gravityOrientation);
    }

    void SmoothRotation(){
        transform.rotation = Quaternion.Lerp(transform.rotation, groundOrientation, rotTimeCount * rotSpeed);
        rotTimeCount += Time.deltaTime;
        if(transform.rotation == groundOrientation) rotate = false;
    }
}
