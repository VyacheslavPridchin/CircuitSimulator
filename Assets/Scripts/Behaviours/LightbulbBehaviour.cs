using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightbulbBehaviour : MonoBehaviour
{
    [SerializeField]
    private Sprite lightbulbOff;
    [SerializeField]
    private Sprite lightbulbOn;
    [SerializeField]
    private Image componentIcon;
    [SerializeField]
    private List<Point> lightbulbRequiredPoints = new List<Point>();

    void Start()
    {
        //CircuitController.Instance.CheckCercuitComplete.AddListener(ChangeBehaviour);
        GameController.Instance.ChangeGameState.AddListener(ChangeBehaviour);
    }

    public void ChangeBehaviour(bool activate)
    {
        if (activate)
            foreach (Point point in lightbulbRequiredPoints)
            {
                if (!point.PointActivated)
                    activate = false;
            }

        if (activate)
            componentIcon.sprite = lightbulbOn;
        else
            componentIcon.sprite = lightbulbOff;
    }
}
