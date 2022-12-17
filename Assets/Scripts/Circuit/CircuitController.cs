using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CircuitController : MonoBehaviour
{
    [field: SerializeField]
    public ElectricitySource ElectricitySource { get; private set; }
    public static CircuitController Instance { get; private set; }
    private void Awake() => Instance = this;

    public UnityEvent StartCheckCercuit { get; private set; } = new UnityEvent();

    public UnityEvent<bool> CheckCercuitComplete { get; private set; } = new UnityEvent<bool>();

    public bool IsCercuitComplete { get; private set; } = false;

    [field: SerializeField]
    public List<ElectricalComponent> CurrentElectricalComponentOrder { get; private set; } = new List<ElectricalComponent>();


    private List<ElectricalWire> alreadyCheckedWires = new List<ElectricalWire>();

    public void LateCheckCercuitContinuity()
    {
        Invoke("CheckCercuitContinuity", 0.05f);
    }

    private void CheckCercuitContinuity()
    {
        CurrentElectricalComponentOrder.Clear();

        StartCheckCercuit?.Invoke();

        Point outPoint = ElectricitySource.Points.LastOrDefault(x => x.WireOutput != null);

        if (outPoint != null)
        {
            ElectricitySource electricitySource = null;

            GetElectricitySource(outPoint, out electricitySource, isStart: true);

            bool isCercuitComplete = electricitySource == ElectricitySource;

            IsCercuitComplete = isCercuitComplete;
        }
        else
        {
            IsCercuitComplete = false;
        }

        if (IsCercuitComplete)
            foreach (ElectricalWire wire in alreadyCheckedWires)
            {
                if (wire == null) continue;

                if (wire.Input != null)
                    wire.Input.PointActivated = true;

                if (wire.Output != null)
                    wire.Output.PointActivated = true;
            }

        alreadyCheckedWires.Clear();

        CurrentElectricalComponentOrder.Reverse();

        CheckCercuitComplete?.Invoke(IsCercuitComplete);
    }

    bool GetElectricitySource(Point point, out ElectricitySource electricitySource, bool isStart = false)
    {
        if (!point.ElectricalComponent.ComponentEnabled)
        {
            electricitySource = null;
            return false;
        }

        if (!isStart)
            if (point.ElectricalComponent.GetType() == typeof(ElectricitySource))
            {
                electricitySource = (ElectricitySource)point.ElectricalComponent;
                return true;
            }

        if (point.WireOutput != null)
        {
            if (GetElectricitySource(point.WireOutput, out electricitySource))
            {
                return true;
            }
        }

        foreach (Point p in point.ElectricalComponent.Points)
        {
            try
            {
                if (!alreadyCheckedWires.Contains(p.WireOutput))
                    if (GetElectricitySource(p.WireOutput, out electricitySource))
                    {
                        return true;
                    }

                //if (!alreadyCheckedWires.Contains(p.WireInput))
                //    if (GetElectricitySource(p.WireInput, out electricitySource))
                //    {
                //        return true;
                //    }
            }
            catch { Debug.Log("Stack overflow"); }
        }

        electricitySource = null;
        return false;
    }

    private void AddElectricalComponentInOrder(ElectricalComponent electricalComponent)
    {
        if (electricalComponent.GetType() != typeof(ElectricitySource))
            if (CurrentElectricalComponentOrder.LastOrDefault() != electricalComponent)
                CurrentElectricalComponentOrder.Add(electricalComponent);
    }

    bool GetElectricitySource(ElectricalWire electricalWire, out ElectricitySource electricitySource)
    {
        if (alreadyCheckedWires.Contains(electricalWire))
        {
            electricitySource = null;
            return false;
        }

        alreadyCheckedWires.Add(electricalWire);

        if (electricalWire != null)
            if (electricalWire.Output != null)
            {
                bool result = GetElectricitySource(electricalWire.Output, out electricitySource);
                if (result) AddElectricalComponentInOrder(electricalWire.Output.ElectricalComponent);
                return result;
            }

        electricitySource = null;
        return false;
    }
}
