using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathOrb : InteractObject
{
    //private StarterAssets.StarterAssetsInputs inputs;
    private GameObject _player;
    private CharacterController _controller;
    private BoxCollider _collider;
    private bool _title = false;
    private bool _sitting = false;
    private Inspection _inspection;
    private StarterAssets.StarterAssetsInputs inputs;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _controller = _player.GetComponent<CharacterController>();
        //_collider = gameObject.GetComponent<BoxCollider>();
        _inspection = gameObject.GetComponent<Inspection>();
        inputs = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
    }

    public override void Interact()
    {
        Pickup();
    }

    public void Pickup()
    {
        
        _inspection.TakeObject();
        if (!_sitting)
        {
            //Sitting animation

            _sitting = true;
            //_collider.enabled = false;
            inputs.cursorInputForLook = false;
            _controller.enabled = false;
           
        }
        else
        {
            EndSitting();
        }
    }

    private void EndSitting()
    {
        _sitting = false;
        //_collider.enabled = true;
        inputs.cursorInputForLook = false;
        _controller.enabled = true;
    }
}
