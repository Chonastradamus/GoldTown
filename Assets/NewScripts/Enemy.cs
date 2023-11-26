using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float distance,ViewAngle;
    [SerializeField] Transform target;

    public Transform[] waypoints;

    public float maxVelocity;
    public float maxForce;
    Vector3 _velocity;
    public Vector3 Velocity { get { return _velocity; } }
    int _actualIndex;
    FSM_Manager _FSM;
    public int currentwaypoint;

    public Node initial;
    public Node Goal;
    public List<Node> Path;

    public bool reciv; 

    void Awake()
    {
        
        // GameManager.instance.Call += goposition;

        _FSM = new FSM_Manager();

        _FSM.CreateState("idle", new thepath(_FSM));

        _FSM.CreateState("serchposition", new GoToLastPosition(_FSM, target, transform, distance, ViewAngle, initial, Goal, Path, _velocity, maxVelocity, maxForce, this));
        _FSM.CreateState("Patrol", new Patrol(_FSM, waypoints, target ,maxVelocity , maxForce, transform, _actualIndex, _velocity, distance, ViewAngle, this, initial, Goal, Path));
        _FSM.CreateState("Hunt", new Hunt(_FSM, target, maxVelocity, maxForce, transform, _velocity, distance, ViewAngle, this));
        _FSM.ChangeState("Patrol");
    }

    void Update()
    {
        _FSM.execute();

      
    }

    #region ShowGizmos
 

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
    #endregion ShowGizmos


    public void Calling(Vector3 position)
    {
        print("voy para alla");
    }


    public void goposition(Vector3 pos)
    {

        _FSM.ChangeState("serchposition");
    }

}
