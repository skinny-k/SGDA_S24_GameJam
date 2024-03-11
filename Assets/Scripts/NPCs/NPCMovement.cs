using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [Header("Wander Settings")]
    [SerializeField] bool _wanders = true;
    [SerializeField] float _wanderInterval = 4f;
    [SerializeField] float _wanderChance = 0.5f;
    [SerializeField] Vector3 _wanderCenter;
    [SerializeField] float _wanderDistance = 2f;
    
    [Header("Flee Settings")]
    [SerializeField] float _fleeAngle = 60f;
    [SerializeField] float _fleeBuffer = 7.5f;
    [SerializeField] float _maxAdditionalFlee = 5f;

    float _wanderTimer = 0;
    
    // Components
    NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _wanderTimer = Mathf.Clamp(_wanderTimer - Time.deltaTime, 0, _wanderInterval);

        if (_wanderTimer <= 0)
        {
            if (_wanders && Random.value <= _wanderChance && !_agent.hasPath)
            {
                Wander();
            }

            _wanderTimer = _wanderInterval;
        }
    }

    public void Wander()
    {
        Vector2 offset = Random.insideUnitCircle.normalized * Random.value * _wanderDistance;
        Vector3 dest = _wanderCenter + new Vector3(offset.x, 0, offset.y);

        _agent.ResetPath();
        _agent.SetDestination(dest);
    }
    
    public void RunAwayFrom(GameObject runFrom, float distance)
    {
        if (!_agent.hasPath)
        {
            float angle = Random.Range(-_fleeAngle, _fleeAngle);

            Vector3 dir = new Vector3(transform.position.x - runFrom.transform.position.x, 0, transform.position.z - runFrom.transform.position.z).normalized;
            dir = Quaternion.AngleAxis(angle, Vector3.up) * dir;
            dir = dir.normalized;

            Vector3 dest = runFrom.transform.position + (dir * (distance + _fleeBuffer + (Random.value * _maxAdditionalFlee)));

            _agent.ResetPath();
            _agent.SetDestination(dest);
        }
    }
}
