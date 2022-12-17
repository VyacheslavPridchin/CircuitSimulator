using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class WireController : MonoBehaviour
{
    [SerializeField]
    private UILineConnector UILineConnector;
    public static WireController Instance { get; private set; }
    private void Awake() => Instance = this;

    public void AddWire(ElectricalWire electricalWire)
    {
        UILineConnector.transforms.Add(electricalWire.Input.transform as RectTransform);
        UILineConnector.transforms.Add(electricalWire.Output.transform as RectTransform);

        electricalWire.Input.WireOutput = electricalWire;
        electricalWire.Output.WireInput = electricalWire;
    }

    public void RemoveWire(ElectricalWire electricalWire)
    {
        if (electricalWire.Input == null || electricalWire.Output == null) return;

        for (int i = 0; i < UILineConnector.transforms.Count - 1; i++)
        {
            if (i % 2 == 0)
                if (UILineConnector.transforms[i] == (electricalWire.Input.transform as RectTransform))
                    if (UILineConnector.transforms[i + 1] == (electricalWire.Output.transform as RectTransform))
                    {
                        UILineConnector.transforms.RemoveAt(i);
                        UILineConnector.transforms.RemoveAt(i);
                        break;
                    }
        }

        electricalWire.Input.WireOutput = null;
        electricalWire.Output.WireInput = null;
    }
}
