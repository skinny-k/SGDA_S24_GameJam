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
[RequireComponent(typeof(PlayerInfo))]
[RequireComponent(typeof(ThirdPersonController))]
public class PlayerBase : MonoBehaviour
{
    public PlayerThreatState ThreatLevel { get; private set; } = PlayerThreatState.Scary;
    
    // Components
    InputManager _im;
    PlayerActions _actions;
    PlayerInfo _info;
    ThirdPersonController _controller;

    public InputManager Input => _im;
    public PlayerActions Actions => _actions;
    public PlayerInfo Info => _info;
    public ThirdPersonController CharacterController => _controller;
    
    void Awake()
    {
        _im = GetComponent<InputManager>();
        _actions = GetComponent<PlayerActions>();
        _info = GetComponent<PlayerInfo>();
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
        if(_actions.Attack())
        {
            ThreatLevel = PlayerThreatState.Killer;
        }
    }
}
