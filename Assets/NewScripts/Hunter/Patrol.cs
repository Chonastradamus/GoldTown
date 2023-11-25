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

    public Patrol(FSM_Manager fsm, Transform[] _WP, Transform TA ,float MV, float MF, Transform MP, int AI, Vector3 V, float DIS, float VIA, GivePosition GP)
    {
        fSM_ = fsm;
        _Waypoints = _WP;
        _target = TA;
        _maxForce = MF;
        _maxVelocity = MV;
        _MyPosition = MP;
        _actualIndex = AI;
        _Velocity = V;
        _distance = DIS;
        _ViewAngle = VIA;
        _GivePosition = GP;
    }
    public void onEnter()
    {
        Debug.Log(" Patroling the area ");

        /*Debug.Log("velocidad" + _maxVelocity);
        Debug.Log("maxforce" + _maxForce);*/
    }

    public void onUpdate()
    {
        if (_GivePosition.Detection(_target.transform))
            fSM_.ChangeState("Hunt");
        else
            Waypoints();

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

    /*public bool InFov(Transform obj)
    {
        var dir = obj.position - _MyPosition.transform.position;
        if (dir.magnitude < _distance)
        {
            // calcula el angulo entre yo mirando adelante y la posicion del jugador y si este es menor o igual a un angulo que dicemos de los dos lados
            if (Vector3.Angle(_MyPosition.transform.forward, dir) <= _ViewAngle * 0.5)
            {
                return GameManager.instance.InLineOfSight(_MyPosition.transform.position, obj.position);
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(_MyPosition.transform.position, _distance);


        Vector3 lineA = GetVectorFromAngle(_ViewAngle * 0.5f + _MyPosition.transform.eulerAngles.y);
        Vector3 lineB = GetVectorFromAngle(-_ViewAngle * 0.5f + _MyPosition.transform.eulerAngles.y);
        Gizmos.DrawLine(_MyPosition.transform.position, _MyPosition.transform.position + lineA * _distance);
        Gizmos.DrawLine(_MyPosition.transform.position, _MyPosition.transform.position + lineB * _distance);

    }

    Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }*/
}
