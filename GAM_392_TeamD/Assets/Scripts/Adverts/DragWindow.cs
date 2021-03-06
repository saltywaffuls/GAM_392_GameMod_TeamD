using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;

public class DragWindow : MonoBehaviour
{
    private Vector3 prevMousePos;
    private Vector3 prevLocation;

    public GameObject controllerObject;
    AdvertController controller;

    public void BeginDrag()
    {
        prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        prevLocation = gameObject.transform.position;
        controller = controllerObject.GetComponent<AdvertController>();
        UpdateOrder();
    }

    public void Drag(SpriteRenderer spriteRender)
    {
        #region Get Position Change
        //Get the change in position (Relative to the previous location(s))
        Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 deltaPos = prevLocation + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - prevMousePos);
        #endregion

        #region Clamp Position
        //Adjust the max distance the window can be moved
        float x_max = spriteRender.sprite.rect.width + 2f;
        float y_max = spriteRender.sprite.rect.height + 11f;

        float x_pos = Mathf.Round(Mathf.Clamp(deltaPos.x, 100.0f, 480.0f - x_max));
        float y_pos = Mathf.Round(Mathf.Clamp(deltaPos.y, -270.0f + y_max + 12, 0.0f));
        #endregion

        //Update the positon
        gameObject.transform.position = new Vector3(x_pos, y_pos, deltaPos.z);
        UpdateOrder();
    }

    private void UpdateOrder()
    {
        controller.UpdateOrder(gameObject);
    }
}
