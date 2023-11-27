using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLastPosition : Istate
{
    FSM_Manager _FSM;
    Enemy _enemy;
    


    public GoToLastPosition(FSM_Manager FSM, Enemy enemy)
    {
        _FSM = FSM;
        _enemy = enemy;
    }


    public void onEnter()
    {
        Debug.Log($" {_enemy.gameObject.name} calculo a star ");

        _enemy.initial = ManagerNode.Instance.NearsNode(_enemy.transform);
        _enemy.Goal = ManagerNode.Instance.NearsNode(_enemy.target);
        _enemy.reciv = false;

        _enemy.Path = Pathfinding.instance.CalculateAStar(_enemy.initial, _enemy.Goal);
    }

    public void onUpdate()
    {
        if (Pathfinding.instance.InFov(_enemy.target, _enemy.transform, _enemy.distance, _enemy.ViewAngle))
        {
            _FSM.ChangeState("Hunt");
        }

        if (!Pathfinding.instance.InFov(_enemy.target, _enemy.transform, _enemy.distance, _enemy.ViewAngle))

        {
            if (_enemy.Path.Count > 0)
            {
                Debug.Log(" Pathfinding ");

                _enemy.AddForce(_enemy.Seek(_enemy.Path[0].transform.position));
                if (Vector3.Distance(_enemy.transform.gameObject.transform.position, _enemy.Path[0].transform.position) <= 0.3f) _enemy.Path.RemoveAt(0);
                _enemy.transform.position += _enemy.Velocity * Time.deltaTime;
                _enemy.transform.forward = _enemy.Velocity;
                
            }
            if (!_enemy.reciv)
            {
                _FSM.ChangeState("GoToPatrol");
            }
            else
            {
                _FSM.ChangeState("Patrol");
            }
        }




    }

    public void OnExit()
    {
        
    }
}
