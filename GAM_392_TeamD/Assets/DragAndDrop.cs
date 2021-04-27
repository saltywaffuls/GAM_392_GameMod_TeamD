using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler
{
    //Reference game canvas when dragging
    [SerializeField] private Canvas canvas;

    //Move Objects
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }




    //Begin Dragging
  public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
    } 
    
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }


    //Release Mouse
  public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
    }  
    

    //Click Mouse down
  public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }


}
