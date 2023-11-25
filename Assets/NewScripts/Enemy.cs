using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float distance,ViewAngle;
    public Transform target;

    public Transform[] waypoints;

    public float maxVelocity;
    public float maxForce;
    Vector3 _velocity;
    public Vector3 Velocity { get { return _velocity; } }
    int _actualIndex;
    bool seen ;
    FSM_Manager _FSM;

    public GivePosition _giviPosition;


    void Awake()
    {
        _giviPosition.Detection += InFov;
         GameManager.instance.Call += goposition;

        _FSM = new FSM_Manager();

        _FSM.CreateState("idle", new thepath(_FSM));
        _FSM.CreateState("Patrol", new Patrol(_FSM, waypoints, target ,maxVelocity , maxForce, transform, _actualIndex, _velocity, distance, ViewAngle, _giviPosition));
        _FSM.CreateState("Hunt", new Hunt(_FSM, target, maxVelocity, maxVelocity, transform, _actualIndex, _velocity, distance, ViewAngle, _giviPosition));
        _FSM.ChangeState("Patrol");
    }

    void Update()
    {
        _FSM.execute();
        if (InFov(target))
        {
            GameManager.instance.Call(target.position);
            //_giviPosition.Detection(target.transform);
        }

/*
        {
            _FSM.ChangeState("Hunt");
            /* Debug.Log("te Veo");
             AddForce(Seek(target.position));

             if (!seen)
             {
                 GameManager.instance.Call(target.position);

               //Pathfinding.instance.CalculateAStar(this.transform.position,)
                 //GP.LocationPlayer(target.p3osition);
                 seen = true;
             }
        }
        else
        {
          
            print("no te veo");
            seen = false;
        }
        transform.position += _velocity * Time.deltaTime;
            transform.forward = _velocity;
        */

        
    }
    // idel
    public void Waypoints()
    {
        AddForce(Seek(waypoints[_actualIndex].position));

        Debug.Log("waypoints");

        if (Vector3.Distance(transform.position, waypoints[_actualIndex].position) <= 0.3f)
        {
            _actualIndex++;

            if (_actualIndex >= waypoints.Length)
                _actualIndex = 0;
        }
    }
    //en hunt
    Vector3 Seek(Vector3 target)
    {
        var desired = target - transform.position;
        desired.Normalize();
        desired *= maxVelocity;

        var steering = desired - _velocity *10;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        Debug.Log("seek");

        return steering;
    }

    public void AddForce(Vector3 dir)
    {
        _velocity += dir;
    }

    #region Infov
    public bool InFov(Transform obj)
    {
        var dir =  obj.position - transform.position;
        if ( dir.magnitude < distance)
        {

            // calcula el angulo entre yo mirando adelante y la posicion del jugador y si este es menor o igual a un angulo que dicemos de los dos lados
            if (Vector3.Angle(transform.forward,dir) <= ViewAngle * 0.5)
            {
                return GameManager.instance.InLineOfSight(transform.position,obj.position);
            }
        }


        return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, distance);


        Vector3 lineA = GetVectorFromAngle(ViewAngle * 0.5f + transform.eulerAngles.y);
        Vector3 lineB = GetVectorFromAngle(-ViewAngle * 0.5f + transform.eulerAngles.y);
        Gizmos.DrawLine(transform.position, transform.position + lineA * distance);
        Gizmos.DrawLine(transform.position, transform.position + lineB * distance);

    }

    Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    #endregion Infov
    

    public void Calling(Vector3 position)
    {
        print("voy para alla");
    }


    public void goposition(Vector3 pos)
    {
        
    }

}
