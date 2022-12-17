using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(Point))]
public class OutputBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<Point>().WireOutput != null) return;

        TempLineController.Instance.ShowTempWire(transform as RectTransform);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<Point>().WireOutput != null)
            Destroy(GetComponent<Point>().WireOutput.gameObject);

        CircuitController.Instance.LateCheckCercuitContinuity();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TempLineController.Instance.HideTempWire();

        var position = GlobalLinksStorage.Instance.CanvasRectTransform.InverseTransformPoint(Input.mousePosition);

        LayerMask mask = LayerMask.GetMask("Points");

        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, 1, mask);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Point")
            {
                if (hit.collider.GetComponent<Point>().WireInput != null) return;
                if (hit.collider.GetComponent<Point>().WireOutput != null)
                if (hit.collider.GetComponent<Point>().WireOutput == GetComponent<Point>().WireInput) return;
                if (hit.collider.GetComponent<Point>().ElectricalComponent == GetComponent<Point>().ElectricalComponent) return;


                GameObject wire = new GameObject();
                wire.transform.SetParent(GlobalLinksStorage.Instance.WiresStorage);
                wire.name = "Wire";

                ElectricalWire wireComponent = wire.AddComponent<ElectricalWire>();
                wireComponent.Input = GetComponent<Point>();
                wireComponent.Output = hit.collider.GetComponent<Point>();

                CircuitController.Instance.LateCheckCercuitContinuity();
            }
        }
    }
}
