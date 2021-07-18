using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{


    Rigidbody controller;

    [SerializeField] float movement_speed = 6f;
    [SerializeField] float grabbing_movement_speed = 3f;
    [SerializeField] float jump_height = 10f;
    [SerializeField] BoxCollider grabTrigger;
    [SerializeField] GameObject foldedBody;
    [SerializeField] BoxCollider foldedCollider;
    [SerializeField] GameObject fullBody;
    [SerializeField] BoxCollider fullCollider;


    [SerializeField] ColliderTriggerEvent triggerEvent_FrontSide;
    [SerializeField] Transform ray_point1;
    [SerializeField] Transform ray_point2;

    bool is_grounded;
    bool is_grabbing;
    bool is_looking_left;

    bool touching_front;
    Vector3 movement_direction = Vector3.zero;

    private Quaternion look_left;
    private Quaternion look_right;

    float distance_to_ground;

    private void Awake()
    {
        controller = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {


        look_right = transform.rotation;
        look_left = transform.rotation * Quaternion.Euler(0, 180, 0); ;

        is_grounded = false;
        is_grabbing = false;
        is_looking_left = transform.rotation.eulerAngles.y > 90f;
        touching_front = false;
    }

    private void OnEnable()
    {
        controller.constraints = RigidbodyConstraints.None;
        controller.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        grabTrigger.enabled = true;
        foldedBody.SetActive(false);
        foldedCollider.enabled = false;
        fullCollider.enabled = true;
        controller.isKinematic = false;
        controller.useGravity = true;

        triggerEvent_FrontSide.ActionOccured_OnTriggerEnter += LeftSideTriggerEnter;
        triggerEvent_FrontSide.ActionOccured_OnTriggerExit += LeftSideTriggerExit;
        fullBody.SetActive(true);
    }

    private void OnDisable()
    {
        controller.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        grabTrigger.enabled = false;
        foldedBody.SetActive(true);
        foldedCollider.enabled = true;

        fullCollider.enabled = false;
        controller.isKinematic = true;
        controller.useGravity = false;


        triggerEvent_FrontSide.ActionOccured_OnTriggerEnter -= LeftSideTriggerEnter;
        triggerEvent_FrontSide.ActionOccured_OnTriggerExit -= LeftSideTriggerExit;
        fullBody.SetActive(false);
    }

    private void Update()
    {
        HandleInput();
        RotateCharacter();
    }

    void LeftSideTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
            touching_front = true;
    }
    void LeftSideTriggerExit(Collider other)
    {
        if (!other.isTrigger)
            touching_front = false;
    }


    private void FixedUpdate()
    {


        Move();
        Jump();

    }

    private void Move()
    {

        Vector3 velocity = controller.velocity;

        //prev version
        //if (is_grabbing)
        //    controller.MovePosition(controller.position + movement_direction * grabbing_movement_speed * Time.fixedDeltaTime);
        //else
        //    controller.MovePosition(controller.position + movement_direction * movement_speed * Time.fixedDeltaTime);


        if (is_grabbing)
             velocity.x = movement_direction.x * grabbing_movement_speed;
        else
             velocity.x = movement_direction.x * movement_speed;

        if (touching_front)
        {
            velocity.x = 0;
        }

        controller.velocity = velocity;
    }

    private void Jump()
    {
        float distance_to_ground = GetComponent<Collider>().bounds.extents.y;

        is_grounded = Physics.Raycast(ray_point1.position, Vector3.down, distance_to_ground/2);

        if(!is_grounded)
            is_grounded = Physics.Raycast(ray_point2.position, Vector3.down, distance_to_ground / 2);

        Debug.DrawRay(transform.position, Vector3.down, Color.black, distance_to_ground/2);

        if (movement_direction.z == 1 && is_grounded && !is_grabbing && controller.velocity.y > -0.00001f && controller.velocity.y < 0.00001f)
        {
            controller.AddForce(Vector3.up * Mathf.Sqrt(jump_height * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    private void RotateCharacter()
    {
        if (!is_grabbing)
        {
            if (movement_direction.x > 0)
            {
                is_looking_left = false;
                transform.rotation = look_right;
            }
            else if (movement_direction.x < 0)
            {
                is_looking_left = true;
                transform.rotation = look_left;
            }
        }
    }

    private void HandleInput()
    {
        movement_direction.x = Input.GetAxisRaw("Horizontal");
        movement_direction.z = Input.GetAxisRaw("Vertical");
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Crate") && is_grounded)
        {
            if ((Input.GetKeyDown("right ctrl") || Input.GetKeyDown("left ctrl")))
            {
                if(!is_grabbing)
                {
                    is_grabbing = true;

                    other.attachedRigidbody.useGravity = false;
                    other.attachedRigidbody.isKinematic = true;

                    if (other.transform.position.x > transform.position.x)
                    {
                        if (is_looking_left)
                        {
                            transform.rotation = look_right;
                            is_looking_left = false;
                        }
                    }
                    else
                    {
                        if (!is_looking_left)
                        {
                            is_looking_left = true;
                            transform.rotation = look_left;
                        }
                    }

                    other.transform.parent.parent = transform;
                    if (is_looking_left)
                        other.transform.parent.position = other.transform.parent.position + new Vector3(-0.1f, 0f, 0f);
                    else
                        other.transform.parent.position = other.transform.parent.position + new Vector3(0.1f, 0f, 0f);

                    Debug.Log("Successful Grab!");
                }
            }
            else if(Input.GetKeyUp("right ctrl") || Input.GetKeyUp("left ctrl"))
            {
                if (is_grabbing)
                {
                    is_grabbing = false;
                    other.attachedRigidbody.useGravity = true;
                    other.attachedRigidbody.isKinematic = false;
                    other.transform.parent.parent = null;

                    Debug.Log("Successful Un-Grab!");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if(other.CompareTag("Crate") && is_grabbing)
        //{
        //    is_grabbing = false;
        //    other.attachedRigidbody.useGravity = true;
        //    other.attachedRigidbody.isKinematic = false;
        //    other.transform.parent.parent = null;

        //    Debug.Log("Forced Un-Grab!");
        //}
    }
}
