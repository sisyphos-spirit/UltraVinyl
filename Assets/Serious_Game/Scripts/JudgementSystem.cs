using System;
using UnityEngine;

public class JudgementSystem : MonoBehaviour
{
    [SerializeField] private SongConductor conductor;
    [SerializeField] private LaneManager laneManager;
    [SerializeField] private NeedleController needle;

    private ComboSystem comboSystem = new ComboSystem();
    public ComboSystem ComboSystem => comboSystem;

    private int _lastJudgedBeat = -1;

    public event Action<JudgementResult> OnJudgement;

    public void EvaluateInput()
    {
        double currentBeat = conductor.CurrentBeat;

        int nearestBeat = Mathf.RoundToInt((float)currentBeat);

        if (nearestBeat == _lastJudgedBeat) return;

        double distance =
            Math.Abs(currentBeat - nearestBeat);

        JudgementType timing;

        if (distance <= 0.08)
            timing = JudgementType.Perfect;
        else if (distance <= 0.16)
            timing = JudgementType.Good;
        else if (distance <= 0.3)
            timing = JudgementType.Okay;
        else
            timing = JudgementType.Miss;

        bool correctLane =
            needle.CurrentLane ==
            laneManager.CurrentLane;

        JudgementResult result =
            new JudgementResult
            {
                Timing = timing,
                CorrectLane = correctLane,
                BeatIndex = nearestBeat
            };

        comboSystem.ProcessJudgement(result);
        OnJudgement?.Invoke(result);

        _lastJudgedBeat = nearestBeat;
    }
}