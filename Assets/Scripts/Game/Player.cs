using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float simulateGravity = 9.8f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private PlayerInput playerInput;
    private float gravityValue = 9.81f;
    private Transform cameraTransform;

    private void Start() {
        // Initialize controller
        controller = gameObject.GetComponent<CharacterController>();

        Debug.Log(gameObject.GetComponent<CharacterController>());

        // Initialize Player Input
        playerInput = gameObject.GetComponent<PlayerInput>();
        Debug.Log(playerInput);

        this.cameraTransform = Camera.main.transform;
    }

    void Update() {

        // Obtain input information
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float jumpInput = playerInput.actions["Jump"].ReadValue<float>();

        Vector3 move = new(moveInput.x, 0, moveInput.y);

        move = move.x * cameraTransform.right + move.z * cameraTransform.forward;

        Debug.Log("Grounded 0: " + controller.isGrounded);

        if (jumpInput == 1) {
            move.y = jumpForce;
        }
        // controller.Move(moveSpeed * Time.deltaTime * move);

        Debug.Log("Grounded 1: " + controller.isGrounded);

        // Gravity implementation
        playerVelocity.y += simulateGravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        controller.Move(moveSpeed * Time.deltaTime * move);


        // If the player is moving, rotate its model towards the direction its moving to
        if (moveInput != Vector2.zero) {
            float rotateSpeed = 10f;

            // Slerp smoothens the rotation of the model
            transform.forward = Vector3.Slerp(transform.forward, new Vector3(move.x, 0, move.z), Time.deltaTime * rotateSpeed);
        }
        Debug.Log("Grounded 3: " + controller.isGrounded);
    }

}
