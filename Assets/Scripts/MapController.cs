using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public int level = 4;
    List<Transform> platforms = new List<Transform>();
    public Vector3 mapMinBoundaries = new Vector3(20, 0, 0);
    public Vector3 mapMaxBoundaries = new Vector3(40, 20, 20);
    public Transform platformPrefab;
    public PlayerController player;

    void Start(){
        CreateMap();
    }


    void CreateMap(){
        Vector3 platformPos = new Vector3(0, 0, 0);
        for (int i = 0; i < level; i++)
        {
            Transform newPlatform = Instantiate(platformPrefab, platformPos, Quaternion.identity);  
            newPlatform.Rotate(90, 0, 0);
            if(i > 0){
                newPlatform.Rotate(0, Random.Range(-120, 120), 0);   
                newPlatform.position += newPlatform.forward*30;
            }
            platformPos = new Vector3(0,0,
                platformPos.z + Random.Range(mapMinBoundaries.z, mapMaxBoundaries.z));
            platforms.Add(newPlatform);
        }

        player.transform.position = platforms[0].position - (platforms[0].forward*5);
    }
}
