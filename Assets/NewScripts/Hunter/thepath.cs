using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thepath : Istate
{
     FSM_Manager _fsm;
     Transform House;
    float counter;

    public thepath(FSM_Manager fsm)
    {
        _fsm = fsm;
    }
    public void onEnter()
    {
        counter = 5;
        Debug.Log("Freno la velocida y me voy a mi casa ");
    }

    public void onUpdate()
    {
        float timer = Time.deltaTime;

        counter -= timer;

        if (counter <= 0)
            _fsm.ChangeState("Patrol");


        Debug.Log("descanso");
    }

    public void OnExit()
    {
        Debug.Log("salgo de la casa y me voy a patruyar");
    }
}
