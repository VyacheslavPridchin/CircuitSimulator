using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GlobalLinksStorage : MonoBehaviour
{
    #region Global links
    [field: Header("Global links")]
    [field: SerializeField]
    public UnityEngine.UI.CanvasScaler CanvasScaler { get; private set; }

    [field: SerializeField]
    public Transform WorkspaceArea { get; private set; }

    [field: SerializeField]
    public Transform WiresStorage { get; private set; }
    #endregion
    [field: SerializeField]
    public RectTransform CanvasRectTransform { get; private set; }

    [field: SerializeField]
    public UILineRenderer UILineRenderer { get; private set; }

    [field: SerializeField]
    public Button NextLevelButton { get; private set; }
    public static GlobalLinksStorage Instance { get; private set; }
    private void Awake() => Instance = this;
}
