using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerBase _player;
    
    void Awake()
    {
        _player = GetComponent<PlayerBase>();
    }

    public void Interact()
    {
        Debug.Log("Interact Pressed");
    }

    public bool Attack()
    {
        bool result = false;
        
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + Vector3.up, new Vector3(1f, 0.25f, 0.5f), transform.forward);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage();
                if (hit.collider.gameObject.GetComponent<NPCBase>() != null)
                {
                    result = true;
                }
            }
        }

        return result;
    }
}
