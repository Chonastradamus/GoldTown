using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunt :  Istate
{
    FSM_Manager fSM_;
    float _maxVelocity;
    float _maxForce;
    Transform _target;
    Transform _MyPosition;
    int _actualIndex;
    Vector3 _Velocity;
    float _distance, _ViewAngle;

    public Hunt(FSM_Manager fsm, Transform TA, float MV, float MF, Transform MP, int AI, Vector3 V, float DIS, float VIA)
    {
        fSM_ = fsm;
        _target = TA;
        _maxVelocity = MV;
        _maxForce = MF;
        _MyPosition = MP;
        _actualIndex = AI;
        _Velocity = V;
        _distance = DIS;
        _ViewAngle = VIA;
    }
    public void onEnter()
    {
        Debug.Log(" follow the enemy ");
    }

    public void onUpdate()
    {
        if (!InFov(_target))
            fSM_.ChangeState("Patrol");
        else
            AddForce(Seek(_target.position));

        _MyPosition.transform.position += _Velocity * Time.deltaTime;
        _MyPosition.transform.forward = _Velocity;
    }

    public void OnExit()
    {
        Debug.Log(" I don´t found the Enemy ");
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

    public bool InFov(Transform obj)
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
    }
}
