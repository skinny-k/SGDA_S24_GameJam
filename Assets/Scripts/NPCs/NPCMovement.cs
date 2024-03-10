using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField] float _fleeAngle = 60f;
    [SerializeField] float _fleeBuffer = 7.5f;
    
    // Components
    NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    public void RunAwayFrom(GameObject runFrom, float distance)
    {
        if (!_agent.hasPath)
        {
            float angle = Random.Range(-_fleeAngle, _fleeAngle);
            Vector3 dir = (transform.position - runFrom.transform.position).normalized;
            dir = Quaternion.AngleAxis(angle, Vector3.up) * dir;
            Vector3 dest = runFrom.transform.position + (dir * (distance + _fleeBuffer));

            _agent.SetDestination(dest);
        }
        
    }
}
