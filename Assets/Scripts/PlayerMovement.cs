using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    public float runSpeed = 40f;
    void Start() {

    }

    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            crouch = false;
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
