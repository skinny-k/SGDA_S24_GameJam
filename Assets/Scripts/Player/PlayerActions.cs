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

    public void Attack()
    {
        Debug.Log("Attack Pressed");
    }
}
