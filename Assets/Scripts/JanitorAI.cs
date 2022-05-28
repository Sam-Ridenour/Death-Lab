using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JanitorAI : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Transform janitorStartingPoint;
    [SerializeField] GameObject[] partolGuard;

    NavMeshAgent navMeshAgent;

    float distanceToTarget = Mathf.Infinity;
    float distanceToPartolGuard = Mathf.Infinity;
    float bubbleRange = 6f;
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
            FindTargetDeathPoint();
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

    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bubbleRange);
    }
}
