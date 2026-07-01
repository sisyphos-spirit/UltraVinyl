using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.CullingGroup;

public class CameraMove : MonoBehaviour
{
    //Servicios
    private EventBinding<S_PlayingEvent> _eventBinding;
    private S_StateChanger _stateChanger;
    [SerializeField] private SongConductor _songConductor;
    [SerializeField] private MenuVinyl _pauseMenu;

    [SerializeField] private Transform[] _positions;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speed = 2f;

    private int _currentIndex = 0;
    private Transform _targetPos;
    private bool _isPlaying = false;

    void OnEnable()
    {
        _eventBinding = new EventBinding<S_PlayingEvent>(HandlePlayingEvent);
        EventBus<S_PlayingEvent>.Register(_eventBinding);
    }

    void OnDisable()
    {
        EventBus<S_PlayingEvent>.Deregister(_eventBinding);
    }

    private void HandlePlayingEvent(S_PlayingEvent playingEvent)
    {
        _isPlaying = playingEvent.isPlaying;
        if (_isPlaying)
        {
            _currentIndex = 1;
            _targetPos = _positions[_currentIndex];
        }else
        {
            _currentIndex = 2;
            _targetPos = _positions[_currentIndex];
        }
    }

    private void Start()
    {
        _stateChanger = Core.Services.Get<S_StateChanger>();
        _targetPos = transform;
    }

    private void Update()
    {
        if (_playerInput.actions["escape"].WasPressedThisFrame())
        {
            if (_currentIndex == 1)
            {
                _songConductor.PauseSong();
                _stateChanger.Pause();
                _pauseMenu.SetMenuActive(true);
            }
            else if (_currentIndex == 2)
            {
                _songConductor.ResumeSong();
                _stateChanger.Play();
                _pauseMenu.SetMenuActive(false);
            }
        }


        transform.position = Vector3.Slerp(transform.position, _targetPos.position, Time.deltaTime * _speed);
    }
}
