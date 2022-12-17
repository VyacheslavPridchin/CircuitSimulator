using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<Point> RequiredPoints = new List<Point>();

    [SerializeField]
    private List<ElectricalComponent> RequiredElectricalComponentOrder = new List<ElectricalComponent>();

    [SerializeField]
    public static GameController Instance { get; private set; }

    public UnityEvent<bool> ChangeGameState { get; private set; } = new UnityEvent<bool>();

    public bool RequiredElectricalComponentOrderEqual
    {
        get => RequiredElectricalComponentOrder.SequenceEqual(CircuitController.Instance.CurrentElectricalComponentOrder);
    }

    private void Awake() => Instance = this;

    private void Start()
    {
        CircuitController.Instance.CheckCercuitComplete.AddListener(OnCheckCercuitComplete);
    }

    public void OnCheckCercuitComplete(bool isCercuitComplete)
    {
        //Invoke("CheckGameState", 0.05f);
        CheckGameState();
    }

    private void CheckGameState()
    {
        bool LevelCompleted = true;

        if (!CircuitController.Instance.IsCercuitComplete) LevelCompleted = false;

        foreach (Point point in RequiredPoints)
            if (!point.PointActivated) LevelCompleted = false;

        if (!RequiredElectricalComponentOrderEqual)
            LevelCompleted = false;

        if (LevelCompleted)
        {
            Debug.Log("U WIN!");
            GlobalLinksStorage.Instance.NextLevelButton.interactable = true;
            GlobalLinksStorage.Instance.UILineRenderer.color = new Color32(43, 192, 30, 255);
        }
        else
            GlobalLinksStorage.Instance.UILineRenderer.color = new Color32(255, 122, 0, 255);

        ChangeGameState?.Invoke(LevelCompleted);
    }
}
