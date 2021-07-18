using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody controller;

    [SerializeField] float movement_speed = 6f;
    [SerializeField] float grabbing_movement_speed = 3f;
    [SerializeField] float jump_height = 10f;

    bool is_grounded;
    bool is_grabbing;

    Vector3 movement_direction = Vector3.zero;

    private Quaternion look_left;
    private Quaternion look_right;

    float distance_to_ground;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Rigidbody>();

        look_right = transform.rotation;
        look_left = transform.rotation * Quaternion.Euler(0, 180, 0); ;

        is_grabbing = false;
    }


    private void FixedUpdate()
    {

        HandleInput();

        //check if the character is grounded




        Move();
        Jump();
        RotateCharacter();
    }

    private void Move()
    {
        controller.MovePosition(controller.position + movement_direction * movement_speed * Time.fixedDeltaTime);
    }

    private void Jump()
    {


        bool is_grounded = Physics.Raycast(transform.position, Vector3.down, distance_to_ground + 0.1f);
        Debug.DrawRay(transform.position, Vector3.down, Color.black, distance_to_ground + 0.1f);

        if (movement_direction.z == 1 && is_grounded)
        {
            controller.AddForce(Vector3.up * Mathf.Sqrt(jump_height * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }


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
        movement_direction.x = Input.GetAxisRaw("Horizontal");
        movement_direction.z = Input.GetAxisRaw("Vertical");
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crate") && (Input.GetKeyDown("right ctrl") || Input.GetKeyDown("left ctrl")))
        {
            Debug.Log("Attempting Grab");

            if (!is_grabbing)
            {
                is_grabbing = true;
                other.transform.parent.parent = transform;
                other.attachedRigidbody.useGravity = false;
                other.attachedRigidbody.isKinematic = true;

                Debug.Log("Successful Grab!");
            }
        }
        else if (!Input.GetKey("right ctrl") && !Input.GetKey("left ctrl"))
        {
            if (is_grabbing)
            {
                is_grabbing = false;
                other.transform.parent.parent = null;
                other.attachedRigidbody.useGravity = true;
                other.attachedRigidbody.isKinematic = false;

                Debug.Log("Successful Un-Grab!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if(is_grabbing)
        //{
        //    is_grabbing = false;
        //    other.transform.parent.parent = null;
        //    other.attachedRigidbody.isKinematic = false;

        //    Debug.Log("Forced Un-Grab!");
        //}
    }
}
