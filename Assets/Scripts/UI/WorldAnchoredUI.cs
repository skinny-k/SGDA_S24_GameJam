using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAnchoredUI : MonoBehaviour
{
    Vector3 _worldAnchor;
    RectTransform _rt;

    void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        // _rt.anchoredPosition3D = Camera.main.WorldToScreenPoint(_worldAnchor);
        // Debug.Log(Camera.main.WorldToScreenPoint(_worldAnchor));
    }

    public void SetWorldAnchor(Vector3 newAnchor)
    {
        _worldAnchor = newAnchor;
        _rt.anchoredPosition3D = Camera.main.WorldToScreenPoint(_worldAnchor);
        Debug.Log(Camera.main.WorldToScreenPoint(_worldAnchor));
    }
}
