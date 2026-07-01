using UnityEngine;

public class SandboxBootstrapper : MonoBehaviour
{
    [SerializeField] private TimeTickSystem _timeTickSystem;

    private void Awake()
    {
        var _bootstrapper = new Bootstrapper(_timeTickSystem);

        _bootstrapper.Run();
    }

}
