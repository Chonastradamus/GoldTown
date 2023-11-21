using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    List<Node> _path = new List<Node>();
    public float Speed;

    void Update()
    {
        Move();
    }

    public void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(horizontal, 0, vertical) * Speed * Time.deltaTime;

    }
    public void SetPath(List<Node> newPath)
    {
        _path.Clear();

        foreach (var item in newPath)
            _path.Add(item);
    }
}
