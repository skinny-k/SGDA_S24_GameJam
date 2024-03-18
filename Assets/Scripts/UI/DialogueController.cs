using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [Header("Dialogue Display ")]
    [Tooltip("The gameobject that will be set active and inactive.")]
    [SerializeField] GameObject _dialogueBox;
    [Tooltip("The text field that will display the header text.")]
    [SerializeField] TextMeshProUGUI _headerText;
    [Tooltip("The text field that will display the body text.")]
    [SerializeField] TextMeshProUGUI _bodyText;
    [Space]
    [Tooltip("The text that will be displayed in the dialogue box.")]
    [SerializeField] Dialogue _dialogue;

    private Queue<string> sentences;
    private StarterAssets.StarterAssetsInputs inputs;
    private PlayerInput playerInput;

    public CinemachineVirtualCamera npcCamera;
    public UnityEvent OnDialogueEnd;

    private void Awake()
    {
        inputs = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
        playerInput = FindObjectOfType<PlayerInput>();
    }
    private void Start()
    {
        sentences = new Queue<string>();
        _dialogueBox.SetActive(false);
    }
    private void EnableCursor(bool enabled)
    {
        if (enabled)
        {
            Cursor.lockState = CursorLockMode.None;
            inputs.cursorInputForLook = false;
            playerInput.actions.FindAction("Move").Disable();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            inputs.cursorInputForLook = true;
            playerInput.actions.FindAction("Move").Enable();    
        }
    }
    private void ChangeText()
    {
        if (_headerText.text != _dialogue._speakerName)
            _headerText.text = _dialogue._speakerName;
    }

    public void SetDialogue(Dialogue dialogue)
    {
        _dialogue = dialogue;
        StartDialogue();
    }

    public void StartDialogue()
    {
        Debug.Log("Start of conversation...");

        //enable cursor
        EnableCursor(true);

        //unhide dialogue box and update text
        if (_dialogueBox.activeInHierarchy == false)
            _dialogueBox.SetActive(true);

        //update header
        ChangeText();

        //update sentences
        sentences.Clear();

        foreach (var sentence in _dialogue._sentences)
        {
            sentences.Enqueue(sentence);
        }

        //start queueing
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        if (sentence == "Cut")
        {
            CutCamera(); 
        }
        else if (sentence == "Uncut")
        {
            UncutCamera();
        }
        else
        {
            //display dialogue
            _bodyText.text = sentence;
        }
        
        

    }

    private void CutCamera()
    {
        npcCamera.transform.position = _dialogue.GetTarget().transform.position;
        npcCamera.LookAt = _dialogue.GetTarget().transform;
        npcCamera.Priority = 11;
        DisplayNextSentence();
    }

    private void UncutCamera()
    {
        npcCamera.Priority = 0;
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation...");

        //disable cursor
        EnableCursor(false);
        _dialogue._inDialogue = false;
        OnDialogueEnd?.Invoke();
        _dialogue.OnDialogueFinish?.Invoke();
        _dialogue.index = 0;
        //_dialogueBox.SetActive(false);
    }
}
