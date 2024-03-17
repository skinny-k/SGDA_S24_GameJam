using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenameQuest : MonoBehaviour
{
    public string NameGiven = "Dave";
    
    public void Complete()
    {
        MasterUI.Instance.SetBaseName(NameGiven);
        FindObjectOfType<PlayerInfo>().UpdateQuestStatus(Quest.GetAName, QuestStatus.Completed);
    }
}
