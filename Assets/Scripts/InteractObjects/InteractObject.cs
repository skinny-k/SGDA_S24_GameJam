using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractObject : MonoBehaviour, IInteractable
{
    PlayerBase _playerInRange = null;
    Tooltip _tip = null;
    
    public abstract void Interact();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBase>() != null)
        {
            _tip = MasterUI.Instance.InteractTooltip(transform.position);
            _playerInRange = other.GetComponent<PlayerBase>();
            _playerInRange.Actions.OnInteract += Interact;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerBase>() != null)
        {
            Destroy(_tip.gameObject);
            _playerInRange.Actions.OnInteract -= Interact;
            _playerInRange = null;
        }
    }
}
