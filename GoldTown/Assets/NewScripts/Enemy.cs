using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float distance,ViewAngle;
    public Transform target;

    public delegate void DetectionEvent();
    public event DetectionEvent Detection;


    public Transform[] waypoints;

    public float maxVelocity;
    public float maxForce;
    Vector3 _velocity;
    public Vector3 Velocity { get { return _velocity; } }
    int _actualIndex;

    void Update()
    {
        Waypoints();

        if (InFov(target))
        {
            Debug.Log("te Veo");
            transform.position = target.position;

        }

        else
        {
            print("no te veo");
        }
    }

    public void Waypoints()
    {
        AddForce(Seek(waypoints[_actualIndex].position));

        if (Vector3.Distance(transform.position, waypoints[_actualIndex].position) <= 0.3f)
        {
            _actualIndex++;

            if (_actualIndex >= waypoints.Length)
                _actualIndex = 0;
        }

        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity;
    }

    Vector3 Seek(Vector3 target)
    {
        var desired = target - transform.position;
        desired.Normalize();
        desired *= maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        return steering;
    }

    public void AddForce(Vector3 dir)
    {
        _velocity += dir;

    }


    // clases 
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


    public void Calling()
    {
        print("voy para alla");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, ViewAngle);


        Vector3 lineA = GetVectorFromAngle(distance * 0.5f + transform.eulerAngles.y);
        Vector3 lineB = GetVectorFromAngle(-distance * 0.5f + transform.eulerAngles.y);
        Gizmos.DrawLine(transform.position, transform.position + lineA * ViewAngle);
        Gizmos.DrawLine(transform.position, transform.position + lineB * ViewAngle);

    }


    Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }


}
