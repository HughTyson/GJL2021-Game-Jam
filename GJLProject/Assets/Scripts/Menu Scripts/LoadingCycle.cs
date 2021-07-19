using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCycle : MonoBehaviour
{
    // Speed of rotation
    [SerializeField, Min(0.0f)] float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Rotate arrows clockwise by a defined speed
        transform.Rotate(Vector3.forward * (-speed));
    }
}
