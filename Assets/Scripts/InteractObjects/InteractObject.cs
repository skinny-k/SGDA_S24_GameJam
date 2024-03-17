using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] protected Vector3 _tooltipOffset = Vector3.zero;
    
    protected PlayerBase _playerInRange = null;
    protected Tooltip _tip = null;
    public UnityEvent OnInteract;

    public virtual void Interact()
    {
        OnInteract?.Invoke();
    }

    void OnDisable()
    {
        if (_tip != null)
        {
            Destroy(_tip.gameObject);
        }
    }
    
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
