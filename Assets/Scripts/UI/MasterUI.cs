using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterUI : MonoBehaviour
{
    [SerializeField] Button _pauseButton;
    [SerializeField] GameObject _pausePanel;
    
    [Header("Prefabs")]
    [SerializeField] WorldAnchoredUIWithOffset _damageCallout;
    [SerializeField] Tooltip _interactTooltip;
    
    public static MasterUI Instance;

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

    public void SetPause(bool state)
    {
        _pausePanel.gameObject.SetActive(state);
        _pauseButton.gameObject.SetActive(!state);
        
        GameManager.SetPause(state);
    }

    public void TogglePause()
    {
        SetPause(!_pausePanel.activeSelf);
    }

    public void LoadScene(string sceneName)
    {
        GameManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        GameManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        GameManager.Quit();
    }
}
