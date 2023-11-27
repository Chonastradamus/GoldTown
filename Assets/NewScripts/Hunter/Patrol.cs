using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Istate
{

    FSM_Manager fSM_;
    Enemy _enemy;


    public Patrol(FSM_Manager fsm,Enemy enemy)
    {
        fSM_ = fsm;
        _enemy = enemy;

        
    }

    public void onEnter()
    {

        _enemy.initial = ManagerNode.Instance.NearsNode(_enemy.transform);
        _enemy.Goal = ManagerNode.Instance.NearsNode(_enemy.target);

        _enemy.Path = Pathfinding.instance.CalculateAStar(_enemy.Goal, _enemy.initial);

        if (_enemy.reciv)
        {
            fSM_.ChangeState("serchposition");
        }
    }

    public void onUpdate()
    {
       

        if (Pathfinding.instance.InFov(_enemy.target, _enemy.transform, _enemy.distance, _enemy.ViewAngle))
        {
            fSM_.ChangeState("Hunt");
            GameManager.instance.Call(_enemy.target.position);
        }
        if (_enemy.reciv)
        {
            fSM_.ChangeState("serchposition");
        }
        else
        {
           _enemy.Waypoints();

        }


        _enemy.transform.position += _enemy.Velocity * Time.deltaTime;
        _enemy.transform.forward = _enemy.Velocity;
    }

    public void OnExit()
    {
        //Debug.Log(" I see a enemy ");
    }
    



}
