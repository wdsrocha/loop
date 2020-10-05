using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
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

        float x = touchPosition.x;
        float y = touchPosition.y;
        float r = 2f;

        float diff = Mathf.Abs(x * x + y * y - r * r);

        Debug.Log($"({x},{y}); diff = {diff}");

        if (diff >= 2f) {
            Debug.Log("Falhou");
		} else if (diff >= 1f) { 
            Debug.Log("Mais ou menos");
		} else { 
            Debug.Log("Perfeito");
		}

        if (touch.phase == TouchPhase.Began)
        {
        }
        else if (touch.phase == TouchPhase.Moved)
        {
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            //Debug.Log("Falhou");
        }
    }
}
