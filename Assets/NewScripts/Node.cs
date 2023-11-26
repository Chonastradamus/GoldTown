using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    public List<Node> _neighbors = new List<Node>();
   // public LayerMask WallLayer;
    public int cost;

    private void Start()
    {
        foreach (var item in ManagerNode.Instance.NodosInScene)
        {
            if(item != this)
            {

                var dir = item.transform.position - transform.position;

                if (GameManager.instance.InLineOfSight(this.transform.position,item.transform.position))
                {
                    _neighbors.Add(item);
                }
            }
        }
    }

    public void SetCost(int newCost)
    {
        cost = Mathf.Clamp(newCost, 1, 99);
        //GetComponentInChildren<TextMeshProUGUI>().text = cost + "";
    }
}
