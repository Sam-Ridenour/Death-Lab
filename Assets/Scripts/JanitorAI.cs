using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class JanitorAI : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject player;
    [SerializeField] Transform janitorStartingPoint;
    [SerializeField] GameObject[] partolGuard;

    NavMeshAgent navMeshAgent;

    float distanceToTarget = Mathf.Infinity;
    float distanceToPlayer = Mathf.Infinity;
    float distanceToPartolGuard = Mathf.Infinity;
    float bubbleRange = 6f;
    float bigBubbleRange = 10f;
    bool hasBeenNotified = false;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        partolGuard = GameObject.FindGameObjectsWithTag("Patrol Guard");
    }
    void Update()
    {
        BeingNotified();
    }

    void BeingNotified()
    {
        MeshRenderer playerMesh = player.GetComponent<MeshRenderer>();
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (!hasBeenNotified)
        {
            for (int i = 0; i < partolGuard.Length; i++)
            {
                distanceToPartolGuard = Vector3.Distance(partolGuard[i].transform.position, transform.position);
                if (distanceToPartolGuard <= bubbleRange)
                {
                    hasBeenNotified = true;
                }
            }
        }
        else if (hasBeenNotified)
        {
            if (distanceToPlayer <= bubbleRange && playerMesh.enabled)
            {
                EngageTarget();
                bubbleRange = bigBubbleRange;
            }
            else if (!playerMesh.enabled)
            {
                bubbleRange = 6f;
                FindTargetDeathPoint();
            }
        }       
    }

    void FindTargetDeathPoint()
    {       
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        //if the capsule is active and the guard has notified
        if (target.activeSelf)
        {
            navMeshAgent.SetDestination(target.transform.position);   
        }
    }

    void EngageTarget()
    {
        // is the distance to the target greater than the ai's stopping distance
        if (distanceToPlayer <= bubbleRange)
        {
            // chasing player according to players current position
            navMeshAgent.SetDestination(player.transform.position);
        }    
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bubbleRange);
    }   
}
