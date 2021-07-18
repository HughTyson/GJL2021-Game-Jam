using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_ : AutoCleanupSingleton<GM_>
{
    
    Members members;
    [SerializeField] AudioManager audio_manager;
    [SerializeField] CustomeSceneManager scene_manager;

    public new static GM_ instance
    {
        get
        {
            //If the instance is null, meaning there is not currently a component of this type
            if (_instance == null)
            {
                //Attempt to find an instance of this type
                _instance = FindObjectOfType<GM_>();


                //If the instance is still null
                //Create a new instance and return that instead
                if (_instance == null)
                {
                    Debug.LogWarning("Component type " + typeof(GM_) + " could not be found. Instantiating a new one");
                    _instance = Instantiate(Resources.Load<GameObject>("GM_")).GetComponent<GM_>();
                }
            }
            return _instance;
        }
    }

    // Creates an interface to the GM_ to hide MonoBehavour methods
    public class Members
    {
        public AudioManager audio;
        public CustomeSceneManager scene_mgr;
    };

    public Members GetMembers
    {
        get { return members; } // returns an interface to the GM_ to hide MonoBehavour methods
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        members = new Members();

        members.audio = audio_manager;
        members.scene_mgr = scene_manager;

    }
    private void OnDestroy()
    {

        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
            Destroy(gameObject.transform.GetChild(i).gameObject);
    }



}
