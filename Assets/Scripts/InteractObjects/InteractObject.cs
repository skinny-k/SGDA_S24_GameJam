using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] protected Vector3 _tooltipOffset = Vector3.zero;
    
    protected PlayerBase _playerInRange = null;
    protected Tooltip _tip = null;
    
    public abstract void Interact();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBase>() != null)
        {
            _tip = MasterUI.Instance.InteractTooltip(transform.TransformPoint(_tooltipOffset));
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
