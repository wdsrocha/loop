using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    Collider2D col;
    float maxDiff;
    float minDiff;

    Vector2 initialPosition;

    Vector2 polarOpposite;
    bool hasPassedHalfway = false;

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

        //Debug.Log($"({x},{y}); diff = {diff}");

        if (diff >= 2f) {
            //Debug.Log("Falhou");
		} else if (diff >= 1f) { 
            //Debug.Log("Mais ou menos");
		} else { 
            //Debug.Log("Perfeito");
		}

        if (touch.phase == TouchPhase.Began)
        {
            initialPosition = new Vector2(x, y);
            polarOpposite = new Vector2(-x, -y);
			Debug.Log("Touch phase began");
			Debug.Log($"polarOpposite = ({polarOpposite.x}, {polarOpposite.y})");
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            if (!hasPassedHalfway && Vector2.Distance(polarOpposite, touchPosition) <= 1f) {
                hasPassedHalfway = true;
                Debug.Log("passed halfway");
			}

            if (hasPassedHalfway && Vector2.Distance(touchPosition, initialPosition) <= 0.5f) {
                hasPassedHalfway = false;
                Debug.Log("Completed");
			}
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            //Debug.Log("Falhou");
        }
    }
}
