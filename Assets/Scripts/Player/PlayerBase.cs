using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public enum PlayerThreatState
{
    Killer,
    Scary,
    None
}

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(PlayerActions))]
[RequireComponent(typeof(ThirdPersonController))]
public class PlayerBase : MonoBehaviour
{
    public PlayerThreatState ThreatLevel { get; private set; } = PlayerThreatState.Scary;
    
    // Components
    InputManager _im;
    PlayerActions _actions;
    ThirdPersonController _controller;

    public InputManager Input => _im;
    public PlayerActions Actions => _actions;
    public ThirdPersonController CharacterController => _controller;
    
    void Awake()
    {
        _im = GetComponent<InputManager>();
        _actions = GetComponent<PlayerActions>();
        _controller = GetComponent<ThirdPersonController>();
    }

    void OnEnable()
    {
        _im.OnInteract += Interact;
        _im.OnAttack += Attack;
    }

    void OnDisable()
    {
        _im.OnInteract -= Interact;
        _im.OnAttack -= Attack;
    }

    void Interact()
    {
        _actions.Interact();
    }

    void Attack()
    {
        _actions.Attack();

        /*
        if (hit npc)
        {
            ThreatLevel = PlayerThreatState.Killer;
        }
        */
    }
}
