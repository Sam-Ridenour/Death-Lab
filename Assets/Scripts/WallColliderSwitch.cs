using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderSwitch : MonoBehaviour
{
    Collider walls;
    GhostForm ghostForm;
    [SerializeField] GameObject player;
    
    void Start()
    {
        walls = GetComponent<Collider>();
        ghostForm = player.GetComponent<GhostForm>();
    }

    // Update is called once per frame
    void Update()
    {       
        if (ghostForm.ghostMode.Equals(false))
        {
            walls.enabled = true;
        } else if(ghostForm.ghostMode.Equals(true))
        {
            walls.enabled = false;
        }
    }
}
