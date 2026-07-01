using System;
using UnityEngine;

public class SongConductor : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _song;

    [Header("Song Settings")]
    [SerializeField, Range(1, 300)]
    private float _bpm = 150f;

    [SerializeField]
    private int _beatsPerBar = 3;

    [Tooltip("Corrección en segundos si la música tiene silencio al inicio")]
    [SerializeField]
    private double _offset = 0;


    // DSP timing
    private double _songStartDSPTime;
    private double _pauseStartDSPTime;
    private double _totalPausedTime;


    // State
    private bool _isPlaying;
    private bool _isPaused;


    // Beat tracking
    private double _songPosition;
    private double _currentBeat;

    private int _lastBeat = -1;


    // Events
    public event Action OnBeat;
    public event Action<int> OnBeatDetailed;
    public event Action OnBar;
    public event Action OnSongFinished;



    private double SecondsPerBeat =>
        60.0 / _bpm;


    private void Awake()
    {
        if (_musicSource == null)
            _musicSource = GetComponent<AudioSource>();

        _musicSource.clip = _song;
        _musicSource.playOnAwake = false;
    }

    private void Update()
    {
        if (!_isPlaying || _isPaused) return;

        if (_musicSource.isPlaying == false && _isPlaying)
        {
            FinishSong();
        }

        UpdateSongTime();
        UpdateBeat();

    }

    private void UpdateSongTime()
    {
        _songPosition =
            AudioSettings.dspTime
            - _songStartDSPTime
            - _totalPausedTime
            - _offset;


        _currentBeat =
            _songPosition / SecondsPerBeat;
    }

    private void UpdateBeat()
    {
        int beatIndex =
            Mathf.FloorToInt((float)_currentBeat);


        if (beatIndex == _lastBeat)
            return;


        _lastBeat = beatIndex;


        int beatInBar =
            (beatIndex % _beatsPerBar) + 1;


        if (beatInBar == 1)
            OnBar?.Invoke();

        OnBeat?.Invoke();

        OnBeatDetailed?.Invoke(
            beatInBar
        );
    }

    public void StartSong()
    {
        if (_song == null)
        {
            Debug.LogError(
                "No hay canción asignada"
            );
            return;
        }


        _songStartDSPTime =
            AudioSettings.dspTime + 1.0;


        _totalPausedTime = 0;

        _lastBeat = -1;


        _musicSource.PlayScheduled(
            _songStartDSPTime
        );


        _isPlaying = true;
        _isPaused = false;
    }

    public void StopSong()
    {
        _musicSource.Stop();

        _isPlaying = false;
        _isPaused = false;

        _songPosition = 0;
        _currentBeat = 0;

        _lastBeat = -1;
    }

    public void PauseSong()
    {
        if (!_isPlaying || _isPaused)
            return;


        _pauseStartDSPTime =
            AudioSettings.dspTime;


        _isPaused = true;

        _musicSource.Pause();
    }

    public void ResumeSong()
    {
        if (!_isPaused)
            return;


        _totalPausedTime +=
            AudioSettings.dspTime
            - _pauseStartDSPTime;


        _isPaused = false;

        _musicSource.UnPause();
    }

    private void FinishSong()
    {
        _isPlaying = false;

        OnSongFinished?.Invoke();
    }





    // =============================
    // Public getters
    // =============================


    public double SongTime =>
        _songPosition;


    public double CurrentBeat =>
        _currentBeat;


    public int CurrentBeatIndex =>
        Mathf.FloorToInt(
            (float)_currentBeat
        );


    public float CurrentBPM =>
        _bpm;



    public void SetBPM(float newBPM)
    {
        if (newBPM <= 0)
            return;


        _bpm = newBPM;
    }



    public void SetOffset(double offset)
    {
        _offset = offset;
    }
}
