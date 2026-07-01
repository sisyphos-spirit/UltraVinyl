using UnityEngine;
using UnityEngine.UI;


public class HeatBarUI : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;


    [SerializeField]
    private SpeedController speedController;


    private void OnEnable()
    {
        speedController.OnHeatChanged += UpdateHeat;

        UpdateHeat(speedController.CurrentHeat);
    }


    private void OnDisable()
    {
        speedController.OnHeatChanged -= UpdateHeat;
    }


    private void UpdateHeat(float heat)
    {
        fillImage.fillAmount = heat;
    }
}