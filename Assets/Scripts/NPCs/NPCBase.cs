using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCHealth))]
[RequireComponent(typeof(NPCMovement))]
public class NPCBase : MonoBehaviour
{
    [SerializeField] float _maxLookDistance = 10f;

    PlayerBase _playerInView = null;

    // Components
    NPCMovement _movement;
    NPCHealth _health;

    public NPCMovement Movement => _movement;
    public NPCHealth Health => _health;
    
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
            switch (_playerInView.ThreatLevel)
            {
                case PlayerThreatState.Killer:
                    _movement.RunAwayFrom(_playerInView.gameObject, _maxLookDistance);
                    break;
                case PlayerThreatState.Scary:
                    // _movement.RunAwayFrom(_playerInView.gameObject, _maxLookDistance);
                    break;
                case PlayerThreatState.None:
                    break;
            }
        }
    }
}
