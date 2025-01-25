using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [Range(0f, 1f)]
    public float Smoothness = 0.95f;

    void Update()
    {
        transform.position = Vector3.Lerp(
            a: transform.position,
            b: Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)
            ),
            t: Smoothness
        );
    }
}
