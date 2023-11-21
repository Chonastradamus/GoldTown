using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    FSM_Manager _FSM;
    // Start is called before the first frame update
    void Start()
    {
        _FSM = new FSM_Manager();

        _FSM.CreateState("idle", new Idel(_FSM));
        _FSM.CreateState("Patrol", new Patrol(_FSM));
        _FSM.CreateState("Hunt", new Hunt(_FSM));

        _FSM.ChangeState("idle");

    }

    void Update()
    {
        _FSM.execute();
    }
}
