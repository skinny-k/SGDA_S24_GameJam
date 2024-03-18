using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaParty : MonoBehaviour
{
    [SerializeField] Bench _playerSeat;
    [SerializeField] string _rewardTitle = "the Honorable Baron of Squimblingham";

    public void StartTeaParty()
    {
        _playerSeat.Interact();
    }
    
    public void EndTeaParty()
    {
        _playerSeat.Interact();
        _playerSeat.enabled = false;

        FindObjectOfType<PlayerInfo>().UpdateQuestStatus(Quest.TeaParty, QuestStatus.Completed);
        MasterUI.Instance.UpdateTitle(", " + _rewardTitle);
    }
}
