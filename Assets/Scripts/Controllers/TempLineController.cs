using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class TempLineController : MonoBehaviour
{
    [SerializeField]
    private UILineRenderer UILineRenderer;
    [SerializeField]
    private ScrollRect workspaceScrollRect;

    public static TempLineController Instance { get; private set; }
    private void Awake() => Instance = this;

    private RectTransform startPoint;
    public bool TempWireVisible { get; private set; } = false;
    public void ShowTempWire(RectTransform startPoint)
    {
        if (TempWireVisible) return;

        workspaceScrollRect.enabled = false;
        this.startPoint = startPoint;
        TempWireVisible = true;
    }

    public void HideTempWire()
    {
        workspaceScrollRect.enabled = true;
        startPoint = null;
        UILineRenderer.Points = new Vector2[1] { new Vector2(0, 0) };
        TempWireVisible = false;
    }

    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private void Update()
    {
        if (startPoint == null) return;

        firstPoint = startPoint.TransformPoint((UILineRenderer.transform as RectTransform).pivot);
        firstPoint = GlobalLinksStorage.Instance.CanvasRectTransform.InverseTransformPoint(firstPoint);
        secondPoint = GlobalLinksStorage.Instance.CanvasRectTransform.InverseTransformPoint(Input.mousePosition);

        UILineRenderer.Points = new Vector2[2] { firstPoint, secondPoint };
    }
}
