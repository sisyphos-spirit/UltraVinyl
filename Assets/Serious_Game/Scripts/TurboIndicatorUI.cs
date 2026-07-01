using TMPro;
using UnityEngine;


public class TurboIndicatorUI : MonoBehaviour
{
    [SerializeField]
    private SpeedController speedController;


    [SerializeField]
    private TextMeshProUGUI text;


    private void OnEnable()
    {
        speedController.OnStateChanged += UpdateState;

        UpdateState(
            speedController.CurrentState
        );
    }


    private void OnDisable()
    {
        speedController.OnStateChanged -= UpdateState;
    }


    private void UpdateState(SpeedState state)
    {
        switch (state)
        {
            case SpeedState.Normal:

                text.gameObject.SetActive(false);

                break;


            case SpeedState.Accelerating:

                text.gameObject.SetActive(true);

                text.text = "VERY SERIOUS MODE";
                text.color = Color.cyan;

                break;


            case SpeedState.Overheated:

                text.gameObject.SetActive(true);

                text.text = "OVERHEAT!";
                text.color = Color.red;

                break;


            case SpeedState.Recovery:

                text.gameObject.SetActive(true);

                text.text = "NOT SO SERIOUS :(";
                text.color = Color.yellow;

                break;
        }
    }
}