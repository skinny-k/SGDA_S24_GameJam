using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : NPCMovement
{
    [Header("Weapon Settings")]
    [SerializeField] Transform _weapon;
    [Tooltip("The rotation the guard holds its weapon at while the player is hostile.")]
    [SerializeField] Vector3 _weaponAttackRotation;

    [Header("Attack Settings")]
    [Tooltip("The distance at which the guard can attack the player.")]
    [SerializeField] float _attackDistance = 4f;
    [Tooltip("The time in seconds the guard should wait between attacks.")]
    [SerializeField] float _attackInterval = 2f;
    [SerializeField] string _damageCalloutMessage = "0";
    [SerializeField] float _calloutOffsetY = 2f;

    [Header("Chase Settings")]
    [Tooltip("The interval at which the guard should adjust its path towards the player.")]
    [SerializeField] float _repathInterval = 1f;

    Vector3 _weaponDefaultRotation;
    float _repathTimer = 0f;
    float _attackTimer = 0f;

    void OnValidate()
    {
        if (_weapon != null)
        {
            _weaponDefaultRotation = _weapon.localRotation.eulerAngles;
        }
    }

    void Update()
    {
        _attackTimer -= Time.deltaTime;
    }

    public override void RespondToKiller(PlayerBase killer)
    {
        _repathTimer -= Time.deltaTime;
        
        if (_repathTimer <= 0)
        {
            _dest = killer.transform.position;

            _agent.ResetPath();
            _agent.SetDestination(_dest);
        }

        if (Vector3.Distance(transform.position, killer.transform.position) <= _attackDistance)
        {
            TryAttack(killer);
        }
    }

    public override void RespondToScary(PlayerBase scary)
    {
        if (Vector3.Distance(transform.position, scary.transform.position) <= _attackDistance)
        {
            TryAttack(scary);
        }
        else
        {
            base.RespondToScary(scary);
        }

        _weapon.localRotation = Quaternion.Euler(_weaponAttackRotation);
    }

    public override void RespondToPassive(PlayerBase passive)
    {
        base.RespondToPassive(passive);
        
        _weapon.localRotation = Quaternion.Euler(_weaponDefaultRotation);
    }

    public void TryAttack(PlayerBase player)
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= _attackDistance)
        {
            if (_attackTimer <= 0)
            {
                _attackTimer = _attackInterval;
            }
        }
    }
}
