using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerMovement : NPCMovement
{
    private void Update()
    {
        if (Animator != null)
        {
            if (_agent.velocity.magnitude > 0)
            {
                Animator.SetBool("is_Walking", true);
            }
            else
            {
                Animator.SetBool("is_Walking", false);
            }

        }
    }

    public override void RespondToKiller(PlayerBase killer)
    {
        RespondToPassive(killer);
    }

    public override void RespondToScary(PlayerBase scary)
    {
        RespondToPassive(scary);
    }

    public override void RespondToPassive(PlayerBase passive)
    {
        base.RespondToPassive(passive);
    }
}
