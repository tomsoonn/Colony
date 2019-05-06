using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveFactor = 0.5f;
    public float fasterFactor = 2f;
    public Vector2 leftUpperBound = new Vector2Int(-20, -20);
    public Vector2 rightBottomBound = new Vector2Int(20, 20);

    public float minZoom = 3f;
    public float maxZoom = 15f;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        Assert.Less(leftUpperBound.x, rightBottomBound.x, "incorrect bound values set");
        Assert.Less(leftUpperBound.y, rightBottomBound.y, "incorrect bound values set");
    }

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void ZoomCamera()
    {
        var zoom = _camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * 10f;
        float newFov;
        if (zoom < minZoom)
        {
            newFov = minZoom;
        }
        else if (zoom > maxZoom)
        {
            newFov = maxZoom;
        }
        else
        {
            newFov = zoom;
        }

        _camera.orthographicSize = newFov;
    }

    private float Clamp(float value, float min, float max)
    {
        if ((double) value < (double) min)
            value = min;
        else if ((double) value > (double) max)
            value = max;
        return value;
    }

    private void MoveCamera()
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