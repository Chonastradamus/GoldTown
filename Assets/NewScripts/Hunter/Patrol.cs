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

    GivePosition _GivePosition;


    public Patrol(FSM_Manager fsm, Transform[] _WP, Transform Target ,float MaxVelocity, float MaxForce, Transform EnemyPosition, int ActualIndex, Vector3 Velocity, float Distance, float ViewAngle)
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
        
    }

    public void onEnter()
    {
        Debug.Log(" Patroling the area ");
       // GameManager.instance.Call += calling();
    }

    public void onUpdate()
    {
        if (Pathfinding.instance.InFov(_target, _MyPosition, _distance, _ViewAngle))
        {
            fSM_.ChangeState("Hunt");
        }
        else
        {
            Waypoints();
        }

        /*if (GameManager.instance.Call())
        {

        }*/

        _MyPosition.transform.position += _Velocity * Time.deltaTime;
        _MyPosition.transform.forward = _Velocity;
    }

    public void OnExit()
    {
        Debug.Log(" I see a enemy ");
    }
    
    public void Waypoints()
    {
        AddForce(Seek(_Waypoints[_actualIndex].position));

        Debug.Log("waypoints");

        if (Vector3.Distance(_MyPosition.transform.position, _Waypoints[_actualIndex].position) <= 0.3f)
        {
            _actualIndex++;

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

       // Debug.Log("seek");

        return steering;
    }

    public void calling(Vector3 pos)
    {

        fSM_.ChangeState("serchposition");

    }

    // pasar de estado al activar el evento haciendo que el que vio al enemigo lo persiga, mientras que los demas tienen que usar pathfinding para hacercarce al lugar. 
}
