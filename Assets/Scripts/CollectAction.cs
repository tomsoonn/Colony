using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectAction : Action
{
    private void Start()
    {
        Selectable = GameObject.Find("CollectButton").GetComponent<Toggle>();
    }

    public override void MakeAction(Vector2 vector2)
    {
        Debug.Log("BuildAction");
    }
}