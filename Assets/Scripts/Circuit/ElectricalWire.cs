using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalWire : MonoBehaviour
{
    [field: SerializeField]
    public Point Input { get; set; }

    [field: SerializeField]
    public Point Output { get; set; }

    private void Start()
    {
        WireController.Instance.AddWire(this);
    }

    private void OnDestroy()
    {
        WireController.Instance.RemoveWire(this);
    }
}
