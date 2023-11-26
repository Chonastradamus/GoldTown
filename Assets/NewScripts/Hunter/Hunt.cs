using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunt :  Istate
{
    FSM_Manager fSM_;
 
    Enemy _enemy;



    public Hunt(FSM_Manager fsm,  Enemy enemy)
    {
        fSM_ = fsm;
        _enemy = enemy;
        
    }

    public void onEnter()
    {
        //Debug.Log(" follow the enemy ");
        GameManager.instance.Call += calling;
    }

    public void onUpdate()
    {
        if (Pathfinding.instance.InFov(_enemy.target, _enemy.transform, _enemy.distance, _enemy.ViewAngle))
        {
            _enemy.AddForce(_enemy.Seek(_enemy.target.position));
            GameManager.instance.Call(_enemy.target.position);

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


        _enemy.transform.position += _enemy.Velocity * Time.deltaTime;
        _enemy.transform.forward = _enemy.Velocity;
    }

    public void OnExit()
    {
       // Debug.Log(" I don�t found the Enemy ");
    }

  

    public void calling(Vector3 pos)
    {
        fSM_.ChangeState("serchposition");
    }
}
