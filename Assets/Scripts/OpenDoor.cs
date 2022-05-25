using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        OpeningDoor();
    }

    void OpeningDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //open door (rotate 90)
            //sfx
            //animation
        }
    }
}
