using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(PlayerActions))]
[RequireComponent(typeof(PlayerInfo))]
[RequireComponent(typeof(ThirdPersonController))]
public class PlayerBase : MonoBehaviour
{
    [SerializeField] float _scareTime = 15f;
    [SerializeField] string _playerTitle = "Death Knight";

    float _threatTimer = 0f;
    
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

    private void Start()
    {
        MasterUI.Instance.UpdateTitle(_playerTitle);
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

    void Update()
    {
        _threatTimer += Time.deltaTime;
        if (_threatTimer >= _scareTime && _info.ThreatLevel != PlayerThreatState.Killer)
        {
            _info.SetThreatState(PlayerThreatState.None);
        }
    }

    void Interact()
    {
        _actions.Interact();
    }

    void Attack()
    {
        if(_actions.Attack())
        {
            _info.SetThreatState(PlayerThreatState.Killer);
        }
    }
}
