using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    CharacterController controller;
    Vector3 movement = Vector3.zero;

    [SerializeField] float movement_speed = 6f;
    [SerializeField] float jump_force = 10f;
    [SerializeField] float gravity = 20;

    Vector3 movement_direction = Vector3.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {

        HandleInput();

        
        Jump();
        Move();
    }

    private void Move()
    {
        if(!controller.isGrounded)
            movement_direction.y -= gravity * Time.deltaTime;

        controller.Move(movement_direction * Time.deltaTime);
    }

    private void Jump()
    {
        if (controller.isGrounded)
            if (Input.GetKey(KeyCode.UpArrow))
                movement_direction.y = jump_force;

    }

    private void HandleInput()
    {
        movement_direction.x = Input.GetAxisRaw("Horizontal") * movement_speed;
    }

}
