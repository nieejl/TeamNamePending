using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    static Plane xyPlane = new Plane(Vector3.up, new Vector3(0f, 0f, 0f));
    
    public static Vector3 ToWorldPoint(this Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        xyPlane.Raycast(ray, out float distance);

        return ray.GetPoint(distance);
    }

    public static Vector3 ChangeDirectionRelativeToCamera(this Vector3 direction, Transform cameraTransform)
    {
        var relativeDirection = Camera.main.transform.TransformDirection(direction);
        relativeDirection.y = 0;
        return relativeDirection.normalized;
    }
}
