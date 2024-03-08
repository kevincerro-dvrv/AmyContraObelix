using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool firstFrame;
    private float playerSpeed = 3.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private PlayerState state;

    private Transform cameraTransform;


    private Animator animator;

    private void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        SetState(PlayerState.Idle);
        firstFrame = true;

        cameraTransform = Camera.main.transform;
    }

    void Update() {
        
        groundedPlayer = controller.isGrounded;
        if(firstFrame) {
            groundedPlayer = true;
            firstFrame = false;
        }



        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = cameraTransform.forward * Input.GetAxis("Vertical") + cameraTransform.right * Input.GetAxis("Horizontal");
        move.y = 0;
        move = move.normalized;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if(state != PlayerState.Jump && groundedPlayer) {
            if (move != Vector3.zero) {
                gameObject.transform.forward = move;
                SetState(PlayerState.Run);
            } else {
                SetState(PlayerState.Idle);
            }
        }

        if(state == PlayerState.Jump && playerVelocity.y < 0) {
            SetState(PlayerState.Fall);
        }
        if(state == PlayerState.Idle || state == PlayerState.Run) {
            if(! groundedPlayer) {
                playerVelocity.y = 0;
                SetState(PlayerState.Fall);
            }
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)  {
           playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
           SetState(PlayerState.Jump);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        //Clampeamos la velocidad vertical. 
        //El máximo está en torno a la velocidad terminal de un humano en caída libre
        playerVelocity.y = Mathf.Clamp(playerVelocity.y, -50f, 50f);
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    private void SetState(PlayerState newState) {
        state = newState;
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Fall");
        animator.SetTrigger($"{newState}");
    }
}

public enum PlayerState {
    Idle,
    Run,
    Jump, 
    Fall
}
