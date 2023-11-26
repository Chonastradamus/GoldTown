using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public Node NearsNode(Transform MyPosition)
    {
        Node nodeMasCercano = default;

        foreach (var item in ManagerNode.Instance.NodosInScene)
        {
            var maxdistance = Mathf.Infinity;

            var dir = Vector3.Distance(MyPosition.position, item.transform.position);

            if (GameManager.instance.InLineOfSight(MyPosition.position, item.transform.position))
            {
                if (dir < maxdistance)
                {
                    nodeMasCercano = item;
                }
            }
        }

        return (nodeMasCercano);
    }

}
