using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    [HideInInspector]
    public GameObject _player;
    private CharacterController _controller;
    private BoxCollider _collider;
    public bool _sitting = false;
    [HideInInspector]
    private Inspection _inspection;
    private StarterAssets.StarterAssetsInputs inputs;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _controller = _player.GetComponent<CharacterController>();
        //_collider = gameObject.GetComponent<BoxCollider>();
        _inspection = gameObject.GetComponent<Inspection>();
        inputs = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
    }

    public void Interact()
    {
        Pickup();
    }

    public void Pickup()
    {
             
        
        if (!_sitting)
        {
            //Sitting animation
            _inspection.TakeObject();
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
        _player.GetComponent<PlayerBase>().Actions.OnInteract -= Pickup;
        _sitting = false;
        //_collider.enabled = true;
        inputs.cursorInputForLook = true;
        _controller.enabled = true;
        _inspection.Return();
        transform.GetChild(0).gameObject.SetActive(false);
        //Destroy(gameObject, 0.01f);
    }
}
