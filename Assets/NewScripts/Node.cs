using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Node : MonoBehaviour
{
    public List<Node> _neighbors = new List<Node>();
   
    public int cost;
  
    Grid _grid;
    int _x, _y;

    public void SetCost(int newCost)
    {
        cost = Mathf.Clamp(newCost, 1, 99);
        //GetComponentInChildren<TextMeshProUGUI>().text = cost + "";
    }
}
