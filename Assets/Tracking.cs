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
    public Text helpText;
    int score = 0;

    bool isStopped = false;

    public GameObject restartPanel;

    //public GameObject effect;
    //List<GameObject> trail;

    string getTextScore(int points) {
        return $"Score: {points:0000}";
	}

    void Start() {
        scoreText.text = getTextScore(0);
	 }

    void Update()
    {
        if (Input.touchCount <= 0 || isStopped)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        float x = touchPosition.x;
        float y = touchPosition.y;
        float r = 2f;

        float diff = Mathf.Abs(x * x + y * y - r * r);

        maxDiff = Mathf.Max(maxDiff, diff);

        if (diff >= 2f) {
            restartPanel.SetActive(true);
        }
		
		if (touch.phase == TouchPhase.Began)
        {
            initialPosition = new Vector2(x, y);
            polarOpposite = new Vector2(-x, -y);
            helpText.text = "";
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            if (!hasPassedHalfway && Vector2.Distance(polarOpposite, touchPosition) <= 1f) {
                hasPassedHalfway = true;
			}

            if (hasPassedHalfway && Vector2.Distance(touchPosition, initialPosition) <= 0.5f) {
                hasPassedHalfway = false;
                if (maxDiff >= 1f) {
                    score += 1;
				} else {
                    score += 5;
				}
                scoreText.text = getTextScore(score);
			}
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            restartPanel.SetActive(true);
        }
    }
}
