using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool isMoveAllowed;
    Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        if (touch.phase == TouchPhase.Began)
        {
            isMoveAllowed = col == Physics2D.OverlapPoint(touchPosition);
        }
        else if (touch.phase == TouchPhase.Moved && isMoveAllowed)
        {
            transform.position = new Vector2(touchPosition.x, touchPosition.y);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            isMoveAllowed = false;
        }
    }
}
