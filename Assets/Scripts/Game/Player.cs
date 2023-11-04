using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private float jumpSpeed = 3.0f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float simulateGravity = -9.81f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Transform cameraTransform;
    
    private Vector3 playerVelocity;
    private Vector3 turnVelocity;

    private bool groundedPlayer;

    private void Start() {
        // Initialize controller
        controller = gameObject.GetComponent<CharacterController>();

        // Initialize Player Input
        playerInput = gameObject.GetComponent<PlayerInput>();

        this.cameraTransform = Camera.main.transform;
    }


/*    private void Update() {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float jumpInput = playerInput.actions["Jump"].ReadValue<float>();

        // if (controller.isGrounded) {
        playerVelocity = transform.forward * moveSpeed * moveInput.x;
        turnVelocity = transform.up * rotationSpeed * moveInput.x;
        if (jumpInput == 1) {
            playerVelocity.y = jumpSpeed;
        }
        // }

        // Adding Gravity
        playerVelocity.y += simulateGravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }*/



    void Update() {

        // Obtain input information
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        float jumpInput = playerInput.actions["Jump"].ReadValue<float>();

        Vector3 move = new(moveInput.x, 0, moveInput.y);

        move = move.x * cameraTransform.right + move.z * cameraTransform.forward;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * moveSpeed);

        if (jumpInput == 1 && groundedPlayer) {
            Debug.Log("ADDED!");
           playerVelocity.y += Mathf.Sqrt(jumpSpeed * -3.0f * simulateGravity);
        }

        playerVelocity.y += simulateGravity * Time.deltaTime;
        // Move the player using CharacterController
        controller.Move(playerVelocity * Time.deltaTime);

        // If the player is moving, rotate its model towards the direction its moving to
        if (moveInput != Vector2.zero) {

            // Slerp smoothens the rotation of the model
            transform.forward = Vector3.Slerp(transform.forward, new Vector3(move.x, 0, move.z), Time.deltaTime * rotationSpeed);
        }
    }
}
