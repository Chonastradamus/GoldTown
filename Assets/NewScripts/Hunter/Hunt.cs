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
    Vector3 _Velocity;
    float _distance, _ViewAngle;
    Enemy _enemy;



    public Hunt(FSM_Manager fsm, Transform Target, float MaxVelocity, float MaxForce, Transform MyPosition, Vector3 Velocity, float Distance, float ViewAngle, Enemy enemy)
    {
        fSM_ = fsm;
        _target = Target;
        _maxVelocity = MaxVelocity;
        _maxForce = MaxForce;
        _MyPosition = MyPosition;
        _Velocity = Velocity;
        _distance = Distance;
        _ViewAngle = ViewAngle;
        _enemy = enemy;
        
    }

    public void onEnter()
    {
        Debug.Log(" follow the enemy ");
        GameManager.instance.Call += calling;
    }

    public void onUpdate()
    {
        if (Pathfinding.instance.InFov(_target, _MyPosition, _distance, _ViewAngle))
        {
            AddForce(Seek(_target.position));
            GameManager.instance.Call(_target.transform.position);

            foreach (var item in GameManager.instance.enemis)
            {
                item.reciv = true;
            }
            
        }
        else if(_enemy.reciv)
        {
            fSM_.ChangeState("serchposition");
        }  
        else
        {
            fSM_.ChangeState("Patrol");
        }


        _MyPosition.transform.position += _Velocity * Time.deltaTime;
        _MyPosition.transform.forward = _Velocity;
    }

    public void OnExit()
    {
        Debug.Log(" I don�t found the Enemy ");
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

        Debug.Log("seek");

        return steering;
    }

    public void calling(Vector3 pos)
    {
        fSM_.ChangeState("serchposition");
    }
}
