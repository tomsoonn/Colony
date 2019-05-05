using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveFactor = 0.5f;
    public float fasterFactor = 2f;
    public Vector2 leftUpperBound = new Vector2Int(-100, -100);
    public Vector2 rightBottomBound = new Vector2Int(100, 100);

    private void Start()
    {
        Assert.Less(leftUpperBound.x, rightBottomBound.x, "incorrect bound values set");
        Assert.Less(leftUpperBound.y, rightBottomBound.y, "incorrect bound values set");
    }

    private void Update()
    {
        var deltaX = Input.GetAxisRaw("Horizontal") * moveFactor;
        var deltaY = Input.GetAxisRaw("Vertical") * moveFactor;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            deltaX *= fasterFactor;
            deltaY *= fasterFactor;
        }

        var position = transform.position;
        position = new Vector3(
            Mathf.Clamp(position.x + deltaX, leftUpperBound.x, rightBottomBound.x),
            Mathf.Clamp(position.y + deltaY, leftUpperBound.y, rightBottomBound.y),
            position.z);
        transform.position = position;
    }
}