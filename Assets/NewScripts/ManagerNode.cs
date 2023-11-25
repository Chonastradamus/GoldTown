using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerNode : MonoBehaviour
{
    public static ManagerNode Instance;
    public Node[] NodosInScene;

    private void Awake()
    {
        Instance = this;
    }



}
