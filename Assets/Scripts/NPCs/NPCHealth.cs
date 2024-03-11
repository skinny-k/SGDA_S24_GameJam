using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour, IDamageable
{
    [SerializeField] string _damageCalloutMessage = "-99999";
    
    public void TakeDamage()
    {
        // do whatever needs to happen on death
        Destroy(gameObject);

        MasterUI.Instance.DamageCallout(transform.position, _damageCalloutMessage);
    }
}
