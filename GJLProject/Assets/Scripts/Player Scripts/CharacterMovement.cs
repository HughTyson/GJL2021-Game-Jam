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

    private Quaternion look_left;
    private Quaternion look_right;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        look_right = transform.rotation;
        look_left = transform.rotation * Quaternion.Euler(0, 180, 0); ;
    }


    private void FixedUpdate()
    {

        HandleInput();
        
        Jump();
        Move();
        RotateCharacter();
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

    private void RotateCharacter()
    {
        if(movement_direction.x > 0)
        {
            transform.rotation = look_right;

        }
        else if(movement_direction.x < 0)
        {
            transform.rotation = look_left;
        }
    }

    private void HandleInput()
    {
        movement_direction.x = Input.GetAxisRaw("Horizontal") * movement_speed;
    }

}
