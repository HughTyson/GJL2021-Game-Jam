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
        public Sprite sprite;
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

        for(int i = 0; i < UI_elements.Length; i++)
        {
            
            if(UI_elements[i].found != true)
            {
                if (UI_elements[i].collectbale_type == collectable_type)
                {
                    //show proper UI elements
                    UI_elements[i].displayed_image.sprite = UI_elements[i].sprite;
                    UI_elements[i].found = true;
                    GM_.instance.GetMembers.audio.PlaySFX("Collectable");
                    break;
                }
            }
        }

        
    }


}
