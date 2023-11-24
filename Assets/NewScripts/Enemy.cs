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

    public GivePosition GP;

    bool seen ;

    void Awake()
    {
        GP.Detection += goposition;
    }

    void Update()
    {
        if (!InFov(target))
        {
            Waypoints();
            print("no te veo");
            seen = false;
        }
        else
        {
            Debug.Log("te Veo");
            AddForce(Seek(target.position));

            if (!seen)
            {
                GP.LocationPlayer(target.position);
                seen = true;
            }
        }
            transform.position += _velocity * Time.deltaTime;
            transform.forward = _velocity;
    }

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

    Vector3 Seek(Vector3 target)
    {
        var desired = target - transform.position;
        desired.Normalize();
        desired *= maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        Debug.Log("seek");

        return steering;
    }

    public void AddForce(Vector3 dir)
    {
        _velocity += dir;
    }

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

    public void Calling(Vector3 position)
    {
        print("voy para alla");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, distance);


        Vector3 lineA = GetVectorFromAngle(distance * 0.5f + transform.eulerAngles.y);
        Vector3 lineB = GetVectorFromAngle(-distance * 0.5f + transform.eulerAngles.y);
        Gizmos.DrawLine(transform.position, transform.position + lineA * ViewAngle);
        Gizmos.DrawLine(transform.position, transform.position + lineB * ViewAngle);

    }

    Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
/*
    public void goposition(Vector3 pos)
    {
        
    }
*/
}
