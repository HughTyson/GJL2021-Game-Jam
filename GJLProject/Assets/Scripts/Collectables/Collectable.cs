using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum COLLECTABLE
{ 
    SCREWDRIVER, SCREW
}

public class Collectable : MonoBehaviour
{

    public COLLECTABLE collectable_type;

    public static event Action<COLLECTABLE> OnCollectionCollision;

    // Speed of rotation
    [SerializeField, Min(0.0f)] float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate collectable clockwise by a defined speed
        //transform.Rotate(Vector3.up * (-speed));

        transform.Rotate(0, 20 * Time.deltaTime * speed, 0);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {

    //        OnCollectionCollision?.Invoke(collectable_type);

    //    }

    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {

            if (!other.isTrigger)
            {
                OnCollectionCollision?.Invoke(collectable_type);
                Destroy(gameObject);
            }

        }
    }



}
