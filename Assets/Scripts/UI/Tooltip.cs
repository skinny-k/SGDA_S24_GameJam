using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : WorldAnchoredUI
{
    [SerializeField] float _edgeBuffer = 50;
    
    protected override void SetPosition(Vector3 screenPosition)
    {
        screenPosition.x = Mathf.Clamp(screenPosition.x, _edgeBuffer - (Screen.width / 2), (Screen.width / 2) - _edgeBuffer);
        screenPosition.y = Mathf.Clamp(screenPosition.y, _edgeBuffer - (Screen.height / 2), (Screen.height / 2) - _edgeBuffer);

        _rt.anchoredPosition = screenPosition;
    }
}
