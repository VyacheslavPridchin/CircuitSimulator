using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject descriptionPanel;

    public void ShowDescriptionPanel() => descriptionPanel.SetActive(true);

    public void HideDescriptionPanel() => descriptionPanel.SetActive(false);

    private void Start()
    {
        ShowDescriptionPanel();
    }
}
