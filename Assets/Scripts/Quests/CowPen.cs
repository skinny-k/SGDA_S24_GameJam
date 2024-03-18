using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CowPen : MonoBehaviour
{
    [SerializeField] float _radius = 3f;
    [SerializeField] int _needsCows = 1;
    [SerializeField] string _rewardTitle = "He Who Wrangles Cows";

    public UnityEvent OnQuestComplete;

    List<CowMovement> _cows = new List<CowMovement>();

    public float Radius => _radius;
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.GetComponent<CowMovement>() != null)
        {
            CowMovement cow = other.GetComponent<CowMovement>();
            
            if (!_cows.Contains(cow))
            {
                cow.GoToPen(this);
                _cows.Add(cow);

                if (_cows.Count >= _needsCows)
                {
                    FindObjectOfType<PlayerInfo>().UpdateQuestStatus(Quest.WrangleCows, QuestStatus.Completed);
                    MasterUI.Instance.UpdateTitle(", " + _rewardTitle);
                    OnQuestComplete?.Invoke();
                }
            }
        }
    }
}
