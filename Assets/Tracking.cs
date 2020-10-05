using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracking : MonoBehaviour
{
    float maxDiff;

    Vector2 initialPosition;

    Vector2 polarOpposite;
    bool hasPassedHalfway = false;

    public Text scoreText;
    int score = 0;

    public GameObject restartPanel;

    void Start() { }

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

        maxDiff = Mathf.Max(maxDiff, diff);

        if (diff >= 2f) {
            restartPanel.SetActive(true);
        }
		
		if (touch.phase == TouchPhase.Began)
        {
            initialPosition = new Vector2(x, y);
            polarOpposite = new Vector2(-x, -y);
			//Debug.Log("Touch phase began");
			//Debug.Log($"polarOpposite = ({polarOpposite.x}, {polarOpposite.y})");
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            if (!hasPassedHalfway && Vector2.Distance(polarOpposite, touchPosition) <= 1f) {
                hasPassedHalfway = true;
			}

            if (hasPassedHalfway && Vector2.Distance(touchPosition, initialPosition) <= 0.5f) {
                hasPassedHalfway = false;
                if (maxDiff >= 1f) {
                    score += 50;
				} else {
                    score += 100;
				}
                scoreText.text = $"Score: {score}";
			}
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            restartPanel.SetActive(true);
        }
    }
}
