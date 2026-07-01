using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private RectTransform scoreTransform;

    private void OnEnable()
    {
        scoreSystem.OnScoreChanged += UpdateScore;

        UpdateScore(scoreSystem.TotalScore);
    }

    private void OnDisable()
    {
        scoreSystem.OnScoreChanged -= UpdateScore;
    }

    private void Update()
    {
        scoreTransform.localScale =
        Vector3.Lerp(
            scoreTransform.localScale,
            Vector3.one,
            10f * Time.deltaTime
        );
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "SCORE: " + score.ToString("000000");
        scoreTransform.localScale = Vector3.one * 1.15f;
    }
}