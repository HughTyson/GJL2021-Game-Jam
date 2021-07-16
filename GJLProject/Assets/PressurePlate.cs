using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");

        this.gameObject.transform.position = originalPosition - (new Vector3(0.0f, 0.075f, 0.0f));
    }

    public void OnTriggerExit(Collider other)
    {
        this.gameObject.transform.position = originalPosition;
    }
}
