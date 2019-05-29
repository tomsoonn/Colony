using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveAction : Action
{
    private GameObject _target;

    private void Start()
    {
        Selectable  = GameObject.Find("Canvas")
            .transform.Find("ActionPanel")
            .transform.Find("MoveButton")
            .GetComponent<Toggle>();
        _target = new GameObject("targetMoveAction");
        _target.transform.SetParent(GameObject.Find("Astar").GetComponent<AstarPath>().transform);
    }

    public override void MakeAction(Vector2 vector2)
    {
        Debug.Log("ActionMove");
        _target.transform.position = new Vector3(vector2.x, vector2.y, 0);
        GetComponent<AIDestinationSetter>().target = _target.transform;
//        GetComponent<AIPath>().SearchPath();

    }
}