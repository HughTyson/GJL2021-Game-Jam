using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerEvent : MonoBehaviour
{

    public System.Action<Collider> ActionOccured_OnTriggerStay;
    public System.Action<Collider> ActionOccured_OnTriggerEnter;
    public System.Action<Collider> ActionOccured_OnTriggerExit;


    private void OnTriggerStay(Collider other)
    {
        ActionOccured_OnTriggerStay?.Invoke(other);
    }
    private void OnTriggerEnter(Collider other)
    {
        ActionOccured_OnTriggerEnter?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        ActionOccured_OnTriggerExit?.Invoke(other);
    }

}
