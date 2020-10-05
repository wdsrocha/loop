using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomPatrol : MonoBehaviour
{
    public const float MIN_X = -7.8f;
    public const float MAX_X = 7.8f;
    public const float MIN_Y = -3.9f;
    public const float MAX_Y = 3.9f;
    public const float MIN_SPEED = 1f;
    public const float MAX_SPEED = 5f;

    public const float SECONDS_TO_MAX_DIFFICULTY = 60;

    public GameObject restartPanel;

    Vector2 targetPosition;

    void Start()
    {   
        targetPosition = GetRandomPosition();
        Time.timeScale = 1;
    }

    void Update()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            float speed = Mathf.Lerp(MIN_SPEED, MAX_SPEED, GetDifficultyPercent());
            transform.position = Vector2.MoveTowards(
                transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            targetPosition = GetRandomPosition();
        }
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(MIN_X, MAX_X);
        float randomY = Random.Range(MIN_Y, MAX_Y);
        return new Vector2(randomX, randomY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            Time.timeScale = 0;
            restartPanel.SetActive(true);
        }
    }

    float GetDifficultyPercent() {
        Debug.Log(Mathf.Clamp01(Time.timeSinceLevelLoad / SECONDS_TO_MAX_DIFFICULTY));
        return Mathf.Clamp01(Time.timeSinceLevelLoad / SECONDS_TO_MAX_DIFFICULTY);
    }
}
