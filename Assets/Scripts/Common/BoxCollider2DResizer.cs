using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(RectTransform))]
public class BoxCollider2DResizer : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private RectTransform rectTransform;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        boxCollider2D.size = rectTransform.rect.size;
    }
}
