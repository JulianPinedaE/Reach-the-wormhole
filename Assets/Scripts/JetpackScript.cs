using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackScript : MonoBehaviour
{
    public float fuelRechargeRate = 50;
    public float fuel = 100;
    public float fuelConsumption = 25;
    public float fuelUseTimer = 1;
    public float fuelRechargeTimer = 1;
    public float jetpackForce = 10;
    private PlayerController playerController;

    void Start(){
        playerController = GetComponent<PlayerController>();
    }

    void FixedUpdate(){     
        if(playerController.fly && fuel > 0){ //Fly
            fuelUseTimer -= Time.deltaTime;
            playerController.playerRB.AddForce(playerController.transform.up*jetpackForce);
        }

        if(fuelUseTimer < 0 && !playerController.onGround){ //Use fuel
            fuelUseTimer = 1;
            UseFuel(fuelConsumption);
        }

        if(playerController.onGround && fuel < 100){ //Recharge Fuel
            fuelRechargeTimer -= Time.deltaTime;
            if(fuelRechargeTimer <= 0){
                fuelRechargeTimer = 1;
                RechargeFuel(fuelRechargeRate);
            }
        }
    }

    void UseFuel(float consumption){
        fuel -= consumption;
    }
    void RechargeFuel(float recharge){        
        fuel += recharge;
        if(fuel > 100) fuel = 100;
    }
}
