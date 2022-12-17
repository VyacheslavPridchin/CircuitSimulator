using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkspaceZoomController : MonoBehaviour
{
    [SerializeField]
    private RectTransform workspaceArea;
    [SerializeField]
    private ScrollRect workspaceScrollRect;
    [SerializeField]
    private float zoomOutMin = 0.8f;
    [SerializeField]
    private float zoomOutMax = 5f;

    private Vector2 touchStart0, touchStart1;
    private float distanceStart;
    private float localSizeStart;

    private void Update()
    {
        if (Input.touchCount == 0 && !Input.GetMouseButton(0))
        {
            ResetDataForZoom();
        }
        else if (Input.touchCount >= 2)
        {
            workspaceScrollRect.enabled = false;
            if (touchStart0 == Vector2.zero || touchStart1 == Vector2.zero)
            {
                touchStart0 = Input.GetTouch(0).position;
                touchStart1 = Input.GetTouch(1).position;
                distanceStart = Vector2.Distance(touchStart0, touchStart1);
                localSizeStart = workspaceArea.localScale.x;
            }

            var touch0 = Input.GetTouch(0).position;
            var touch1 = Input.GetTouch(1).position;
            var distance = Vector2.Distance(touch0, touch1);

            var delta = distance - distanceStart;
            delta = delta * 0.001f;

            var scale = Mathf.Clamp(localSizeStart + delta, zoomOutMin, zoomOutMax);
            workspaceArea.localScale = new Vector3(scale, scale, scale);
        }

        ScrollWheelZoom();
    }

    private void ResetDataForZoom()
    {
        touchStart0 = Vector2.zero;
        touchStart1 = Vector2.zero;
        distanceStart = 0;
        workspaceScrollRect.enabled = true;
    }

    private void ScrollWheelZoom()
    {
        var increment = Input.GetAxis("Mouse ScrollWheel");

        var scale = Mathf.Clamp(workspaceArea.localScale.x + increment, zoomOutMin, zoomOutMax);
        workspaceArea.localScale = new Vector3(scale, scale, scale);
    }
}
