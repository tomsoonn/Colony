using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class CollectAction : Action
{
    private GameObject _target;
    public float targetDelta = 3f;
    public float CollectAmount = 0.3f;
    public float CollectSpeed = 0.5f;

    private float _mined = 0;
    private GameObject _resourceTarget;

    private void Start()
    {
        Selectable = GameObject.Find("Canvas")
            .transform.Find("ActionPanel")
            .transform.Find("CollectButton")
            .GetComponent<Toggle>();
        _target = new GameObject("targetMoveAction");
        _target.transform.SetParent(GameObject.Find("Astar").GetComponent<AstarPath>().transform);
        StartCoroutine("Collect");
    }

    public override void MakeAction(Vector2 vector2)
    {
        Debug.Log("CollectAction");

        var vec = new Vector3(vector2.x, vector2.y, 0);
        Debug.Log("vec" + vec.x + " " + vec.y + " " + vec.z);

        int mask = 1 << 8;
        var raycast = Physics2D.Raycast(vector2, Vector2.zero, Mathf.Infinity, mask);
        if (raycast)
        {
            Debug.Log("hit" + mask);
            if (raycast.collider != null)
            {
                _resourceTarget = raycast.collider.gameObject;
                _target.transform.position = vec;
                GetComponent<AIDestinationSetter>().target = _target.transform;
            }
        }
        else
        {
            Debug.DrawRay(vec, new Vector3(0, 0, 100), Color.red, Mathf.Infinity);
        }

        Debug.Log("end");
    }

    private IEnumerator Collect()
    {
        for (;;)
        {
            yield return new WaitForSeconds(CollectSpeed);
            if (_resourceTarget != null &&
                Vector3.Distance(_resourceTarget.transform.position, gameObject.transform.position) < targetDelta)
            {
                if (_mined > 1)
                {
                    _mined -= 1;
                    var resourceManager = GameObject.Find("GameController").GetComponent<ResourceManager>();
                    Resource res = _resourceTarget.GetComponent<Resource>();
                    resourceManager.ChangeResourceAmount(res, 1);
                }
                else
                {
                    _mined += CollectAmount;
                }
            }
            else
            {
                _mined = 0;
            }
        }
    }
}