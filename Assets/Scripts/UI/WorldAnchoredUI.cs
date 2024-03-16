using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAnchoredUI : MonoBehaviour
{
    public Transform FollowTransform = null;
    public Vector3 FollowOffset = Vector3.zero;
    
    protected RectTransform _rt;
    protected Vector3 _worldAnchor;

    protected virtual void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    protected virtual void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(_worldAnchor);
        
        if (FollowTransform != null)
        {
            screenPoint = Camera.main.WorldToScreenPoint(FollowTransform.position + FollowOffset);
        }
        
        screenPoint.x -= Screen.width / 2;
        screenPoint.y -= Screen.height / 2;

        SetPosition(screenPoint);
    }

    public virtual void SetWorldAnchor(Vector3 newAnchor)
    {
        _worldAnchor = newAnchor;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(_worldAnchor);
        screenPoint.x -= Screen.width / 2;
        screenPoint.y -= Screen.height / 2;

        SetPosition(screenPoint);
    }

    protected virtual void SetPosition(Vector3 screenPoint)
    {
        _rt.anchoredPosition3D = screenPoint;
    }
}
