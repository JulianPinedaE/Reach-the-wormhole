using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhormholeScript : MonoBehaviour
{
    public MapController mapController;

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Player"){
            mapController.ReachWhormhole();
        }
    }
}
