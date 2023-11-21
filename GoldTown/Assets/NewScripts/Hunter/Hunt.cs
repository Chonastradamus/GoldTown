using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunt :  Istate
{
    FSM_Manager fSM_;


    public Hunt(FSM_Manager fsm)
    {
        fSM_ = fsm;
    }
    public void onEnter()
    {
        Debug.Log("busca al enemigo cercano en el area");
    }

    public void onUpdate()
    {
        Debug.Log("va hacia el enemigo una ves que lo encuetre");
    }

    public void OnExit()
    {
        Debug.Log("dejo de cazar y me voy a patruyar");
    }
}
