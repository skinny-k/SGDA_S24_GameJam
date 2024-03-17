using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    [Tooltip("The text that will be displayed in the header of the dialogue box.")]
    public string _speakerName;

    [Tooltip("The text that will be displayed in the body of the dialogue box.")]
    [TextArea(3, 10)]
    public string[] _sentences;
    [Tooltip("First game object will be new position of camera, second will be target of camera.")]
    [SerializeField]
    public GameObject[] _objects;
    [HideInInspector]
    public int index = 0;

    public UnityEvent OnDialogueFinish;

    public DialogueController _dialogueController;

    public void DisplayDialogue()
    {
        _dialogueController.SetDialogue(this);
    }

    public GameObject GetTarget()
    {
        return _objects[index++];
    }
}
