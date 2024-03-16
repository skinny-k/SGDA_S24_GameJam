using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagMovement : NPCMovement
{
    PlayerBase _playingTag = null;
    TagBody _tb;

    public TagBody TB => _tb;

    public void SetTagGame(TagGame game)
    {
        _tb = transform.Find("Tag Body").GetComponent<TagBody>();
        _tb.SetNPCPlayer(this);
        _tb.SetGame(game);
    }

    public override void RespondToScary(PlayerBase scary)
    {
        if (_playingTag != null)
        {
            base.RespondToKiller(_playingTag);
        }
        else
        {
            base.RespondToScary(scary);
        }
    }

    public override void RespondToPassive(PlayerBase passive)
    {
        if (_playingTag != null)
        {
            base.RespondToKiller(_playingTag);
        }
        else
        {
            base.RespondToPassive(passive);
        }
    }

    public void StartTag(PlayerBase player)
    {
        _playingTag = player;
        _tb.gameObject.SetActive(true);
    }

    public void EndTag()
    {
        _agent.ResetPath();
        _playingTag = null;
    }
}
