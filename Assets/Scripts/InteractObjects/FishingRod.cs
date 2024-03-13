using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRod : InteractObject
{
    [SerializeField] string _catchCalloutMessage = "Got a Bite!";
    [SerializeField] string _failCalloutMessage = "Lost it...";
    [SerializeField] float _calloutOffsetY = 2f;

    //private StarterAssets.StarterAssetsInputs inputs;
    private PlayerInput playerInput;
    private bool _catchable = false;
    private bool _fishing = false;
    private bool _title = false;
    private IEnumerator wait;
    private void Awake()
    {
        //inputs = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public override void Interact()
    {
        StartFishing();
    }

    public void StartFishing()
    {
        if (_catchable)
        {
            CatchFish();
        }
        else if(!_fishing)
        {
            //Replace with animation
            MasterUI.Instance.DamageCallout(transform.position + new Vector3(0, _calloutOffsetY, 0), "Fishing...");

            _fishing = true;
            //Cursor.lockState = CursorLockMode.None;
            //inputs.cursorInputForLook = false;
            playerInput.actions.FindAction("Move").Disable();
            wait = WaitForFish();
            StartCoroutine(wait);         
        }
        else
        {
            EndFishing();
        }
    }
    public IEnumerator WaitForFish()
    {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        _catchable = true;
        MasterUI.Instance.DamageCallout(transform.position + new Vector3(0, _calloutOffsetY, 0), _catchCalloutMessage);
        yield return new WaitForSeconds(1);
        MasterUI.Instance.DamageCallout(transform.position + new Vector3(0, _calloutOffsetY, 0), _failCalloutMessage);
        EndFishing();
    }
    private void CatchFish()
    {
        MasterUI.Instance.DamageCallout(transform.position + new Vector3(0, _calloutOffsetY, 0), "Caught a Fish!");
        if (!_title)
        {
            MasterUI.Instance.UpdateTitle(", Fisherman");
            _title = true;
        }
        EndFishing();
    }

    private void EndFishing()
    {
        _catchable = false;
        _fishing = false;
        StopCoroutine(wait);
        //Cursor.lockState = CursorLockMode.Locked;
        //inputs.cursorInputForLook = true;
        playerInput.actions.FindAction("Move").Enable();
        //gameObject.GetComponent<InteractableObject>()._playerActions.Interacting = false;
    }
}