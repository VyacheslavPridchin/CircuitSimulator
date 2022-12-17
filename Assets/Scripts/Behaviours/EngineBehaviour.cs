using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<Point> EngineRequiredPoints = new List<Point>();
    [SerializeField]
    private Point activateUpPoint;
    [SerializeField]
    private Point activateDownPoint;

    [SerializeField]
    private Slider slider;

    private bool activateUp = false;
    private bool activateDown = false;

    void Start()
    {
        CircuitController.Instance.CheckCercuitComplete.AddListener(ChangeBehaviour);
        //GameController.Instance.ChangeGameState.AddListener(ChangeBehaviour);
    }

    public void ChangeBehaviour(bool activate)
    {
        activateUp = false;
        activateDown = false;

        if (activate)
            foreach (Point point in EngineRequiredPoints)
            {
                if (!point.PointActivated)
                    activate = false;
            }

        if (!GameController.Instance.RequiredElectricalComponentOrderEqual) activate = false;

        if (activate && activateUpPoint.PointActivated) activateUp = true;
        if (activate && activateDownPoint.PointActivated) activateDown = true;
    }

    private void FixedUpdate()
    {
        if (activateUp)
            if (slider.value > slider.minValue)
                slider.value -= 1;

        if (activateDown)
            if (slider.value < slider.maxValue)
                slider.value += 1;
    }
}
