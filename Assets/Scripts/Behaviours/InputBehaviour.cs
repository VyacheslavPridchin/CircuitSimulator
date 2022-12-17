using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Point))]
public class InputBehaviour : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<Point>().WireInput != null)
            Destroy(GetComponent<Point>().WireInput.gameObject);

        CircuitController.Instance.LateCheckCercuitContinuity();
    }
}
