using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum PLATFORM_TYPES
{
    AUTO_MOVE, UNLOCK_MOVE, ONPRESSURE_MOVE
}

public class MoveablePlatforms : MonoBehaviour
{

    [SerializeField] Vector3[] points;
    [SerializeField] PLATFORM_TYPES platform_type = PLATFORM_TYPES.AUTO_MOVE; //set by default to auto move

    [Header("Trigger to activate")]
    [SerializeField] PressurePlate[] connected_plates;


    int point_number = 0;
    Vector3 current_target;

    float tolerance;
    float speed =  2;
    float delay_time = 2;

    float delay_start;

    private void Start()
    {
        if(points.Length > 0)
        {
            current_target = points[0];
        }

        tolerance = speed * Time.deltaTime;

    }

    private void Update()
    {

        UpdateTarget();
    }

    public void MovePlatform() 
    {
        Vector3 pos = current_target - transform.position;
        transform.position += (pos / pos.magnitude) * speed * Time.deltaTime;
        if(pos.magnitude < tolerance)
        {
            transform.position = current_target;
            delay_start = Time.time;
        }
    }

    public void UpdateTarget()
    {

        switch (platform_type)
        {
            case PLATFORM_TYPES.AUTO_MOVE:
                {

                    if (transform.position != current_target)
                    {
                        MovePlatform();
                    }
                    else
                    {
                        if (Time.time - delay_start > delay_time)
                        {
                            NextTarget();
                        }                       
                    }
                }
                break;
            case PLATFORM_TYPES.UNLOCK_MOVE:
                {
                    //check refernce to pressure plate, if it has been pressed then change to AUTO_MOVE

                    foreach(PressurePlate plate in connected_plates)
                    {
                        if(!plate.isTriggered)
                        {
                            return;
                        }
                    }

                    platform_type = PLATFORM_TYPES.AUTO_MOVE;

                }
                break;
            case PLATFORM_TYPES.ONPRESSURE_MOVE:
                {
                    //constantly check if the pressure plate is down - if pressed down then move the platform

                    foreach (PressurePlate plate in connected_plates)
                    {
                        if (!plate.isTriggered)
                        {
                            return;
                        }
                    }

                    if (transform.position != current_target)
                    {
                        MovePlatform();
                    }
                    else
                    {
                        if (Time.time - delay_start > delay_time)
                        {
                            NextTarget();
                        }
                    }

                }
                break;
            default:
                break;
        }
    }

    void NextTarget()
    {
        point_number++;

        if(point_number >= points.Length)
        {
            point_number = 0;
        }

        current_target = points[point_number];
    }


    //make the platform a parent of the character so they move with it
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

}
