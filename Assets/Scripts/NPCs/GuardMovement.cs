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
    [SerializeField] float _attackDistance = 2f;
    [Tooltip("The time in seconds the guard should wait between attacks.")]
    [SerializeField] float _attackInterval = 2f;
    [SerializeField] string _damageCalloutMessage = "0";
    [SerializeField] float _calloutOffsetY = 2f;

    [Header("Chase Settings")]
    [Tooltip("The interval at which the guard should adjust its path towards the player.")]
    [SerializeField] float _repathInterval = 1f;

    Vector3 _weaponDefaultRotation;
    Vector3 _weaponTargetPosition;
    Vector3 _weaponDefaultPosition;
    float _repathTimer = 0f;
    float _attackTimer = 0f;

    void OnValidate()
    {
        if (_weapon != null)
        {
            _weaponDefaultRotation = _weapon.localRotation.eulerAngles;
            _weaponDefaultPosition = _weapon.localPosition;
            _weaponTargetPosition = _weaponDefaultPosition;
        }
    }

    private void Update()
    {
        if (Animator != null)
        {
            if (_agent.velocity.magnitude > 0)
            {
                Animator.SetBool("is_Walking", true);
            }
            else
            {
                Animator.SetBool("is_Walking", false);
            }

        }
        _attackTimer -= Time.deltaTime;
        //_weapon.localPosition = Vector3.MoveTowards(_weapon.localPosition, _weaponTargetPosition, _attackDistance * 2 * Time.deltaTime);
    }

    public override void RespondToKiller(PlayerBase killer)
    {
        _repathTimer -= Time.deltaTime;

        if (_weapon != null)
        {
            _weapon.gameObject.SetActive(true);
        }

        if (Vector3.Distance(transform.position, killer.transform.position) > _attackDistance)
        {
            if (_repathTimer <= 0)
            {
                _dest = killer.transform.position;

                _agent.ResetPath();
                _agent.SetDestination(_dest);

                _repathTimer = _repathInterval;
            }
        }
        else
        {
            _agent.ResetPath();
        }

        Vector3 lookDir = killer.transform.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), 360f * Time.deltaTime);

        if (Vector3.Distance(transform.position, killer.transform.position) <= _attackDistance)
        {
            TryAttack(killer);
        }
    }

    public override void RespondToScary(PlayerBase scary)
    {
        base.RespondToScary(scary);
        
        if (Vector3.Distance(transform.position, scary.transform.position) <= _attackDistance)
        {
            TryAttack(scary);
        }

        if (_weapon != null)
        {
            _weapon.gameObject.SetActive(true);
        }
        //_weapon.localRotation = Quaternion.Euler(_weaponAttackRotation);
    }

    public override void RespondToPassive(PlayerBase passive)
    {
        base.RespondToPassive(passive);

        if (_weapon != null)
        {
            _weapon.gameObject.SetActive(false);
        }
        //_weapon.localRotation = Quaternion.Euler(_weaponDefaultRotation);
    }

    public void TryAttack(PlayerBase player)
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= _attackDistance)
        {
            if (_attackTimer <= 0)
            {
                if (Animator != null)
                {
                    Animator.ResetTrigger("Swing");
                    Animator.SetTrigger("Swing");
                }
                //StartCoroutine(AttackAnim());
                MasterUI.Instance.DamageCallout(player.transform.position + new Vector3(0, _calloutOffsetY, 0), _damageCalloutMessage);
                _attackTimer = _attackInterval;
            }
        }
    }

    IEnumerator AttackAnim()
    {
        _weaponTargetPosition = _weaponDefaultPosition + new Vector3(0, 0, _attackDistance);

        yield return new WaitForSeconds(0.25f);

        _weaponTargetPosition = _weaponDefaultPosition;
    }
}
