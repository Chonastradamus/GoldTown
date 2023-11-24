using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsMove : MonoBehaviour
{
    public Transform[] waypoints;

    public float maxVelocity;
    public float maxForce;
    Vector3 _velocity;
    public Vector3 Velocity { get { return _velocity; } }
    int _actualIndex;

    void Update()
    {
       Waypoints();
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

        _velocity = Vector3.ClampMagnitude(_velocity, maxVelocity);
    }
}
