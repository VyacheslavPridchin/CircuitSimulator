using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalComponent : MonoBehaviour
{
    [field: SerializeField]
    public List<Point> Points { get; private set; } = new List<Point>();

    private UnityEvent ActivateComponent = new UnityEvent();

    private void Awake()
    {
        ActivateComponent.AddListener(OnActivateComponent);
    }

    public virtual void OnActivateComponent()
    {
        Debug.Log($"Electrical component '{gameObject.name}' activated");
    }
}
