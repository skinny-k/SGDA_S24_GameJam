using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    Dictionary<Quest, QuestStatus> _questStatuses = new Dictionary<Quest, QuestStatus>();

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
}
