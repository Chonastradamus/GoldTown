using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPAtrol : Istate
{
    FSM_Manager _fSM;
    Enemy _enemy;


    public GoToPAtrol (FSM_Manager fsm, Enemy enemy)
    {
        _fSM = fsm;
        _enemy = enemy;
    }


    public void onEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void onUpdate()
    {
        throw new System.NotImplementedException();
    }
}
