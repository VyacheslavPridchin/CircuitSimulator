using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class ElectricalComponentDragBehavior : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform myRectTransform;
    [SerializeField]
    private List<GameObject> HideElements;
    [SerializeField]
    private Image Outline;
    private Vector2 lastAnchoredPosition;
    private bool lockedWithAnimate  = false;
    public bool Locked { get; private set; } = false;

    private void Start()
    {
        foreach (GameObject go in HideElements)
            go.SetActive(false);

        Outline.enabled = false;

        Invoke("CheckWorkspace", 0.05f);
    }

    private void CheckWorkspace()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Workspace")
            {
                foreach (GameObject go in HideElements)
                    go.SetActive(true);

                Outline.enabled = true;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (lockedWithAnimate) return;

        Vector2 newPosition = myRectTransform.anchoredPosition + eventData.delta / myRectTransform.lossyScale * myRectTransform.localScale;
        myRectTransform.anchoredPosition = newPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Locked = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Invoke("Unlock", 0.1f);
    }

    private void Unlock()
    {
        Locked = false;
    }

    private Transform prevParent;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (lockedWithAnimate) return;

        lastAnchoredPosition = myRectTransform.anchoredPosition;
        prevParent = myRectTransform.parent;
        myRectTransform.SetParent(GlobalLinksStorage.Instance.CanvasRectTransform);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (lockedWithAnimate) return;

        LayerMask mask = LayerMask.GetMask("ActiveZone");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1, mask);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.tag);

            if (hit.collider.tag == "Workspace")
            {
                lastAnchoredPosition = myRectTransform.position;
                myRectTransform.SetParent(GlobalLinksStorage.Instance.WorkspaceArea);
                myRectTransform.localScale = Vector3.one;

                foreach (GameObject go in HideElements)
                    go.SetActive(true);

                Outline.enabled = true;
            }
            else
            if (hit.collider.tag == "Pocket")
            {
                lastAnchoredPosition = myRectTransform.position;
                myRectTransform.SetParent(hit.collider.transform);
                myRectTransform.localScale = Vector3.one;
                myRectTransform.localPosition = Vector3.zero;

                foreach (GameObject go in HideElements)
                    go.SetActive(false);

                Outline.enabled = false;
            }
            else
            {
                if (myRectTransform.parent != GlobalLinksStorage.Instance.WorkspaceArea)
                    myRectTransform.SetParent(prevParent);

                lockedWithAnimate = true;
                myRectTransform.DOAnchorPos(lastAnchoredPosition, 0.3f).OnComplete(() => { lockedWithAnimate = false; });
            }
        }
        else
        {
            if (myRectTransform.parent != GlobalLinksStorage.Instance.WorkspaceArea)
                myRectTransform.SetParent(prevParent);

            lockedWithAnimate = true;
            myRectTransform.DOAnchorPos(lastAnchoredPosition, 0.3f).OnComplete(() => { lockedWithAnimate = false; });
        }
    }
}
