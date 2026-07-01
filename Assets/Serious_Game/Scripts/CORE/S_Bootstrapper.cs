using UnityEngine;

public class S_Bootstrapper : MonoBehaviour
{
    [SerializeField] private TimeTickSystem _timeTickSystem;

    private void Awake()
    {
        var _bootstrapper = new Bootstrapper(_timeTickSystem);

        _bootstrapper.Run();
    }
}
