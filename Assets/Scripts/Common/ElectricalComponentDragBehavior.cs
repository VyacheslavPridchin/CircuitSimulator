using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class ElectricalComponentDragBehavior : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private RectTransform myRectTransform;
    [SerializeField]
    private List<GameObject> HideElements;
    [SerializeField]
    private Image Outline;
    private Vector2 lastAnchoredPosition;
    private bool locked = false;

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
        if (locked) return;

        Vector2 newPosition = myRectTransform.anchoredPosition + eventData.delta / transform.parent.lossyScale;
        myRectTransform.anchoredPosition = newPosition;
    }

    private Transform prevParent;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (locked) return;

        lastAnchoredPosition = myRectTransform.anchoredPosition;

        prevParent = myRectTransform.parent;
        myRectTransform.SetParent(GlobalLinksStorage.Instance.CanvasRectTransform);

        //Vector2 newPosition = myRectTransform.anchoredPosition;
        //newPosition.y += 50f;
        //myRectTransform.anchoredPosition = newPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (locked) return;

        LayerMask mask = LayerMask.GetMask("ActiveZone");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1, mask);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.tag);

            if (hit.collider.tag == "Workspace")
            {
                lastAnchoredPosition = myRectTransform.position;
                //if (transform.parent.tag != "Workspace") Destroy(transform.parent.gameObject);
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
                //if (transform.parent.tag != "Workspace") Destroy(transform.parent.gameObject);
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
                locked = true;
                myRectTransform.DOAnchorPos(lastAnchoredPosition, 0.3f).OnComplete(() => { locked = false; });
            }
        }
        else
        {
            if (myRectTransform.parent != GlobalLinksStorage.Instance.WorkspaceArea)
                myRectTransform.SetParent(prevParent);
            locked = true;
            myRectTransform.DOAnchorPos(lastAnchoredPosition, 0.3f).OnComplete(() => { locked = false; });
        }
    }
}
