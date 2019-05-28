using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionsShower : MonoBehaviour
{
    public Action[] actions;
    private GameObject _panel;

    private void Start()
    {
        _panel = GameObject.Find("ActionPanel");
    }

    private void OnMouseDown()
    {
        Debug.Log("mouse");
        if (!Input.GetMouseButtonDown(0)) return;
        var pc = _panel.GetComponent<PanelController>();
        pc.Clean();
        pc.LastActionsShower = this;

        ShowPanel(pc);
        SetActionEnabled(true);
        pc.CleanBuilding();

    }

    private void ShowPanel(PanelController pc)
    {
        var sr = GetComponent<SpriteRenderer>();
        pc.CreateImage(sr.sprite);
    }


    public void SetActionEnabled(bool isEnabled)
    {
        foreach (var action in actions)
        {
            action.Selectable.gameObject.SetActive(isEnabled);
        }
    }

}