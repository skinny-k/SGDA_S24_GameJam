using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagBody : InteractObject
{
    TagMovement _npcPlayer = null;
    TagGame _game = null;

    bool _inGame = false;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (_tip != null)
        {
            _tip.FollowTransform = transform;
            _tip.FollowOffset = _tooltipOffset;
        }
    }

    public override void Interact()
    {
        _game.EndTagGame();
        OnInteract?.Invoke();
        gameObject.SetActive(false);
    }

    public void SetNPCPlayer(TagMovement npcPlayer)
    {
        _npcPlayer = npcPlayer;
    }

    public void SetGame(TagGame game)
    {
        _game = game;
    }
}
