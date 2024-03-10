using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerActions : MonoBehaviour
{
    InputManager _im;
    
    void OnValidate()
    {
        _im = GetComponent<InputManager>();
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
        Debug.Log("Interact Pressed");
    }

    void Attack()
    {
        Debug.Log("Attack Pressed");
    }
}
