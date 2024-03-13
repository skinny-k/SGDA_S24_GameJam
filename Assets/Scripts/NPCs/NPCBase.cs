using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCHealth))]
[RequireComponent(typeof(NPCMovement))]
public class NPCBase : MonoBehaviour
{
    [Tooltip("The distance within which the NPC can perceive the player")]
    [SerializeField] float _maxLookDistance = 10f;
    [Tooltip("The distance within which the NPC should always face the player")]
    [SerializeField] float _focusDistance = 3f;

    PlayerBase _playerInView = null;
    bool _scared = true;

    // Components
    NPCMovement _movement;
    NPCHealth _health;

    public NPCMovement Movement => _movement;
    public NPCHealth Health => _health;
    public float MaxLookDistance => _maxLookDistance;
    public float FocusDistance => _focusDistance;
    
    void OnValidate()
    {
        GetComponent<SphereCollider>().radius = _maxLookDistance;
    }

    void Awake()
    {
        _movement = GetComponent<NPCMovement>();
        _health = GetComponent<NPCHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBase>() != null)
        {
            _playerInView = other.gameObject.GetComponent<PlayerBase>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBase>() != null)
        {
            _playerInView = null;
        }
    }

    protected virtual void Update()
    {
        if (_playerInView != null)
        {
            switch (_playerInView.Info.ThreatLevel)
            {
                case PlayerThreatState.Killer:
                    _movement.RespondToKiller(_playerInView);
                    break;
                case PlayerThreatState.Scary:
                    _movement.RespondToScary(_playerInView);
                    break;
                case PlayerThreatState.None:
                    if (_scared)
                    {
                        _scared = false;
                        _movement.ClearScared();
                    }
                    _movement.RespondToPassive(_playerInView);
                    break;
            }
        }
        else
        {
            _movement.RespondToPassive();
        }
    }
}
