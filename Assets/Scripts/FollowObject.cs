using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] GameObject _follow;
    [SerializeField] bool _useStartOffset = true;
    [SerializeField] Vector3 _followOffset;

    private Vector3 offset;

    void Start()
    {
        if (_useStartOffset)
        {
            offset = transform.position - _follow.transform.position;
        }
        else
        {
            offset = _followOffset;
        }
    }

    void Update()
    {
        // transform.position = Vector3.Slerp(transform.position, _follow.transform.position, 0.9f);
        transform.position = _follow.transform.position + offset;
    }
}
