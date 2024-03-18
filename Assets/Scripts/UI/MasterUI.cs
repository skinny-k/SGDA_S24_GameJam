using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterUI : MonoBehaviour
{
    [SerializeField] Button _pauseButton;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] TextMeshProUGUI _playerTitle;

    [Header("Prefabs")]
    [SerializeField] WorldAnchoredUIWithOffset _damageCallout;
    [SerializeField] Tooltip _interactTooltip;

    string _baseName;

    Image _fadePanel;
    bool _fade = false;
    float _fadeTime = 1.5f;
    float _currentFade = 0f;
    
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

        _fadePanel = transform.Find("FadePanel").GetComponent<Image>();
    }

    void Update()
    {
        if (_fade)
        {
            _currentFade += Time.deltaTime;
            Color newColor = _fadePanel.color;
            newColor.a = Mathf.Lerp(0, 1, _currentFade / _fadeTime);
            _fadePanel.color = newColor;
        }
    }

    public WorldAnchoredUIWithOffset DamageCallout(Vector3 position, string msg)
    {
        WorldAnchoredUIWithOffset callout = Instantiate(_damageCallout, transform);
        callout.GetComponent<TMP_Text>().text = msg;
        callout.SetWorldAnchor(position);

        return callout;
    }

    public WorldAnchoredUIWithOffset DamageCallout(Vector3 position, string msg, Color color)
    {
        WorldAnchoredUIWithOffset callout = Instantiate(_damageCallout, transform);
        callout.GetComponent<TMP_Text>().text = msg;
        callout.GetComponent<TMP_Text>().color = color;
        callout.SetWorldAnchor(position);

        return callout;
    }

    public Tooltip InteractTooltip(Vector3 position)
    {
        Tooltip tip = Instantiate(_interactTooltip, transform);
        tip.SetWorldAnchor(position);

        return tip;
    }

    public void StartFadeToBlack(float time = 1.5f)
    {
        _fadePanel.gameObject.SetActive(true);
        _fadePanel.color = new Color(0, 0, 0, 0);
        _fadeTime = time;
        _fade = true;
    }

    public void StartFadeToWhite(float time = 1.5f)
    {
        _fadePanel.gameObject.SetActive(true);
        _fadePanel.color = new Color(1, 1, 1, 0);
        _fadeTime = time;
        _fade = true;
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

    public void UpdateTitle(string titleAddition)
    {
        _playerTitle.text += titleAddition;
    }

    public void ReplaceTitle(string oldTitle, string newTitle)
    {
        _playerTitle.text = Regex.Replace(_playerTitle.text, ", " + oldTitle, ", " + newTitle);
    }

    public void SetBaseName(string newName)
    {
        if (_baseName == null)
        {
            _playerTitle.text = newName;
        }
        else
        {
            _playerTitle.text = Regex.Replace(_playerTitle.text, _baseName, newName);
        }

        _baseName = newName;
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
