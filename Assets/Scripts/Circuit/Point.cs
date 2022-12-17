using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Point : MonoBehaviour
{
    [field: SerializeField]
    public ElectricalComponent ElectricalComponent { get; private set; }
    [field: SerializeField]
    public ElectricalWire WireInput { get; set; }
    [field: SerializeField]
    public ElectricalWire WireOutput { get; set; }

    [field: SerializeField]
    public bool PointActivated { get; set; } = false;

    private void Start()
    {
        CircuitController.Instance.StartCheckCercuit.AddListener(() => PointActivated = false);
    }
}
