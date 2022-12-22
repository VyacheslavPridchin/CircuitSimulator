using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RotateBehaviour : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ElectricalComponentDragBehavior electricalComponentDragBehavior;
    [SerializeField]
    private RectTransform Icon;

    private bool locked = false;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (locked || electricalComponentDragBehavior.Locked) return;


        clicked++;
        if (clicked == 1) clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            OnPointerDoubleClick();
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
    }

    public void OnPointerDoubleClick()
    {
        locked = true;
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 rotate = rectTransform.localRotation.eulerAngles;
        rotate.z += 90f;
        rectTransform.DOLocalRotate(rotate, 0.5f, RotateMode.Fast).OnComplete(() => locked = false);

        if (Icon != null)
            Icon.DORotate(Vector3.zero, 0.5f, RotateMode.Fast);
    }

}
