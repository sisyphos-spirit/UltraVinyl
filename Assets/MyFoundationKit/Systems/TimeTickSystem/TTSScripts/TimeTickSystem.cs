using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    [SerializeField] private const float TICK_TIMER_MAX = 0.2f; // Se puede alargar o incluso crear otros ticks de mayor duracion
    private TimeTicker _ticker = new TimeTicker();
    private float _timer;

    public void Initialize(TimeTicker ticker) //Bootstrapper crea el TimeTicker y lo pasa al sistema.
    {
        _ticker = ticker;
    }

    private void Update()
    {
        if (_ticker == null) return;

        _timer += Time.deltaTime;
        if (_timer >= TICK_TIMER_MAX)
        {
            _timer -= TICK_TIMER_MAX;
            _ticker.Tick();
        }
    }
}