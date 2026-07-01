using UnityEngine;

public struct JudgementResult
{
    public JudgementType Timing;
    public bool CorrectLane;
    public int BeatIndex;

    public bool IsSuccess =>
        CorrectLane &&
        Timing != JudgementType.Miss;
}
