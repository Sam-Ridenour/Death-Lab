using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostForm : MonoBehaviour
{
    public bool ghostMode = false;
    bool spawnPoint = false;

    float ghostModeStart = 0f;
    float ghostModeCooldown = 5f;

    MeshRenderer meshRend;
    [SerializeField] GameObject capsule;
    [SerializeField] Transform deathPoint;
    [SerializeField] AudioClip backToLifeBreathfx;
    [SerializeField] AudioClip deathfx;
    AudioSource audioSource;

    void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        StopAudio();
    }

    void Update()
    {
        GhostMode();
    }

    void GhostMode()
    {
            bool ghostModeClick = Input.GetButtonDown("Fire2");

            if (Time.time > ghostModeStart)
            {
                if (ghostMode == false && ghostModeClick)
                {
                    EnteringGhostMode();
                    ghostModeStart = Time.time + ghostModeCooldown;
                }
                else if (spawnPoint == true)
                {
                    ExitingGhostMode();
                }
            }
            else if (ghostMode == true && ghostModeClick)
            {
                ExitingGhostMode();
            }
    }

    // EnteringGhostMode() add:anim
    void EnteringGhostMode()
    {       
        // turning on deathpoint mesh
            capsule.SetActive(true);
            audioSource.PlayOneShot(deathfx);
            ghostMode = true;
        // taking of the deathpoint parent
            deathPoint.parent = null;
        // setting a spawnpoint for when player is visible again
            spawnPoint = true;
        // turning off mesh
            meshRend.enabled = false;
            Debug.Log("You have entered Ghost Mode!");       
    }

    // ExitingGhostMode() add:anim
    void ExitingGhostMode()
    {      
        audioSource.PlayOneShot(backToLifeBreathfx);
        // returning back to deathpoint position
        transform.position = deathPoint.transform.position;
        // reattaching deathpoint back to player
        deathPoint.parent = GameObject.FindWithTag("Player").transform;
        // player coming back to life
        capsule.SetActive(false);
        // visible again
        ghostMode = false;
        // returning to spawnpoint and setting false so we can set another spawnpoint on rightclick
        spawnPoint = false;
        // turning on mesh
        meshRend.enabled = true;
        Debug.Log("You have exited Ghost Mode!");
    }  
    
    void StopAudio()
    {
        audioSource.Stop();
    }
}
