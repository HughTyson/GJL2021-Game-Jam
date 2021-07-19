using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextChange : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorial_display;
    [SerializeField] string new_text;

    private void OnTriggerEnter(Collider other)
    {
        tutorial_display.text = new_text;
    }

}
