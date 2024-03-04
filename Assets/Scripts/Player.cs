using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private Animator animator;

    private void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        SetState(PlayerState.Idle);
    }

    void Update() {
        groundedPlayer = controller.isGrounded;


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = move.normalized;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
            SetState(PlayerState.Run);
        } else {
            SetState(PlayerState.Idle);
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)  {
           playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        //Clampeamos la velocidad vertical. 
        //El máximo está en torno a la velocidad terminal de un humano en caída libre
        playerVelocity.y = Mathf.Clamp(playerVelocity.y, -50f, 50f);
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    private void SetState(PlayerState newState) {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.SetTrigger($"{newState}");
    }
}

public enum PlayerState {
    Idle,
    Run
}
