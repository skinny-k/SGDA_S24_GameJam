using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Crates : InteractObject
{
    private GameObject _player;
    private CharacterController _controller;
    private BoxCollider _collider;
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
        OnInteract?.Invoke();
        Pickup();
    }

    public void Pickup()
    {
        
        _tip.gameObject.GetComponent<Image>().enabled = false;
        _tip.gameObject.transform.GetChild(0).gameObject.SetActive(false);
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
        _playerInRange.Actions.OnInteract -= Interact;
        _sitting = false;
        //_collider.enabled = true;
        inputs.cursorInputForLook = true;
        _controller.enabled = true;       
        Destroy(gameObject, 0.01f);
    }
}
