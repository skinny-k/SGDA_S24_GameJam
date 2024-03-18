using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : InteractObject
{
    [SerializeField] string _rewardTitle = "Dog's Best Friend";
    
    public override void Interact()
    {
        if (FindObjectOfType<PlayerInfo>().GetQuestStatus(Quest.PetDog) != QuestStatus.Completed)
        {
            base.Interact();

            GameManager.Player.GetComponent<PlayerInfo>().UpdateQuestStatus(Quest.PetDog, QuestStatus.Completed);
            MasterUI.Instance.UpdateTitle(", " + _rewardTitle);
        }
    }
}
