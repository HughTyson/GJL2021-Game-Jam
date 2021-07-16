using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum PLATFORM_TYPES
{
    AUTO_MOVE, UNLOCK_MOVE, ONBUTTON_MOVE
}

public abstract class MoveablePlatforms : MonoBehaviour
{

    [Tooltip("Limits of movment from it's initial position")]
    [SerializeField] Vector2 movement_limits;

    PLATFORM_TYPES platform_type = PLATFORM_TYPES.AUTO_MOVE; //set by default to auto move


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void MovePlatform() //function to call when platform is moving, will move between two limits set by designer
    {
        
    }
}
