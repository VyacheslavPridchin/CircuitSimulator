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

    //public void OnCheckCercuitComplete(bool isCercuitComplete)
    //{
    //    if (ElectricalComponent.GetType() != typeof(ElectricitySource))
    //    {
    //        try
    //        {
    //            ElectricitySource electricitySourceForward = GetElectricitySourceForward(this);
    //            ElectricitySource electricitySourceBackward = GetElectricitySourceBackward(this);

    //            PointActivated = CircuitController.Instance.IsCercuitComplete && electricitySourceForward == electricitySourceBackward;
    //        }
    //        catch
    //        {
    //            PointActivated = false;
    //        }
    //    }
    //}

    //ElectricitySource GetElectricitySourceForward(Point point)
    //{
    //    if (point.ElectricalComponent.GetType() == typeof(ElectricitySource)) return (ElectricitySource)point.ElectricalComponent;

    //    if (point.WireOutput != null)
    //        return GetElectricitySourceForward(point.WireOutput);

    //    return null;
    //}

    //ElectricitySource GetElectricitySourceForward(ElectricalWire electricalWire)
    //{
    //    if (electricalWire.Output != null)
    //        return GetElectricitySourceForward(electricalWire.Output);

    //    return null;
    //}

    //ElectricitySource GetElectricitySourceBackward(Point point)
    //{
    //    if (point.ElectricalComponent.GetType() == typeof(ElectricitySource)) return (ElectricitySource)point.ElectricalComponent;

    //    if (point.WireInput != null)
    //        return GetElectricitySourceBackward(point.WireInput);

    //    return null;
    //}

    //ElectricitySource GetElectricitySourceBackward(ElectricalWire electricalWire)
    //{
    //    if (electricalWire.Input != null)
    //        return GetElectricitySourceBackward(electricalWire.Input);

    //    return null;
    //}
}
