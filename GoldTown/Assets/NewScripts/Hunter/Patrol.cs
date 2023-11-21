using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Istate
{
    Transform[] _Waypoints;
    FSM_Manager fSM_;
    float counter;

    public Patrol(FSM_Manager fsm)
    {
        fSM_ = fsm;
    }
    public void onEnter()
    {
        Debug.Log("Freno la velocida y me voy a mi casa ");
    }

    public void onUpdate()
    {
        Debug.Log("descanso");
    }

    public void OnExit()
    {
        Debug.Log("salgo de la casa y me voy a patruyar");
    }
}
