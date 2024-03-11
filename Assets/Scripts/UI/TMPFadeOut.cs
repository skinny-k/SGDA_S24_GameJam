using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TMPFadeOut : MonoBehaviour
{
    [SerializeField] bool _fadeOnStart = true;
    [SerializeField] bool _destroyOnFade = true;
    [SerializeField] float _fadeTime = 1f;

    TMP_Text _text;
    float _fadeTimer = 0;
    bool _fading = false;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        if (_fadeOnStart)
        {
            StartFade();
        }
    }

    void Update()
    {
        if (_fading)
        {
            Color newColor = _text.color;
            newColor.a = Mathf.Lerp(1, 0, _fadeTimer / _fadeTime);
            if (_destroyOnFade && newColor.a == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                _text.color = newColor;
            }

            _fadeTimer = Mathf.Clamp(_fadeTimer + Time.deltaTime, 0, _fadeTime);
        }
    }

    public void StartFade()
    {
        _fading = true;
    }

    public void SetFadeTime(float newTime)
    {
        _fadeTimer = newTime;
    }

    public void SetMessage(string msg)
    {
        _text.text = msg;
    }
}
