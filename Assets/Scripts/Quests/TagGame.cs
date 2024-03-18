using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TagGame : MonoBehaviour
{
    [SerializeField] List<TagMovement> _npcPlayers = new List<TagMovement>();
    [SerializeField] string _inProgressTitle = "It";
    [SerializeField] string _rewardTitle = "Not It";

    public void Start()
    {
        foreach (TagMovement npcPlayer in _npcPlayers)
        {
            npcPlayer.SetTagGame(this);
        }
    }

    public void StartTagGame()
    {
        foreach (TagMovement npcPlayer in _npcPlayers)
        {
            npcPlayer.StartTag(GameManager.Player);
            npcPlayer.TB.SetGame(this);
        }

        PlayerInfo quests = GameManager.Player.GetComponent<PlayerInfo>();
        if (quests.GetQuestStatus(Quest.PlayTag) == QuestStatus.NotStarted)
        {
            quests.UpdateQuestStatus(Quest.PlayTag, QuestStatus.InProgress);
            MasterUI.Instance.UpdateTitle(", " + _inProgressTitle);
        }
    }

    public void EndTagGame()
    {
        foreach (TagMovement npcPlayer in _npcPlayers)
        {
            npcPlayer.EndTag();
        }

        PlayerInfo quests = GameManager.Player.GetComponent<PlayerInfo>();
        if (quests.GetQuestStatus(Quest.PlayTag) == QuestStatus.InProgress)
        {
            quests.UpdateQuestStatus(Quest.PlayTag, QuestStatus.Completed);
            MasterUI.Instance.ReplaceTitle(_inProgressTitle, _rewardTitle);
        }
    }
}
