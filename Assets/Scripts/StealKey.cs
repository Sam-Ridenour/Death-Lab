using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealKey : MonoBehaviour
{
    float distanceToKey = Mathf.Infinity;
    float stealRange = 3f;
    int keyCount = 0;
    bool isKeyThere = false;

    [SerializeField] GameObject[] keys;
    GhostForm ghostForm;

    void Start()
    {
        ghostForm = gameObject.GetComponent<GhostForm>();
    }

    void Update()
    {
        if (ghostForm.ghostMode.Equals(true))
        {
            Steal();
        }
    }

    void Steal()
    {
        for(int i = 0; i < keys.Length; i++)
        {
            bool stealingClick = Input.GetButtonDown("Fire1");
            distanceToKey = Vector3.Distance(keys[i].transform.position, transform.position);
            // if the key is active then the key is there
            if (keys[i].activeSelf)
            {
                isKeyThere = true;
            } 
            // if the key is inactive then the key is not there
            else if (!keys[i].activeSelf)
            {
                isKeyThere = false;
            }
            // if left click and the keys is there and we are within range, we can steal key
            if (stealingClick && isKeyThere == true && distanceToKey <= stealRange)
            {
                keys[i].SetActive(false);               
                keyCount++;
                Debug.Log("You have " + keyCount + " keys out of " + keys.Length);
            }
            else if (stealingClick && isKeyThere == false)
            {
                Debug.Log("There is no key here.");
            }
        }             
    }
}
