using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{  
    [SerializeField] Transform target;
    [SerializeField] Transform startingPlace;
    [SerializeField] float chaseRange = 3f;
    

    NavMeshAgent navMeshAgent;
    
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        isMeshOn();        
    }

    public void isMeshOn()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        MeshRenderer targetMesh = target.GetComponent<MeshRenderer>();

        // is targetmesh enabled
        if (targetMesh.enabled)
        {
            // engaging player if provoked
            if (isProvoked)
            {
                EngageTarget();
            }
            // is the distance to the target less than chase range
            else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
            }
        }
        // if the targetmesh is not enabled then provoke is false
        else if (!targetMesh.enabled)
        {
            isProvoked = false;
            navMeshAgent.SetDestination(startingPlace.position);
        }
    }

    
    void EngageTarget()
    {      
            // is the distance to the target greater than the ai's stopping distance
            if (distanceToTarget >= navMeshAgent.stoppingDistance)
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
