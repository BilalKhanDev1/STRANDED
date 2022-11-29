using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContoller : MonoBehaviour
{
    [SerializeField] private float stoppingDistance = 3;

    private float timeOfLastAttack = 0;
    private float attackRange = 5;
    private bool hasStopped = false;

    private NavMeshAgent agent = null;
    private EnemyStats stats = null;
   
    [SerializeField] private Transform target;

   
    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        
            MoveToTarget();
        
           
        
    }
    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        Debug.Log("Moving towards player");
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget <= agent.stoppingDistance)
        {
            if(!hasStopped)
            {
                hasStopped = true;
                timeOfLastAttack = Time.time;   
            }

           
            if (Time.time >= timeOfLastAttack + stats.attackSpeed)
            {
                timeOfLastAttack = Time.time;
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                AttackTarget(targetStats);
            }
            
        }
        else
        {
            if(hasStopped)
            {
                hasStopped= false;  
            }
        }
    }

    private void RotateToTarget()
    {

       Vector3 direction = target.position - transform.position;

       Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

       transform.rotation = rotation;
    }

    private void AttackTarget(CharacterStats statsToDamage)
    {
        stats.DealDamage(statsToDamage);
    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();
        
    }
}
