using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// As of Unity 2021.3.21f, polling for input is the only way to receive input from the new Input System. This class
// instead wraps input from the Player Input component into an event-based system
[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    PlayerInput _input;

    public event Action OnInteract;
    public event Action OnAttack;
    
    void OnValidate()
    {
        _input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // polls each input action for state changes and fires a corresponding event if a state change has occurred

        if (_input.actions["Interact"].triggered)
        {
            OnInteract?.Invoke();
        }

        if (_input.actions["Attack"].triggered)
        {
            OnAttack?.Invoke();
        }
    }
}