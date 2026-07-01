using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ComboFeedbackUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private JudgementSystem judgementSystem;

    [Header("Animation")]
    [SerializeField] private float punchSpeed = 10f;
    [SerializeField] private float punchScale = 1.2f;

    [Header("Combo Colors")]
    [SerializeField] private Color lowComboColor = Color.grey;
    [SerializeField] private Color mediumComboColor = Color.green;
    [SerializeField] private Color highComboColor = Color.yellow;
    [SerializeField] private Color maxComboColor = new Color(1f, 0.5f, 0f);

    private Vector3 defaultScale;
    private float currentTargetScale = 1f;

    private void OnEnable()
    {
        judgementSystem.ComboSystem.OnComboChanged += ShowCombo;
        judgementSystem.ComboSystem.OnComboBroken += HideCombo;
    }

    private void OnDisable()
    {
        judgementSystem.ComboSystem.OnComboChanged -= ShowCombo;
        judgementSystem.ComboSystem.OnComboBroken -= HideCombo;
    }

    private void Awake()
    {
        defaultScale = comboText.rectTransform.localScale;
        comboText.gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 target = defaultScale * currentTargetScale;

        comboText.rectTransform.localScale =
            Vector3.Lerp(
                comboText.rectTransform.localScale,
                target,
                Time.deltaTime * punchSpeed);

        currentTargetScale =
            Mathf.Lerp(
                currentTargetScale,
                1f,
                Time.deltaTime * punchSpeed);
    }

    public void ShowCombo(int combo)
    {
        if (combo <= 0)
        {
            comboText.gameObject.SetActive(false);
            return;
        }

        comboText.gameObject.SetActive(true);

        comboText.text = $"COMBO x{combo}";
        comboText.color = GetComboColor(combo);

        currentTargetScale = combo >= 30
                            ? 1.35f
                            : combo >= 20
                                ? 1.30f
                                : combo >= 10
                                    ? 1.25f
                                    : 1.20f;
    }

    public void HideCombo()
    {
        comboText.gameObject.SetActive(false);
    }

    private Color GetComboColor(int combo)
    {
        if (combo >= 30)
            return maxComboColor;

        if (combo >= 20)
            return highComboColor;

        if (combo >= 10)
            return mediumComboColor;

        return lowComboColor;
    }
}