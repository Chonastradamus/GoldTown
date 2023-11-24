using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNode : MonoBehaviour
{
    public Node[] neighbours;

    private void Start()
    {
        foreach (var item in neighbours)
        {
           // usar line of sight para que busque los nodos cerca de el y que depsues
           // de toda este array de nodos cunado se active los nodos busque el nodo mas cercano.
        }
    }
}
