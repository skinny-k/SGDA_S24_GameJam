using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inspection : MonoBehaviour
{

    [Header("General Settings")]
    [Tooltip("Player camera")]
    public GameObject camera;
    [Tooltip("Distance in front of camera to hold object")]
    [SerializeField]
    private float _cameraOffset = 1;
    [Tooltip("Rotation of object")]
    [SerializeField]
    private Vector3 _rotation;

    [Header("User Interface")]
    [Tooltip("Name of the item to be displayed on canvas")]
    [SerializeField]
    private string _itemName;
    [Tooltip("Description of the item to be displayed on canvas")]
    [SerializeField]
    private string _itemDescription;
    [Tooltip("Text object used to display name")]
    [SerializeField]
    private TextMeshProUGUI _itemNameTextView;
    [Tooltip("Text object used to display name")]
    [SerializeField]
    private TextMeshProUGUI _itemDescriptionTextView;


    private GameObject _player;
    private StarterAssets.StarterAssetsInputs inputs;

    private float _rotationSpeed = 0.02f;
    private float _moveSpeed = 10f;
    private Boolean _taking = false;
    private Boolean _taken = false;
    private Boolean _returning = false;
    private Vector3 _originalSizeBounds;
    private Vector3 _originalSize;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private Collider[] _colliders = new Collider[50];
    private BoxCollider _collider;
    public void Start()
    {
        if (_itemNameTextView != null && _itemDescriptionTextView != null)
        {
            _itemNameTextView.color = new Color(_itemNameTextView.color.r, _itemNameTextView.color.g, _itemNameTextView.color.b, 0);
            _itemDescriptionTextView.color = new Color(_itemDescriptionTextView.color.r, _itemDescriptionTextView.color.g, _itemDescriptionTextView.color.b, 0);
        }       
        _collider = gameObject.GetComponent<BoxCollider>();
        _player = GameObject.FindGameObjectWithTag("Player");
        inputs = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
    }
    
    public void TakeObject()
    {
        if (!_taken && !_taking && !_returning)
        {           
            StartCoroutine(TakingUpdate());                     
        }
        else if (_taken && !_taking && !_returning)
        {
            if (_itemNameTextView != null && _itemDescriptionTextView != null)
            {
                _itemNameTextView.color = new Color(_itemNameTextView.color.r, _itemNameTextView.color.g, _itemNameTextView.color.b, 0);
                _itemDescriptionTextView.color = new Color(_itemDescriptionTextView.color.r, _itemDescriptionTextView.color.g, _itemDescriptionTextView.color.b, 0);
            }
            //StartCoroutine(ReturningUpdate());
        }
        
    }

    IEnumerator TakingUpdate()
    {
        _taking = true;
        _originalSizeBounds = _collider.bounds.size;
        _originalSize = gameObject.transform.localScale;
        _originalPosition = gameObject.transform.position;
        _originalRotation = gameObject.transform.rotation;
        float timeCount = 0.0f;

        if (_itemNameTextView != null && _itemDescriptionTextView != null)
        {
            _itemNameTextView.text = _itemName;
            _itemDescriptionTextView.text = _itemDescription;
        }

        while (gameObject.transform.rotation != Quaternion.Euler(_rotation) || (gameObject.transform.position - (camera.transform.position + camera.transform.forward * _cameraOffset)).magnitude > 5)
        {
            timeCount = timeCount + Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_rotation), timeCount * _rotationSpeed);
            transform.position = Vector3.MoveTowards(transform.position, camera.transform.position + camera.transform.forward * _cameraOffset, Time.deltaTime * _moveSpeed);

            if (_collider.bounds.size.magnitude > 0.4f)
            {
                gameObject.transform.localScale = gameObject.transform.localScale * 0.998f;
            }
            else if (_collider.bounds.size.magnitude < 0.3f)
            {
                gameObject.transform.localScale = gameObject.transform.localScale * 1.002f;
            }
            if (_itemNameTextView != null && _itemDescriptionTextView != null)
            {
                _itemNameTextView.color = new Color(_itemNameTextView.color.r, _itemNameTextView.color.g, _itemNameTextView.color.b, _itemNameTextView.color.a + 0.01f);
                _itemDescriptionTextView.color = new Color(_itemDescriptionTextView.color.r, _itemDescriptionTextView.color.g, _itemDescriptionTextView.color.b, _itemDescriptionTextView.color.a + 0.01f);
            }

            yield return null;
        }
        _taking = false;
        StartCoroutine(TakenUpdate());
    }
    IEnumerator TakenUpdate()
    {
        _taken = true;
        while (!_returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, camera.transform.position + camera.transform.forward * _cameraOffset, Time.deltaTime * _moveSpeed);
            if (inputs.move.x>0)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(90f * Time.deltaTime, 0, 0);
            }
            if (inputs.move.y > 0)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0, 90f * Time.deltaTime, 0);
            }
            if (inputs.move.x < 0)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(-90f * Time.deltaTime, 0, 0);
            }
            if (inputs.move.y < 0)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0, -90f * Time.deltaTime, 0);
            }
            yield return null;
        }
        _taken = false;
    }
    IEnumerator ReturningUpdate()
    {
        _returning = true;
        float timeCount = 0.0f;
        while (gameObject.transform.rotation != _originalRotation || gameObject.transform.position != _originalPosition)
        {
            timeCount = timeCount + Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, _originalRotation, timeCount * _rotationSpeed);
            transform.position = Vector3.MoveTowards(transform.position, _originalPosition, Time.deltaTime * _moveSpeed);

            if (_collider.bounds.size.magnitude > _originalSizeBounds.magnitude + _originalSizeBounds.magnitude * .02)
            {
                gameObject.transform.localScale = gameObject.transform.localScale * 0.999f;
            }
            else if (_collider.bounds.size.magnitude < _originalSizeBounds.magnitude - _originalSizeBounds.magnitude * .02)
            {
                gameObject.transform.localScale = gameObject.transform.localScale * 1.001f;
            }
            if (_itemNameTextView != null && _itemDescriptionTextView != null)
            {
                _itemNameTextView.color = new Color(_itemNameTextView.color.r, _itemNameTextView.color.g, _itemNameTextView.color.b, _itemNameTextView.color.a - 0.01f);
                _itemDescriptionTextView.color = new Color(_itemDescriptionTextView.color.r, _itemDescriptionTextView.color.g, _itemDescriptionTextView.color.b, _itemDescriptionTextView.color.a - 0.01f);
            }
            yield return null;
        }
        gameObject.transform.localScale = _originalSize;
        if (_itemNameTextView != null && _itemDescriptionTextView != null)
        {
            _itemNameTextView.color = new Color(_itemNameTextView.color.r, _itemNameTextView.color.g, _itemNameTextView.color.b, 0);
            _itemDescriptionTextView.color = new Color(_itemDescriptionTextView.color.r, _itemDescriptionTextView.color.g, _itemDescriptionTextView.color.b, 0);
        }
        _returning = false;
        _taken = false;
    }

}
