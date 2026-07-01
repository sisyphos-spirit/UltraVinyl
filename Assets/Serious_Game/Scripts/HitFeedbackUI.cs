using TMPro;
using UnityEngine;

public class HitFeedbackUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private JudgementSystem judgementSystem;

    [Header("Timing")]
    [SerializeField] private float visibleDuration = 0.6f;

    [Header("Colors")]
    [SerializeField] private Color perfectColor = Color.green;
    [SerializeField] private Color goodColor = Color.cyan;
    [SerializeField] private Color okayColor = Color.yellow;
    [SerializeField] private Color missColor = Color.red;

    private void OnEnable()
    {
        judgementSystem.OnJudgement += HandleJudgement;
    }

    private void OnDisable()
    {
        judgementSystem.OnJudgement -= HandleJudgement;
    }

    private void HandleJudgement(JudgementResult result)
    {
        if (!result.CorrectLane) return;

        ShowJudgement(result.Timing);
    }

    private float timer;
    private bool isShowing;

    private void Awake()
    {
        SetAlpha(0f);
    }

    private void Update()
    {
        if (!isShowing)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SetAlpha(0f);
            isShowing = false;
            return;
        }

        float alpha = timer / visibleDuration;
        SetAlpha(alpha);
    }

    public void ShowJudgement(JudgementType rating)
    {
        feedbackText.text = GetLabel(rating);
        feedbackText.color = GetColor(rating);

        timer = visibleDuration;
        isShowing = true;

        SetAlpha(1f);
    }

    private string GetLabel(JudgementType rating)
    {
        return rating switch
        {
            JudgementType.Perfect => "PERFECT",
            JudgementType.Good => "GOOD",
            JudgementType.Okay => "OKAY",
            JudgementType.Miss => "MISS",
            _ => string.Empty
        };
    }

    private Color GetColor(JudgementType rating)
    {
        return rating switch
        {
            JudgementType.Perfect => perfectColor,
            JudgementType.Good => goodColor,
            JudgementType.Okay => okayColor,
            JudgementType.Miss => missColor,
            _ => Color.white
        };
    }

    private void SetAlpha(float alpha)
    {
        Color c = feedbackText.color;
        c.a = alpha;
        feedbackText.color = c;
    }
}