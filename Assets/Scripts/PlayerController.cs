using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Range(0.2f,1)]
    public float speed = 0.5f;
    [Range(0.2f,1)]
    public float oxygen = 100;
    public float oxygenUseRate = 20;
    public float oxygenGainRate = 50;
    public float oxygenTimer = 1;
    public float jumpForce = 0.5f;
    public float maxSpeed = 10;
    private float maxJump = 10;
    public bool onGround = false;
    public bool fly = false;
    public Rigidbody playerRB;
    private Vector2 moveVector = Vector2.zero;
    private Inputactions inputActions; 
    public ConsumableBar oxygenBar;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        inputActions = new Inputactions();
        inputActions.Player.Enable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(onGround && context.started){
            playerRB.AddForce(transform.up*jumpForce*maxJump, ForceMode.Impulse);     
        }
    }

    void FixedUpdate(){     
        if(GameManager.manager.onGame){ 
            moveVector = inputActions.Player.Move.ReadValue<Vector2>();
            Vector3 moveDir = moveVector.y*transform.forward + moveVector.x*transform.right;
            playerRB.AddForce(moveDir*speed*maxSpeed);
        }
        else playerRB.constraints = RigidbodyConstraints.FreezePosition;

        HandleOxygen();
    }
    
    public void OnJetpack(InputAction.CallbackContext context)
    {
        if(context.started)
            fly = true;
            
        if(context.canceled)
            fly = false;
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
    
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }

    void HandleOxygen(){          
        if(!onGround && oxygen > 0){ 
            oxygenTimer -= Time.deltaTime;
        }

        if(oxygenTimer <= 0 && !onGround){
            oxygenTimer = 1;
            oxygen -= oxygenUseRate;
            oxygenBar.UpdateValue((int)oxygen);
            if(oxygen <= 0 && GameManager.manager.onGame) GameManager.manager.GameOver();
        }

        if(onGround && oxygen < 100){
            oxygenTimer -= Time.deltaTime;
            if(oxygenTimer <= 0){
                oxygenTimer = 1;
                oxygen += oxygenGainRate;
                oxygenBar.UpdateValue((int)oxygen);
            }
        }
        oxygen = oxygen > 100 ? 100 : oxygen;
    }
}
