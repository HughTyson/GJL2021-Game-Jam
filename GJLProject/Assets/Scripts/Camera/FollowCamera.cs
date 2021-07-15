using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] Transform follow_transform;
    [SerializeField] Transform look_at;

    [SerializeField] float camera_speed;
    [SerializeField] Vector3 offset;

    Vector3 velocity = Vector3.one;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desired_pos = follow_transform.position + offset;
        Vector3 smooth_pos = Vector3. Lerp(transform.position, desired_pos, camera_speed * Time.deltaTime);
        transform.position = smooth_pos;

        transform.LookAt(look_at);
    }
}
