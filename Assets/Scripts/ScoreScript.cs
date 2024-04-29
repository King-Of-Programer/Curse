using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GameObject.Find("GameScoreText")
            .GetComponent<TMPro.TextMeshProUGUI>();
        GameState.Subscribe(OnGameStateChange);
    }

    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChange);
    }
    private void OnGameStateChange(string propertyName)
    {
        if (propertyName == nameof(GameState.Score))
        {
            scoreText.text = GameState.Score.ToString("0000.0");
        }
    }
    void Update()
    {
        
    }
}
