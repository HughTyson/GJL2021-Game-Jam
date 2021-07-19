using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] public Transform follow_transform;
    [SerializeField] public Transform look_at;

    [SerializeField] float camera_speed;
    [SerializeField] Vector3 offset;

    Vector3 velocity = Vector3.one;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desired_pos = follow_transform.position + offset;
        Vector3 smooth_pos = Vector3. Lerp(transform.position, desired_pos, camera_speed * Time.deltaTime);
        transform.position = smooth_pos;


        Quaternion rot = Quaternion.LookRotation(look_at.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, camera_speed * Time.deltaTime);
    }
}
