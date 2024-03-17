using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Bench : InteractObject
{
    //private StarterAssets.StarterAssetsInputs inputs;
    [SerializeField] string _rewardTitle = "Enjoying the View";
    [SerializeField] Quest _quest;
    private GameObject _player;
    private CharacterController _controller;
    private BoxCollider _collider;
    private bool _title = false;
    private bool _sitting = false;
    public CinemachineVirtualCamera npcCamera;
    public GameObject location;
    public GameObject lookAt;
    public GameObject PlayerModel;
    private GameObject _playerModel;
    private Animator _animator;
    private int _sit;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _controller = _player.GetComponent<CharacterController>();
        _sit = Animator.StringToHash("is_Sitting");
        //_collider = gameObject.GetComponent<BoxCollider>();
    }

    public override void Interact()
    {
        StartSitting();
    }

    public void StartSitting()
    {
        if (!_sitting)
        {
            //Sitting animation

            _sitting = true;
            //_collider.enabled = false;
            _controller.enabled = false;

            //Set art inactive, I sure hope the art isnt ever not the second child
            _player.transform.GetChild(1).gameObject.SetActive(false);

            _playerModel = Instantiate(PlayerModel, gameObject.transform.position, location.transform.rotation);
            _playerModel.GetComponent<Animator>().SetBool(_sit, true);

            if (_tip != null)
            {
                _tip.gameObject.GetComponent<Image>().enabled = false;
                _tip.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            //_player.transform.position = gameObject.transform.position;
            //_player.transform.rotation = location.transform.rotation;

            if (npcCamera != null)
            {
                npcCamera.transform.position = location.transform.position;
                npcCamera.LookAt = lookAt.transform;
                npcCamera.Priority = 11;
            }
            
            StartCoroutine(SittingTimer());
        }
        else
        {
            EndSitting();
        }
    }

    IEnumerator SittingTimer()
    {
        yield return new WaitForSeconds(8);

        if (_quest != Quest.None && FindObjectOfType<PlayerInfo>().GetQuestStatus(_quest) == QuestStatus.NotStarted)
        {
            FindObjectOfType<PlayerInfo>().UpdateQuestStatus(_quest, QuestStatus.Completed);
            if (_rewardTitle != null)
            {
                MasterUI.Instance.UpdateTitle(_rewardTitle);
            }
        }
    }

    private void EndSitting()
    {
        StopAllCoroutines();
        Destroy(_playerModel);
        _player.transform.GetChild(1).gameObject.SetActive(true);
        _sitting = false;
        //_collider.enabled = true;
        _controller.enabled = true;

        if (npcCamera != null)
        {
            npcCamera.Priority = 0;
        }
    }
}
