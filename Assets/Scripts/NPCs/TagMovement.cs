using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TagMovement : NPCMovement
{
    PlayerBase _playingTag = null;
    TagBody _tb;

    public GameObject DialogueNPC;
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
        Destroy(gameObject.GetComponent<Dialogue>());
        _playingTag = player;
        _tb.gameObject.SetActive(true);
    }

    public void EndTag()
    {
        DialogueNPC.transform.position = gameObject.transform.position;
        DialogueNPC.transform.rotation = gameObject.transform.rotation;
        DialogueNPC.SetActive(true);
        _agent.ResetPath();
        _playingTag = null;
        gameObject.SetActive(false);
    }
}
