using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerThreatState
{
    Killer,
    Scary,
    None
}

public class PlayerInfo : MonoBehaviour
{
    Dictionary<Quest, QuestStatus> _questStatuses = new Dictionary<Quest, QuestStatus>();

    public PlayerThreatState ThreatLevel { get; private set; } = PlayerThreatState.Scary;

    public QuestStatus UpdateQuestStatus(Quest quest, QuestStatus status)
    {
        _questStatuses[quest] = status;
        return _questStatuses[quest];
    }


    public QuestStatus GetQuestStatus(Quest quest)
    {
        QuestStatus result = QuestStatus.NotStarted;

        if (!_questStatuses.TryGetValue(quest, out result))
        {
            UpdateQuestStatus(quest, QuestStatus.NotStarted);
            return QuestStatus.NotStarted;
        }
        else
        {
            return result;
        }
    }

    public PlayerThreatState SetThreatState(PlayerThreatState state)
    {
        ThreatLevel = state;
        return ThreatLevel;
    }

    //Added by Skyler to jank a fix in Unity Events
    public void CompleteThisQuest(string quest)
    {
        if (System.Enum.TryParse<Quest>(quest, out Quest yourEnum))
        {
            _questStatuses[yourEnum] = QuestStatus.Completed;
        }

    }
}
