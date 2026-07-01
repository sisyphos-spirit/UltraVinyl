using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private const int BASE_SCORE = 10;

    private ComboSystem comboSystem;
    [SerializeField] private JudgementSystem judgementSystem;
    [SerializeField] private SpeedController speedController;

    public int TotalScore { get; private set; }

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        comboSystem = judgementSystem.ComboSystem;
    }
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
        if (!result.IsSuccess)
            return;

        float timingMultiplier = GetTimingMultiplier(result.Timing);

        float comboMultiplier =
            GetComboMultiplier(comboSystem.CurrentCombo);

        float speedMultiplier =
            speedController != null
                ? speedController.ScoreMultiplier
                : 1f;

        float finalScore =
            BASE_SCORE *
            timingMultiplier *
            comboMultiplier *
            speedMultiplier;

        TotalScore += Mathf.RoundToInt(finalScore);

        OnScoreChanged?.Invoke(TotalScore);
    }

    private float GetTimingMultiplier(
        JudgementType judgement)
    {
        switch (judgement)
        {
            case JudgementType.Perfect:
                return 2f;

            case JudgementType.Good:
                return 1f;

            case JudgementType.Okay:
                return 0.5f;

            default:
                return 0f;
        }
    }

    private float GetComboMultiplier(int combo)
    {
        if (combo >= 30)
            return 4f;

        if (combo >= 20)
            return 3f;

        if (combo >= 10)
            return 2f;

        return 1f;
    }
}
