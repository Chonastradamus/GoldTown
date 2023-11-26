using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLastPosition : Istate
{
    FSM_Manager _FSM;
    Transform _target;
    Transform _MyPosition;
    float _distance, _ViewAngle;
    Vector3 _Velocity;
    float _maxVelocity;
    float _maxForce;

    Enemy _enemy;

    Node _InitialNode;
    Node _FinalNode;
    List<Node> _Path;
    


    public GoToLastPosition(FSM_Manager FSM, Transform _player, Transform myposition, float distance, float viewangle, Node initialnode, Node finalNode, List<Node> Path, Vector3 velocity, float maxvelocity, float maxforce, Enemy enemy)
    {
        _FSM = FSM;
        _target = _player;
        _MyPosition = myposition;
        _distance = distance;
        _ViewAngle = viewangle;
        _InitialNode = initialnode;
        _FinalNode = finalNode;
        _Path = Path;
        _Velocity = velocity;
        _maxVelocity = maxvelocity;
        _maxForce = maxforce;
        _enemy = enemy;
    }


    public void onEnter()
    {
        Debug.Log($" {_enemy.gameObject.name} calculo a star ");

        _InitialNode = ManagerNode.Instance.NearsNode(_MyPosition);
        _FinalNode = ManagerNode.Instance.NearsNode(_target);
        
        _Path = Pathfinding.instance.CalculateAStar(_InitialNode, _FinalNode);
    }

    public void onUpdate()
    {
        if (Pathfinding.instance.InFov(_target, _MyPosition, _distance, _ViewAngle))
        {
            _FSM.ChangeState("Hunt");
        }

        else
        {
            if (_Path.Count > 0)
            {
                Debug.Log(" Pathfinding ");

                AddForce(Seek(_Path[0].transform.position));

                if (Vector3.Distance(_MyPosition.gameObject.transform.position, _Path[0].transform.position) <= 0.3f) _Path.RemoveAt(0);

                _MyPosition.transform.position += _Velocity * Time.deltaTime;
                _MyPosition.transform.forward = _Velocity;

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

    public void AddForce(Vector3 dir)
    {
        _Velocity += dir;
    }

    Vector3 Seek(Vector3 target)
    {
        var desired = target - _MyPosition.transform.position;
        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _Velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);

       // Debug.Log("seek");

        return steering;
    }
}
