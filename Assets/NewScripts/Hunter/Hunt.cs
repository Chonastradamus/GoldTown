using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunt :  Istate
{
 
    FSM_Manager fSM_;
    Enemy _enemy;
    Transform _player;



    public Hunt(FSM_Manager fsm,  Enemy enemy, Transform target)
    {
        fSM_ = fsm;
        _enemy = enemy;
        _player = target;
        
    }

    public void onEnter()
    {
        //Debug.Log(" follow the enemy ");
   
        GameManager.instance.Call(_player.position);

    }

    public void onUpdate()
    {
        if (Pathfinding.instance.InFov(_enemy.target, _enemy.transform, _enemy.distance, _enemy.ViewAngle))
        {
            _enemy.AddForce(_enemy.Seek(_player.position));

            foreach (var item in GameManager.instance.enemis)
            {
                item.reciv = true;
            }
            
        }
       if(_enemy.reciv)
        {
            fSM_.ChangeState("serchposition");
        }  
        else
        {
            fSM_.ChangeState("Patrol");
        }


        _enemy.transform.position += _enemy.Velocity * Time.deltaTime;
        _enemy.transform.forward = _enemy.Velocity;
    }

    public void OnExit()
    {
       // Debug.Log(" I don�t found the Enemy ");
    }

  

}
