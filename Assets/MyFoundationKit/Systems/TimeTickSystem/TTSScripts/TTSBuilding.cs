using UnityEngine;
using UnityEngine.UI;

public class TTSBuilding : MonoBehaviour, ITickable
{
    private int buildTick;
    private bool isConstructing;

    [SerializeField] private GameObject sliderObject;
    private Slider slider;

    private void Awake()
    {
        slider = sliderObject.GetComponent<Slider>();
        buildTick = 0;
        slider.maxValue = 10;
        isConstructing = true;
    }

    public void Tick()
    {
        if (isConstructing)
        {
            buildTick += 1;
            slider.value = buildTick;
            if (buildTick >= slider.maxValue)
            {
                isConstructing = false;
                sliderObject.SetActive(false);
                GetComponent<SpriteRenderer>().color = Color.brown;
            }
        }
    }
    //Hace falta el bootstraper para que esto funcione.
}
