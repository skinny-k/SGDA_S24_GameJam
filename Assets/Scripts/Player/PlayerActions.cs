using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Vector3 _attackBounds;
    
    PlayerBase _player;

    [HideInInspector]
    public bool Interacting;
    public event Action OnInteract;
    
    void Awake()
    {
        _player = GetComponent<PlayerBase>();
    }

    public void Interact()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + new Vector3(0, 0.5f, _attackBounds.z), _attackBounds, transform.forward, Quaternion.identity, 0);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<Dialogue>() != null)
            {
                hit.collider.gameObject.GetComponent<Dialogue>().DisplayDialogue();
            }
            else if (hit.collider.gameObject.GetComponent<InteractableObject>() != null && !Interacting)
            {
                hit.collider.gameObject.GetComponent<InteractableObject>().Interact(this);
            }
        }

        OnInteract?.Invoke();
    }

    public bool Attack()
    {
        bool result = false;
        
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + new Vector3(0, 0.5f, _attackBounds.z), _attackBounds, transform.forward, Quaternion.identity, 0);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage();
                if (hit.collider.gameObject.GetComponent<NPCBase>() != null)
                {
                    result = true;
                }
            }
        }

        return result;
    }
}
