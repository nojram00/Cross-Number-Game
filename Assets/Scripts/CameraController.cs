using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public Transform CameraTransform;
    public float moveSpeed;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private void Update()
    {
        if (Application.isMobilePlatform) { AndroidTouch(); } else { MouseMove(); }
    }

    void MouseMove()
    {
        Vector3 worldPos;
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            worldPos = Camera.main.ScreenToWorldPoint(mousePos + Vector3.forward * -10);

            Vector3 targetPos = worldPos;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
        }
        if (Input.GetMouseButtonUp(0))
        {
            worldPos.z = -10;
        }
    }

    void AndroidTouch()
    {
        Vector3 worldPos;
        if(Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, -10f);

            worldPos = Camera.main.ScreenToWorldPoint(touchPos);
            Vector3 targetPos = worldPos;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
        }
        else
        {
            return;
        }
    }

    private void LateUpdate()
    {
        Vector2 newPos = transform.position;

        newPos.x = Mathf.Clamp(newPos.x, minPosition.x, maxPosition.x);
        newPos.y = Mathf.Clamp(newPos.y, minPosition.y, maxPosition.y);

        transform.position = new Vector3(newPos.x, newPos.y, -10f);
        
    }
}
