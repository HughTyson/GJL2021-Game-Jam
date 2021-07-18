using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isTriggered;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    private void Start()
    {
        isTriggered = false;
        originalPosition = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {

            isTriggered = true;

            transform.localPosition = originalPosition - (new Vector3(0.0f, 0.075f, 0.0f));
        
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;

        transform.localPosition = originalPosition;
    }
}
