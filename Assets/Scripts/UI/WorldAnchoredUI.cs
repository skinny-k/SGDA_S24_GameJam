using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAnchoredUI : MonoBehaviour
{
    [SerializeField] float _xNoise = 0f;
    [SerializeField] float _yNoise = 0f;
    
    [Header("Move Settings")]
    [SerializeField] float _gravity = 0f;
    [SerializeField] Vector3 _initialVelocity = Vector3.zero;
    [SerializeField] float _velocityXNoise = 0f;
    [SerializeField] float _velocityYNoise = 0f;
    
    RectTransform _rt;
    Vector3 _worldAnchor;
    Vector3 _currentVelocity;
    Vector3 _currentOffset = Vector3.zero;

    void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _currentOffset = new Vector3(Random.Range(-_xNoise, _xNoise), Random.Range(-_yNoise, _yNoise), 0);
        _currentVelocity = _initialVelocity + new Vector3(Random.Range(-_velocityXNoise, _velocityXNoise), Random.Range(-_velocityYNoise, _velocityYNoise), 0);
    }

    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(_worldAnchor);
        screenPoint.x -= Screen.width / 2;
        screenPoint.y -= Screen.height / 2;

        if (_currentVelocity != Vector3.zero)
        {
            _currentOffset += _currentVelocity * Time.deltaTime;
        }
        if (_gravity != 0)
        {
            _currentVelocity += Vector3.down * _gravity * 9.81f * Time.deltaTime;
        }

        _rt.anchoredPosition3D = screenPoint + _currentOffset;
    }

    public void SetWorldAnchor(Vector3 newAnchor)
    {
        _worldAnchor = newAnchor;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(_worldAnchor);
        screenPoint.x -= Screen.width / 2;
        screenPoint.y -= Screen.height / 2;

        _rt.anchoredPosition3D = screenPoint + _currentOffset;
    }
}
