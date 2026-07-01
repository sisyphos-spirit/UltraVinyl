using System;
using UnityEngine;

public enum LaneType
{
    Outer = 0,
    Middle = 1,
    Inner = 2,
}

public class LaneManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SongConductor songConductor;

    public LaneType CurrentLane => _currentTarget.Lane;
    private LaneType _lastLane;

    public BeatTarget CurrentTarget => _currentTarget;

    public event Action<LaneType> OnLaneChanged;

    private BeatTarget _currentTarget;



    private void OnEnable()
    {
        if (songConductor != null)
        {
            songConductor.OnBar += HandleBar;
        }
    }

    private void OnDisable()
    {
        if (songConductor != null)
        {
            songConductor.OnBar -= HandleBar;
        }
    }

    private void HandleBar()
    {
        GenerateTarget(songConductor.CurrentBeatIndex);
    }

    private void GenerateTarget(int beatIndex)
    {
        LaneType lane = GetRandomLane();

        _currentTarget = new BeatTarget(
            beatIndex,
            lane
        );

        OnLaneChanged?.Invoke(lane);
    }

    private LaneType GetRandomLane()
    {
        LaneType lane;

        do
        {
            lane = (LaneType)UnityEngine.Random.Range(0, 3);
        }
        while (lane == _lastLane);

        _lastLane = lane;

        return lane;
    }
}
