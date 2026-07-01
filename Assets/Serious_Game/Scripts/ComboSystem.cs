using System;
using UnityEngine;

public class ComboSystem
{
    public int CurrentCombo { get; private set; }
    public int MaxCombo { get; private set; }

    public event Action<int> OnComboChanged;
    public event Action OnComboBroken;

    public void AddCombo()
    {
        CurrentCombo++;

        OnComboChanged?.Invoke(CurrentCombo);
    }

    public void ResetCombo()
    {
        if (CurrentCombo <= 0)
            return;

        CurrentCombo = 0;

        OnComboBroken?.Invoke();
        OnComboChanged?.Invoke(CurrentCombo);
    }

    public void ProcessJudgement(JudgementResult result)
    {
        if (result.IsSuccess)
        {
            AddCombo();
            if (CurrentCombo > MaxCombo)
            {
                MaxCombo = CurrentCombo;
            }
        }
        else
            ResetCombo();
    }
}
