using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PumpBehaviour : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private bool active = false;

    void Start()
    {
        //CircuitController.Instance.CheckCercuitComplete.AddListener(ChangeBehaviour);
        GameController.Instance.ChangeGameState.AddListener(ChangeBehaviour);
    }

    public void ChangeBehaviour(bool activate)
    {
        active = activate;
    }

    private void FixedUpdate()
    {
        if (active)
            if (slider.value > slider.minValue)
                slider.value -= 1;
    }
}
