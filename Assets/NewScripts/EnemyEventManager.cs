using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventManager : MonoBehaviour
{
    public delegate void PlayerDetectedEventHandler(Vector3 playerPosition);
    public static event PlayerDetectedEventHandler OnPlayerDetected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
