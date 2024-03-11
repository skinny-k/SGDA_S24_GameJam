using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAnchoredUIWithOffset : WorldAnchoredUI
{
    [SerializeField] float _xNoise = 0f;
    [SerializeField] float _yNoise = 0f;
    
    [Header("Move Settings")]
    [SerializeField] float _gravity = 0f;
    [SerializeField] Vector3 _initialVelocity = Vector3.zero;
    [SerializeField] float _velocityXNoise = 0f;
    [SerializeField] float _velocityYNoise = 0f;
    
    Vector3 _currentVelocity;
    Vector3 _currentOffset = Vector3.zero;
    
    protected override void Awake()
    {
        base.Awake();

        _currentOffset = new Vector3(Random.Range(-_xNoise, _xNoise), Random.Range(-_yNoise, _yNoise), 0);
        _currentVelocity = _initialVelocity + new Vector3(Random.Range(-_velocityXNoise, _velocityXNoise), Random.Range(-_velocityYNoise, _velocityYNoise), 0);
    }

    protected override void Update()
    {
        if (_currentVelocity != Vector3.zero)
        {
            _currentOffset += _currentVelocity * Time.deltaTime;
        }
        if (_gravity != 0)
        {
            _currentVelocity += Vector3.down * _gravity * 9.81f * Time.deltaTime;
        }

        base.Update();
    }

    protected override void SetPosition(Vector3 screenPoint)
    {
        _rt.anchoredPosition3D = screenPoint + _currentOffset;
    }
}
