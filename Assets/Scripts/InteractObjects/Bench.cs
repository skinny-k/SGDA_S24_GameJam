using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bench : InteractObject
{
    //private StarterAssets.StarterAssetsInputs inputs;
    private GameObject _player;
    private CharacterController _controller;
    private BoxCollider _collider;
    private bool _title = false;
    private bool _sitting = false;
    public CinemachineVirtualCamera npcCamera;
    public GameObject location;
    public GameObject lookAt;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _controller = _player.GetComponent<CharacterController>();
        _collider = gameObject.GetComponent<BoxCollider>();
    }

    public override void Interact()
    {
        StartSitting();
    }

    public void StartSitting()
    {
        if (!_sitting)
        {
            //Sitting animation

            _sitting = true;
            _collider.enabled = false;
            _controller.enabled = false;
            _player.transform.position = gameObject.transform.position;
            _player.transform.rotation = gameObject.transform.rotation;
            npcCamera.transform.position = location.transform.position;
            npcCamera.LookAt = lookAt.transform;
            npcCamera.Priority = 11;
        }
        else
        {
            EndSitting();
        }
    }

    private void EndSitting()
    {
        _sitting = false;
        _collider.enabled = true;
        _controller.enabled = true;
        npcCamera.Priority = 0;
    }
}
