using System;
using UnityEngine;

/*public class SandboxPlayerController : MonoBehaviour
{
    // Servicios
    private SandboxStateChanger _stateChanger;
    private SaveManager _saveManager;
    private EventBinding<PlayingEvent> _playingEventBinding;

    //Variables de escena
    private PlayerSystem _playerSystem;
    [SerializeField] private float _speed = 6f;
    private bool _isPlaying = true;

    void OnEnable()
    {
        _playingEventBinding = new EventBinding<PlayingEvent>(HandlePlayingEvent);
        EventBus<PlayingEvent>.Register(_playingEventBinding);
    }

    void OnDisable()
    {
        EventBus<PlayingEvent>.Deregister(_playingEventBinding);
    }

    private void Start()
    {
        _saveManager = Core.Services.Get<SaveManager>();
        _stateChanger = Core.Services.Get<SandboxStateChanger>();
        _playerSystem = new PlayerSystem(transform);
        _saveManager.Register(_playerSystem);

        if (!_saveManager.Load("slot_1"))
        {
            Logger.Save("No existe partida guardada", this);
            _saveManager.Save("slot_1");
        }
    }
    private void Update()
    {
        if (_isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _stateChanger.Pause();
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector2.up * _speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector2.down * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector2.left * _speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector2.right * _speed * Time.deltaTime);
            }

        } else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _stateChanger.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Core.Services.Get<SaveManager>().Save("slot_1"); //Tengo que crear el slot primero de alguna forma
            Application.Quit();
        }
    }

    private void HandlePlayingEvent(PlayingEvent playingEvent)
    {
        _isPlaying = playingEvent.isPlaying;
    }
}

[Serializable]
public sealed class PlayerState
{
    public Vector3 pos;
}

public sealed class PlayerSystem : ISaveParticipant
{
    private PlayerState runtimeState = new();
    private Transform _playerTransform;

    public string SaveId => "player";

    public int Priority => 0;

    public Type StateType => typeof(PlayerState);

    public PlayerSystem(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public object CaptureState()
    {
        runtimeState.pos = _playerTransform.position;
        return runtimeState;
    }

    public void RestoreState(object state)
    {
        if (state is not PlayerState playerState)
        {
            Debug.LogError("Invalid PlayerState");
            return;
        }

        runtimeState = playerState;

        _playerTransform.position = runtimeState.pos;
    }

    public PlayerState GetState()
    {
        return runtimeState;
    }

    
}*/