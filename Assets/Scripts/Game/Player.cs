using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0); // By logic, the input is X & Y only so the inputVector is not Vector3 automatically.

        // this is Legacy Input Manager, an old style of programming control:

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = 1;
        }

        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = -1;
        }

        // There's this problem where moving diagonally makes the character faster
        // This function call normalizes the vector
        inputVector = inputVector.normalized;

        // Create Vector3 from Vector2
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Move the player
        // DeltaTime should only be multiplied on moving stuff
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Sets the player rotation to movedir
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
}
