using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BodyGuardAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject janitor;
    [SerializeField] GameObject[] patrolPoint;
    float chaseRange = 5f;
    float extendedChaseRange = 12f;

    NavMeshAgent navMeshAgent;

    float distanceToTarget = Mathf.Infinity;
    float distanceToPatrolPoint = Mathf.Infinity;
    float distanceToJanitor = Mathf.Infinity;
    bool isProvoked;
    

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        patrolPoint = GameObject.FindGameObjectsWithTag("Starting Point");
    }

    void Update()
    {
        PatrolAI();
    }

    void PatrolAI()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        distanceToJanitor = Vector3.Distance(janitor.transform.position, transform.position);
        MeshRenderer targetMesh = target.GetComponent<MeshRenderer>();

        // engaging player if provoked
        if (isProvoked)
        {
            EngageTarget();
            // making the chase range bigger
            chaseRange = extendedChaseRange;
            if (distanceToTarget >= chaseRange || !targetMesh.enabled)
            {
                // setting chase range back to original size but if player is within extended chase range will engage
                chaseRange = 5f;
                navMeshAgent.SetDestination(janitor.transform.position);
                if(distanceToJanitor <= chaseRange)
                {
                    isProvoked = false;
                }
            }
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
        else if(!isProvoked)
        {
            Patroling();
        }
    }

    void Patroling()
    {
        for (int i = 0; i < patrolPoint.Length; i++)
        {
            // adds the ability to move to the next partol point
            distanceToPatrolPoint = Vector3.Distance(patrolPoint[i].transform.position, transform.position);
            // sets the ai's destination to the next partol point with starting tag in the for loop
            navMeshAgent.SetDestination(GameObject.FindWithTag("Starting Point").transform.position);

            if (distanceToPatrolPoint <= chaseRange)
            {
                // turns tag null so ai can move to next tag
                patrolPoint[i].tag = "Null Point";
            }
            else if (distanceToPatrolPoint >= chaseRange && patrolPoint[i].CompareTag("Null Point"))
            {
                // turns tag back to starting point once chaseRange is out of bound so can continue loop
                patrolPoint[i].tag = "Starting Point";

            }
        }
    }

    void EngageTarget()
    {
        // is the distance to the target greater than the ai's stopping distance
        if (distanceToTarget <= chaseRange)
        {
            ChaseTarget();
        }
        // is the distance to the target less than the ai's stopping distance
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            CatchTarget();
        }
    }

    void ChaseTarget()
    {
        // chasing player according to players current position
        navMeshAgent.SetDestination(target.position);
    }

    void CatchTarget()
    {
        // catching player
        Debug.Log("I have caught " + target.name);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }   
}
