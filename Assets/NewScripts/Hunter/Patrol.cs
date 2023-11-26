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

        _enemy.Path = Pathfinding.instance.CalculateAStar(_enemy.initial, _enemy.Goal);
    }

    public void onUpdate()
    {
       /* if (!Pathfinding.instance.InFov(_target, _MyPosition, _distance, _ViewAngle) && _enemy.Path.Count <= 0)
        {
            if (_enemy.Path.Count > 0)
            {
                Debug.Log(" Pathfinding ");

                AddForce(Seek(_enemy.Path[0].transform.position));

                if (Vector3.Distance(_MyPosition.gameObject.transform.position, _enemy.Path[0].transform.position) <= 0.3f) _enemy.Path.RemoveAt(0);

                _MyPosition.transform.position += _Velocity * Time.deltaTime;
                _MyPosition.transform.forward = _Velocity;

            }
            else
            {
                Waypoints();

            }
        }*/

        if (Pathfinding.instance.InFov(_enemy.target, _enemy.transform, _enemy.distance, _enemy.ViewAngle))
        {
            fSM_.ChangeState("Hunt");
            GameManager.instance.Call(_enemy.target.position);
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
