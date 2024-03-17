using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Necromancer : MonoBehaviour
{
    [SerializeField] int _questsToKill = 5;
    [SerializeField] NPCBase _killableNPC;
    [SerializeField] string _rewardTitle = "the Savior";

    public UnityEvent OnKill;
    
    void OnEnable()
    {
        PlayerInfo.OnQuestStatusUpdated += TryEnableKill;
    }

    void OnDisable()
    {
        PlayerInfo.OnQuestStatusUpdated -= TryEnableKill;
    }

    void TryEnableKill(int numQuestsCompleted)
    {
        if (numQuestsCompleted >= _questsToKill && _killableNPC != null)
        {
            Debug.Log("Necromancer is now killable");
            _killableNPC.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (_killableNPC == null)
        {
            MasterUI.Instance.UpdateTitle(", " + _rewardTitle);
            FindObjectOfType<PlayerInfo>().UpdateQuestStatus(Quest.SaveTheWorld, QuestStatus.Completed);
            OnKill?.Invoke();
        }
    }
}
