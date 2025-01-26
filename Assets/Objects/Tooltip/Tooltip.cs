using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private Canvas _canvas;

    public TextMeshProUGUI Header;
    public TextMeshProUGUI Body;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    public void Update()
    {
        // Get the mouse position in screen space
        Vector2 screenPosition = Input.mousePosition;

        RectTransform canvasTransform = _canvas.transform as RectTransform;

        // Convert screen position to canvas local position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform,
            screenPosition,
            null,
            out Vector2 localPoint
        );

        Vector2 adjustedPosition = new Vector2(
            localPoint.x + canvasTransform.pivot.x * canvasTransform.rect.width + 10,
            localPoint.y + canvasTransform.pivot.y * canvasTransform.rect.height + 10
        );

        transform.position = adjustedPosition;
    }
}
