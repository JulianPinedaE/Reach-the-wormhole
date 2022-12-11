using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Range(0.2f,1)]
    public float speed = 0.5f;
    [Range(0.2f,1)]
    public float jumpForce = 0.5f;
    public float maxSpeed = 10;
    private float maxJump = 10;
    public bool onGround = false;
    public bool fly = false;
    public Rigidbody playerRB;
    private Vector2 moveVector = Vector2.zero;
    private Inputactions inputActions; 
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        inputActions = new Inputactions();
        inputActions.Player.Enable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(onGround && context.started)
            playerRB.AddForce(Vector3.up*jumpForce*maxJump, ForceMode.Impulse);
    }

    void FixedUpdate(){
        moveVector = inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(moveVector.x, 0.0f, moveVector.y);
        playerRB.AddForce(moveDir*speed*maxSpeed);
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
    
    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }
}
