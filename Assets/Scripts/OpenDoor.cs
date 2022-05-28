using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    float distanceToDoor = Mathf.Infinity;
    float openDoorRange = 3f;

    [SerializeField] GameObject[] doors;
    StealKey stealKey;
    private void Start()
    {
        stealKey = GetComponent<StealKey>();
        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    private void Update()
    {
        OpeningDoor();
    }

    void OpeningDoor()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            bool openDoorOnE = Input.GetKeyDown(KeyCode.E);
            bool key = false;
            distanceToDoor = Vector3.Distance(doors[i].transform.position, transform.position);

            if(stealKey.keyCount > 0)
            {
                key = true;
            }

            // press e, and within range
            if (openDoorOnE && distanceToDoor <= openDoorRange && doors[i].CompareTag("Door"))
            {
                if (key == true)
                {
                    stealKey.keyCount--;
                    doors[i].transform.Translate(Vector3.right, doors[i].transform);
                    doors[i].tag = "Dormant Door";
                    Debug.Log("You have unlocked this door.");
                }
                else if (key == false)
                {
                    Debug.Log("You do not have a key to unlock this door. " + stealKey.keyCount + " key(s).");
                }
            }
                else if (openDoorOnE && distanceToDoor <= openDoorRange && doors[i].CompareTag("Dormant Door"))
            {
                Debug.Log("You have already unlocked this door. " + stealKey.keyCount + " key(s).");
            }
        }
    }
}
