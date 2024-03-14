using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractObject
{
    [SerializeField] Vector3 _altRotation;

    Vector3 _originalRotation;

    public bool IsAtAltRotation { get; private set; } = false;

    void Start()
    {
        _originalRotation = transform.localRotation.eulerAngles;
    }
    
    public override void Interact()
    {
        IsAtAltRotation = !IsAtAltRotation;

        if (IsAtAltRotation)
        {
            transform.localRotation = Quaternion.Euler(_altRotation);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(_originalRotation);
        }

        _tip.SetWorldAnchor(transform.TransformPoint(_tooltipOffset));
    }
}
