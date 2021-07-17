using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isTriggered;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isTriggered = true;

            transform.position = originalPosition - (new Vector3(0.0f, 0.075f, 0.0f));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = false;

            transform.position = originalPosition;
        }
    }
}
