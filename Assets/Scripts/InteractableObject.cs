using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent OnInteract;
    [HideInInspector]
    public PlayerActions _playerActions;

    public void Interact(PlayerActions playerActions)
    {
        _playerActions = playerActions;
        OnInteract?.Invoke();
    }
}
