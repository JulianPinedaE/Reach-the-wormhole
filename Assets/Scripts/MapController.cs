using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Transform[] platforms;
    public PlayerController player;
    private float distanceToGround = float.PositiveInfinity;
    private int closestGround = 0;

    void Start(){
        player.SetGravityDir(platforms[closestGround].forward);
    }


    void LateUpdate(){
        ClosestGroundDistance();
    }

    void ClosestGroundDistance(){
        distanceToGround = Vector3.Distance(player.transform.position, platforms[closestGround].position);
        for (int i = 0; i < platforms.Length; i++)
        {
            Transform currentPlatform = platforms[i];
            float currentDistance = Vector3.Distance(player.transform.position, currentPlatform.position);
            if(currentDistance < distanceToGround){
                closestGround = i;
                player.SetGravityDir(currentPlatform.forward);
            }
        }
    }
}
