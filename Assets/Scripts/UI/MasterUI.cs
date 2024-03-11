using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterUI : MonoBehaviour
{
    public static MasterUI Instance;

    [SerializeField] TMPFadeOut _damageCallout;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public TMPFadeOut DamageCallout(Vector3 position, string msg)
    {
        TMPFadeOut callout = Instantiate(_damageCallout, transform);
        callout.GetComponent<TMP_Text>().text = msg;
        callout.GetComponent<WorldAnchoredUI>().SetWorldAnchor(position);

        return callout;
    }
}
