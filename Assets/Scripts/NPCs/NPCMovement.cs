using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [Header("Wander Settings")]
    [SerializeField] protected bool _wanders = false;
    [Tooltip("How often the NPC should try to wander")]
    [SerializeField] protected float _wanderInterval = 4f;
    [Tooltip("The likelihood that the NPC decides to move on any Wander Interval")]
    [SerializeField] protected float _wanderChance = 0.5f;
    [Tooltip("The central point the NPC should wander around near")]
    [SerializeField] protected Vector3 _wanderCenter;
    [Tooltip("The distance from Wander Center the NPC can wander")]
    [SerializeField] protected float _wanderDistance = 2f;

    [Header("Tremble Settings")]
    [Tooltip("The speed of the tremble")]
    [SerializeField] protected float _trembleInterval = 0.1f;
    [Tooltip("The intensity of the tremble")]
    [SerializeField] protected float _trembleMagnitude = 5f;
    
    [Header("Flee Settings")]
    [Tooltip("The angle it is possible for the NPC to flee from the player at. A value of 0 will force the NPC to run directly away from the player, while a value of 180 will allow the NPC to flee in any direction.")]
    [SerializeField] protected float _fleeAngle = 60f;
    [Tooltip("The amount of extra distance the NPC should flee past its Max Look Distance.")]
    [SerializeField] protected float _fleeBuffer = 7.5f;
    [Tooltip("The maximum amount of additional distance the NPC should flee (a random amount of extra distance is added on top of the minimum of Max Look Distance + Flee Buffer)")]
    [SerializeField] protected float _maxAdditionalFlee = 5f;
    [Tooltip("Forces the NPC to find a new spot to flee to if the player is within this distance of its current destination")]
    [SerializeField] protected float _fleeRepathDistance = 5f;

    [Header("Return Settings")]
    [Tooltip("At this distance from the player, the NPC will teleport back to its base position")]
    [SerializeField] float _returnDistance = 25f;

    protected Vector3 _dest;
    protected Vector3 _basePosition;
    
    float _wanderTimer = 0;
    Quaternion _trembleRot = Quaternion.identity;
    float _trembleTimer = 0f;
    
    // Components
    protected NPCBase _npc;
    protected NavMeshAgent _agent;

    protected virtual void Awake()
    {
        _npc = GetComponent<NPCBase>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Wander()
    {
        Vector2 offset = Random.insideUnitCircle.normalized * Random.value * _wanderDistance;
        _dest = _wanderCenter + new Vector3(offset.x, 0, offset.y);

        _agent.ResetPath();
        _agent.SetDestination(_dest);
    }
    
    public virtual void RespondToKiller(PlayerBase killer)
    {
        if (!_agent.hasPath || Vector3.Distance(_dest, killer.transform.position) <= _fleeRepathDistance)
        {
            float angle = Random.Range(-_fleeAngle, _fleeAngle);

            Vector3 dir = new Vector3(transform.position.x - killer.transform.position.x, 0, transform.position.z - killer.transform.position.z).normalized;
            dir = Quaternion.AngleAxis(angle, Vector3.up) * dir;
            dir = dir.normalized;

            _dest = killer.transform.position + (dir * (_npc.MaxLookDistance + _fleeBuffer + (Random.value * _maxAdditionalFlee)));

            _agent.ResetPath();
            _agent.SetDestination(_dest);
        }
    }

    public virtual void RespondToScary(PlayerBase scary)
    {
        _trembleTimer -= Time.deltaTime;

        if (_trembleTimer <= 0)
        {
            _trembleTimer = _trembleInterval;
            Vector3 eulerRot = new Vector3(Random.value, Random.value, Random.value) * _trembleMagnitude;
            _trembleRot = Quaternion.Euler(eulerRot);
        }

        transform.GetChild(0).localRotation = Quaternion.RotateTowards(transform.GetChild(0).localRotation, _trembleRot, _trembleMagnitude * (1 / _trembleInterval) * Time.deltaTime);

        Vector3 lookDir = scary.transform.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), 90f * Time.deltaTime);
    }

    public virtual void RespondToPassive(PlayerBase passive = null)
    {
        if (passive != null && Vector3.Distance(transform.position, passive.transform.position) <= _npc.FocusDistance)
        {
            Vector3 lookDir = passive.transform.position - transform.position;
            lookDir.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), 90f * Time.deltaTime);
        }
        else if (_wanders)
        {
            _wanderTimer -= Time.deltaTime;

            if (_wanderTimer <= 0)
            {
                if (!_agent.hasPath && Random.value <= _wanderChance)
                {
                    Wander();
                }

                _wanderTimer = _wanderInterval;
            }
        }
    }

    public virtual void ClearScared()
    {
        transform.GetChild(0).localRotation = Quaternion.identity;
    }
}
