using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMovement : NPCMovement
{
    [SerializeField] bool _inPen = true;

    CowPen _returnToPen = null;

    void Update()
    {
        if (_returnToPen != null && _inPen && !_agent.hasPath)
        {
            _wanders = true;
            _wanderCenter = _returnToPen.transform.position;
            _wanderDistance = _returnToPen.Radius;

            _returnToPen = null;
        }
    }
    
    public override void RespondToKiller(PlayerBase killer)
    {
        RunAwayFrom(killer);
    }

    public override void RespondToScary(PlayerBase scary)
    {
        if (!_inPen)
        {
            RunAwayFrom(scary);
        }
        else if (!_returnToPen)
        {
            base.RespondToScary(scary);
        }
    }

    public override void RespondToPassive(PlayerBase passive)
    {
        if (!_inPen && passive != null)
        {
            RunAwayFrom(passive);
        }
        else if (!_returnToPen)
        {
            base.RespondToPassive(passive);
        }
    }

    void RunAwayFrom(PlayerBase player)
    {
        base.RespondToKiller(player);
    }

    public void GoToPen(CowPen pen)
    {
        if (!_inPen)
        {
            _inPen = true;
            _returnToPen = pen;
            _agent.ResetPath();
            _agent.SetDestination(pen.transform.position);
        }
    }
}
