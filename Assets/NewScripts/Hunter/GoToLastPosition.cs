using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLastPosition : Istate
{
    FSM_Manager _FSM;
    Transform _target;
    Transform _MyPosition;
    float _distance, _ViewAngle;


    public GoToLastPosition(FSM_Manager FSM, Transform _player, Transform myposition, float distance, float viewangle)
    {
        _FSM = FSM;
        _target = _player;
        _MyPosition = myposition;
        _distance = distance;
        _ViewAngle = viewangle;

    }


    public void onEnter()
    {
        Debug.Log("");
        
    }

    public void onUpdate()
    {
        if (Pathfinding.instance.InFov(_target, _MyPosition, _distance, _ViewAngle))
        {
            _FSM.ChangeState("Hunt");
        }
        else
        {

        }
    }

    public void OnExit()
    {
        
    }
}
