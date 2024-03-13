using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool firstFrame;
    private float playerFreeSpeed = 3.0f;
    private float playerPushSpeed = 2.0f;
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
        Vector3 displacement = move * Time.deltaTime * playerSpeed;
        controller.Move(displacement);

        if(move != Vector3.zero && groundedPlayer) {
            CheckForPushable(displacement);
        }

        if(state != PlayerState.Jump && groundedPlayer) {
            if (move != Vector3.zero) {
                gameObject.transform.forward = move;
                if(state != PlayerState.Push) {
                    SetState(PlayerState.Run);
                }
            } else {
                SetState(PlayerState.Idle);
                playerSpeed = playerFreeSpeed;
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

    private void CheckForPushable(Vector3 displacement) {
        if(Physics.Raycast(transform.position + Vector3.up, displacement, out RaycastHit hit, 0.5f, ~0, QueryTriggerInteraction.Ignore)) {
            if(hit.collider.gameObject.CompareTag("Pushable")) {
                Debug.Log("[Player] CheckForPushable encontrado pushable");
                hit.collider.GetComponent<Pushable>().Move(displacement);

                if(state != PlayerState.Push) {
                    SetState(PlayerState.Push);
                    playerSpeed = playerPushSpeed;
                }
            } 
        } else if(state == PlayerState.Push) {        
            SetState(PlayerState.Run);
            playerSpeed = playerFreeSpeed;
        } 
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
    Fall,
    Push
}
