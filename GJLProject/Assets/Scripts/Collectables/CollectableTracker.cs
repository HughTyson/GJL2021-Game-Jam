using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableTracker : MonoBehaviour
{

    [System.Serializable]
    public struct CollectableUIType
    {
        public bool found;
        public COLLECTABLE collectbale_type;
        //UI element attatched
        public Image displayed_image;
    }


    //arry of UI elements all blacked out
    [SerializeField] public CollectableUIType[] UI_elements;

    // Start is called before the first frame update
    void Start()
    {
        Collectable.OnCollectionCollision += Collectable_OnCollectionCollision;
    }

    private void OnDestroy()
    {
        Collectable.OnCollectionCollision -= Collectable_OnCollectionCollision;
    }

    private void Collectable_OnCollectionCollision(COLLECTABLE collectable_type)
    {

        foreach(CollectableUIType obj in UI_elements)
        {
            if(obj.found != true)
            {
                if(obj.collectbale_type == collectable_type)
                {
                    //show proper UI elements
                }
            }
        }

        
    }


}
