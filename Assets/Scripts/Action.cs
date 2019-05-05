using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Action : MonoBehaviour
{
    internal Selectable Selectable;

    public abstract void MakeAction(Vector2 vector2);
}