using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterUI : MonoBehaviour
{
    public static MasterUI Instance;

    [SerializeField] WorldAnchoredUIWithOffset _damageCallout;
    [SerializeField] Tooltip _interactTooltip;
    
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

    public WorldAnchoredUIWithOffset DamageCallout(Vector3 position, string msg)
    {
        WorldAnchoredUIWithOffset callout = Instantiate(_damageCallout, transform);
        callout.GetComponent<TMP_Text>().text = msg;
        callout.SetWorldAnchor(position);

        return callout;
    }

    public Tooltip InteractTooltip(Vector3 position)
    {
        Tooltip tip = Instantiate(_interactTooltip, transform);
        tip.SetWorldAnchor(position);

        return tip;
    }
}
