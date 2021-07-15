using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody body;
    Vector3 movement = Vector3.zero;

    [SerializeField] float movement_force = 100f;
    [SerializeField] float jump_force = 50f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {


        Move();
        Jump();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            body.AddForce(movement_force * Vector3.left);

        if (Input.GetKey(KeyCode.RightArrow))
            body.AddForce(movement_force * Vector3.right);
    }

    private void Jump()
    {
        
        if (Input.GetKey(KeyCode.UpArrow))
            body.AddForce(jump_force * Vector3.up);

    }

}
