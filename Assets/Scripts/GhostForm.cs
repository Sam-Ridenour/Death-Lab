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
        bool rightClick = Input.GetButtonDown("Fire2");
        

        if (Time.time > ghostModeStart)
        {
            if (ghostMode == false && rightClick)
            {
                EnteringGhostMode();
                ghostModeStart = Time.time + ghostModeCooldown;
            }
            else if(spawnPoint == true)
            {
                ExitingGhostMode();
            }
        }
        else if (ghostMode == true && rightClick)
        {
            ExitingGhostMode();
        }
    }

    // EnteringGhostMode() add:anim
    void EnteringGhostMode()
    {
        capsule.SetActive(true);
        audioSource.PlayOneShot(deathfx);
        ghostMode = true;
        deathPoint.parent = null;
        spawnPoint = true;
        meshRend.enabled = false;
        Debug.Log("You have entered Ghost Mode!");
    }

    // ExitingGhostMode() add:anim
    void ExitingGhostMode()
    {
        capsule.SetActive(false);
        audioSource.PlayOneShot(backToLifeBreathfx);
        transform.position = deathPoint.transform.position;
        deathPoint.parent = GameObject.FindWithTag("Player").transform;
        ghostMode = false;
        spawnPoint = false;
        meshRend.enabled = true;
        Debug.Log("You have exited Ghost Mode!");
    }  
    
    void StopAudio()
    {
        audioSource.Stop();
    }
}
