using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public static PanelController me;
    private GameObject _lastDisplayImage;
    public GameObject displayImage;
    internal ActionsShower LastActionsShower;
    private Action _currentAction;

    bool buildFlag = false;

    void Awake()
    {
        me = this;
    }

    private void Start()
    {
        foreach (var button in new List<Selectable>
        {
            GameObject.Find("MoveButton").GetComponent<Toggle>(),
            GameObject.Find("CollectButton").GetComponent<Toggle>()
        })
        {
            button.gameObject.SetActive(false);
        }
    }

    public void OnClick(BaseEventData pointer)
    {
        Debug.Log("Click detected");
        var pointerData = pointer as PointerEventData;
        if (pointerData == null) return;

        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            Clean();
            CleanBuilding();
        }
        else if (LastActionsShower && _currentAction)
        {
            System.Diagnostics.Debug.Assert(Camera.main != null, "Camera.main != null");
            _currentAction.MakeAction(Camera.main.ScreenToWorldPoint(pointerData.position));
        }
        else if (LastActionsShower)
        {
            Clean();
        }

        GameObject selectedBuilding = BuildingStore.me.GetToBuild();
        if (selectedBuilding != null)
        {
            if (buildFlag)
            {
                BuildingStore.me.CreateBuilding(Camera.main.ScreenToWorldPoint(pointerData.position));
                buildFlag = false;
            }
            else
            {
                buildFlag = true;
            }
        }
    }

    public void Clean()
    {
        Debug.Log("clean");
        TurnOffOldAction();
        TurnOffLastActionShower();
        CleanImage();
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public void CleanBuilding()
    {
        BuildingStore.me.CleanSelectedBuilding();
        buildFlag = false;
    }

    private void TurnOffLastActionShower()
    {
        if (!LastActionsShower) return;
        LastActionsShower.SetActionEnabled(false);
        LastActionsShower = null;
    }

    private void CleanImage()
    {
        if (!_lastDisplayImage) return;
        Destroy(_lastDisplayImage);
        _lastDisplayImage = null;
    }


    public void CreateImage(Sprite sprite)
    {
        _lastDisplayImage = Instantiate(displayImage, transform);
        _lastDisplayImage.GetComponent<Image>().sprite = sprite;
        GetComponent<CanvasGroup>().alpha = 1;
    }

    public void ToggleAction(Object actionObj)
    {
        if (!LastActionsShower) return;
        var button = actionObj as Toggle;
        if (!button) return;

        if (button.isOn)
        {
            SetCurrentAction(actionObj);
        }
        else
        {
            _currentAction = null;
        }
    }

    public void SetCurrentAction(Object actionObj)
    {
        if (!LastActionsShower) return;
        var button = actionObj as Selectable;
        TurnOffOldAction();
        foreach (var action in LastActionsShower.actions)
        {
            if (action.Selectable != button) continue;
            _currentAction = action;
            break;
        }
    }

    private void TurnOffOldAction()
    {
        if (!_currentAction) return;
        var oldToggle = _currentAction.Selectable as Toggle;
        if (oldToggle)
        {
            oldToggle.isOn = false;
            oldToggle.GetComponentInParent<Animator>().Rebind();
        }
        _currentAction = null;
    }
}