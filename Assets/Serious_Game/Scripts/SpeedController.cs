using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SpeedState
{
    Normal,
    Accelerating,
    Overheated,
    Recovery
}


public class SpeedController : MonoBehaviour
{
    [SerializeField] private SongConductor conductor;
    //[SerializeField] private ComboSystem comboSystem;
    [SerializeField] private Spin vinylSpin;

    [SerializeField] private InputActionReference turboAction;


    [SerializeField] private float baseBpm = 150f;
    //[SerializeField] private float turboBpm = 180f;

    //private float _currentBpm;


    [SerializeField] private float maxHeat = 100f;
    [SerializeField] private float heatGain = 30f;
    [SerializeField] private float heatDecay = 20f;


    [SerializeField] private float overheatDuration = 2f;

    private float _overheatTimer;

    private bool _turboHeld;


    public event Action<float> OnHeatChanged;
    public event Action<SpeedState> OnStateChanged;

    private void OnEnable()
    {
        turboAction.action.Enable();
    }


    private void OnDisable()
    {
        turboAction.action.Disable();
    }

    private void Awake()
    {
        SetState(SpeedState.Normal);

        SetHeat(0f);

        ScoreMultiplier = 1f;
    }


    private void Start()
    {
        //_currentBpm = baseBpm;

        //conductor.SetBPM(baseBpm);
    }


    private void Update()
    {
        UpdateTurboInput();

        switch (CurrentState)
        {
            case SpeedState.Normal:
                UpdateNormal();
                break;


            case SpeedState.Accelerating:
                UpdateAccelerating();
                break;


            case SpeedState.Overheated:
                UpdateOverheated();
                break;


            case SpeedState.Recovery:
                UpdateRecovery();
                break;
        }
    }

    private void UpdateTurboInput()
    {
        if (turboAction == null)
            return;


        if (turboAction.action.IsPressed())
        {
            StartTurbo();
        }
        else
        {
            StopTurbo();
        }
    }

    private void UpdateNormal()
    {
        CoolHeat();

        //SetBpm(baseBpm);

        ScoreMultiplier = 1f;
        vinylSpin.SetSpeedMultiplier(1f);

        if (_turboHeld) /*&&
           comboSystem.CurrentCombo >= 4)*/
        {
            SetState(
                SpeedState.Accelerating
            );
        }
    }


    private void UpdateAccelerating()
    {
        /*if (comboSystem.CurrentCombo < 4)
        {
            SetState(
                SpeedState.Normal
            );

            return;
        }*/


        if (!_turboHeld)
        {
            SetState(
                SpeedState.Normal
            );

            return;
        }


        SetHeat(
            CurrentHeat +
            heatGain * Time.deltaTime
        );


        float heatPercent = CurrentHeat / maxHeat;

        vinylSpin.SetSpeedMultiplier(
            Mathf.Lerp(1f, 1.8f, heatPercent)
        );


        /*float targetBpm =
            Mathf.Lerp(
                baseBpm,
                turboBpm,
                heatPercent
            );


        SetBpm(targetBpm);*/


        ScoreMultiplier =
            Mathf.Lerp(
                1f,
                3f,
                heatPercent
            );




        if (CurrentHeat >= maxHeat)
        {
            EnterOverheat();
        }
    }


    private void EnterOverheat()
    {
        SetState(
            SpeedState.Overheated
        );


        _overheatTimer =
            overheatDuration;


        ScoreMultiplier = 0.5f;
    }


    private void UpdateOverheated()
    {
        /*SetBpm(
            baseBpm * 0.8f
        );*/


        _overheatTimer -= Time.deltaTime;
        vinylSpin.SetSpeedMultiplier(0.8f);


        if (_overheatTimer <= 0)
        {
            SetState(
                SpeedState.Recovery
            );
        }
    }


    private void UpdateRecovery()
    {
        SetHeat(
            CurrentHeat -
            heatDecay * Time.deltaTime
        );


        /*float recoveryPercent =
            1f -
            (CurrentHeat / maxHeat);
        */

        /*float targetBpm =
            Mathf.Lerp(
                baseBpm * 0.8f,
                baseBpm,
                recoveryPercent
            );*/


        //SetBpm(targetBpm);


        ScoreMultiplier = 1f;
        float recovery = 1f - (CurrentHeat / maxHeat);

        vinylSpin.SetSpeedMultiplier(
            Mathf.Lerp(0.8f, 1f, recovery)
        );


        if (CurrentHeat <= 0)
        {
            SetState(
                SpeedState.Normal
            );
        }
    }


    private void SetState(SpeedState state)
    {
        if (CurrentState == state)
            return;


        CurrentState = state;


        OnStateChanged?.Invoke(
            CurrentState
        );
    }


    private void SetHeat(float value)
    {
        value =
            Mathf.Clamp(
                value,
                0f,
                maxHeat
            );


        if (Mathf.Approximately(
            CurrentHeat,
            value))
        {
            return;
        }


        CurrentHeat = value;


        OnHeatChanged?.Invoke(
            CurrentHeat / maxHeat
        );
    }


    private void CoolHeat()
    {
        SetHeat(
            CurrentHeat -
            heatDecay * Time.deltaTime
        );
    }


    /*private void SetBpm(float bpm)
    {
        if (Mathf.Approximately(
            _currentBpm,
            bpm))
        {
            return;
        }


        _currentBpm = bpm;


        conductor.SetBPM(
            _currentBpm
        );
    }*/


    public void StartTurbo()
    {
        Logger.Combat("Turbo pulsado", this);
        _turboHeld = true;
    }


    public void StopTurbo()
    {
        _turboHeld = false;
    }


    public SpeedState CurrentState
    {
        get;
        private set;
    }


    public float CurrentHeat
    {
        get;
        private set;
    }


    public float ScoreMultiplier
    {
        get;
        private set;
    } = 1f;
}