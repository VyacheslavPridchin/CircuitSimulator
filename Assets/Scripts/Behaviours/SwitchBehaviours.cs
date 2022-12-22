using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBehaviours : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ElectricalComponentDragBehavior electricalComponentDragBehavior;
    [SerializeField]
    private ElectricalComponent electricalComponent;

    [SerializeField]
    private Sprite switchOn, switchOff;

    [SerializeField]
    private Image icon;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (electricalComponentDragBehavior.Locked) return;

        electricalComponent.ComponentEnabled = !electricalComponent.ComponentEnabled;

        if (electricalComponent.ComponentEnabled)
            icon.sprite = switchOn;
        else
            icon.sprite = switchOff;

        CircuitController.Instance.LateCheckCercuitContinuity();
    }
}
