using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Istate
{
    Transform[] _Waypoints;
    FSM_Manager fSM_;
    float _maxVelocity;
    float _maxForce;
    Transform _target;
    Transform _MyPosition;
    int _actualIndex;
    Vector3 _Velocity;
    float _distance, _ViewAngle;
    Enemy _enemy;

    Node _InitialNode;
    Node _FinalNode;
    List<Node> _Path;

    public Patrol(FSM_Manager fsm, Transform[] _WP, Transform Target ,float MaxVelocity, float MaxForce, Transform EnemyPosition, int ActualIndex, Vector3 Velocity, float Distance, float ViewAngle, Enemy enemy, Node initial, Node goal, List<Node> Path)
    {
        fSM_ = fsm;

        _Waypoints = _WP;
        _target = Target;
        _maxForce = MaxForce;
        _maxVelocity = MaxVelocity;
        _MyPosition = EnemyPosition;
        _actualIndex = ActualIndex;
        _Velocity = Velocity;
        _distance = Distance;
        _ViewAngle = ViewAngle;
        _enemy = enemy;
        _InitialNode = initial;
        _FinalNode = goal;
        _Path = Path;
        
    }

    public void onEnter()
    {

        _InitialNode = ManagerNode.Instance.NearsNode(_MyPosition);
        _FinalNode = ManagerNode.Instance.NearsNode(_target);

        _Path = Pathfinding.instance.CalculateAStar(_InitialNode, _FinalNode);
    }

    public void onUpdate()
    {
        if (!Pathfinding.instance.InFov(_target, _MyPosition, _distance, _ViewAngle) && _enemy.Path.Count <= 0)
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
        }

        if (Pathfinding.instance.InFov(_target, _MyPosition, _distance, _ViewAngle))
        {
            fSM_.ChangeState("Hunt");
            GameManager.instance.Call(_target.position);
        }


        _MyPosition.transform.position += _Velocity * Time.deltaTime;
        _MyPosition.transform.forward = _Velocity;
    }

    public void OnExit()
    {
        //Debug.Log(" I see a enemy ");
    }
    
    public void Waypoints()
    {
        AddForce(Seek(_Waypoints[_actualIndex].position));


        if (Vector3.Distance(_MyPosition.transform.position, _Waypoints[_actualIndex].position) <= 0.3f)
        {
            _actualIndex++;

            _enemy.currentwaypoint = _actualIndex;

            if (_actualIndex >= _Waypoints.Length)
                _actualIndex = 0;
        }
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

        return steering;
    }



}
