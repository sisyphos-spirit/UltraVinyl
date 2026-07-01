using UnityEngine;

public class SandboxTimeDebugger : MonoBehaviour, ITickable
{
    private TimeTicker _timeTicker;

    private void Start()
    {
        _timeTicker = Core.Services.Get<TimeTicker>();
        _timeTicker.Register(this);
    }

    public void Tick()
    {
        Logger.Time("Tick " + _timeTicker.CurrentTick, this);
    }
}
