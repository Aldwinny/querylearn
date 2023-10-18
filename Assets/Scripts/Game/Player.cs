using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private PlayerInput playerInput;
    private float gravityValue = 9.81f;
    private Transform cameraTransform;

    private void Start() {
        // Initialize controller
        controller = gameObject.AddComponent<CharacterController>();

        // Initialize Player Input
        playerInput = gameObject.GetComponent<PlayerInput>();
        Debug.Log(playerInput);

        cameraTransform = Camera.main.transform;
    }

    void Update() {
        
        // Obtain input information
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Console.WriteLine(input);
        Vector3 move = new(-input.x, 0, -input.y);

        // This
        move = move.x * cameraTransform.right + move.z * cameraTransform.forward;
        move.y = 0f;
        controller.Move(moveSpeed * Time.deltaTime * move);

        // playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (input != Vector2.zero) {
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, move, Time.deltaTime * rotateSpeed);
        }
    }
}
