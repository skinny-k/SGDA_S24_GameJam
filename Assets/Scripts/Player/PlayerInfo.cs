using System;
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

    public static event Action<int> OnQuestStatusUpdated;

    public QuestStatus UpdateQuestStatus(Quest quest, QuestStatus status)
    {
        _questStatuses[quest] = status;
        OnQuestStatusUpdated?.Invoke(GetNumCompletedQuests());

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

    public int GetNumCompletedQuests()
    {
        int result = 0;
        
        foreach (QuestStatus status in _questStatuses.Values)
        {
            if (status == QuestStatus.Completed)
            {
                result++;
            }
        }

        return result;
    }

    public PlayerThreatState SetThreatState(PlayerThreatState state)
    {
        ThreatLevel = state;
        return ThreatLevel;
    }
}
