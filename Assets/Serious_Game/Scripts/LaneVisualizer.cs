using UnityEngine;
using System.Collections;

public class LaneVisualizer : MonoBehaviour
{
    [SerializeField] private LaneManager laneManager;
    [SerializeField] private SongConductor songConductor;

    [Header("Lane Materials")]
    [SerializeField] private Material outerMaterial;
    [SerializeField] private Material middleMaterial;
    [SerializeField] private Material innerMaterial;

    [Header("Emission")]
    [SerializeField] private Color inactiveEmission = Color.black;
    [SerializeField] private Color activeEmission = new Color(2f, 2f, 0f);
    [SerializeField] private Color beatEmission = new Color(5f, 5f, 0f);

    private LaneType currentLane;
    private float beatTimer;

    private void OnEnable()
    {
        laneManager.OnLaneChanged += HandleLaneChanged;
        songConductor.OnBeatDetailed += HandleBeat;
    }

    private void OnDisable()
    {
        laneManager.OnLaneChanged -= HandleLaneChanged;
        songConductor.OnBeatDetailed -= HandleBeat;
    }

    private void Start()
    {
        SetAllInactive();

        HandleLaneChanged(
            laneManager.CurrentLane
        );
    }

    private void Update()
    {
        if (beatTimer <= 0f) return;

        beatTimer -= Time.deltaTime;

        if (beatTimer <= 0f)
        {
            SetEmission(GetCurrentMaterial(), activeEmission);
        }
    }

    private void HandleLaneChanged(LaneType lane)
    {
        SetAllInactive();
        currentLane = lane;

        switch (lane)
        {
            case LaneType.Outer:
                SetEmission(
                    outerMaterial,
                    activeEmission
                );
                break;

            case LaneType.Middle:
                SetEmission(
                    middleMaterial,
                    activeEmission
                );
                break;

            case LaneType.Inner:
                SetEmission(
                    innerMaterial,
                    activeEmission
                );
                break;
        }
    }

    private void HandleBeat(int beatInBar)
    {
        if (beatInBar != 3)
            return;

        beatTimer = 0.12f;
        SetEmission(GetCurrentMaterial(), beatEmission);
    }

    private void SetAllInactive()
    {
        SetEmission(
            outerMaterial,
            inactiveEmission
        );

        SetEmission(
            middleMaterial,
            inactiveEmission
        );

        SetEmission(
            innerMaterial,
            inactiveEmission
        );
    }

    private void SetEmission(
        Material material,
        Color color)
    {
        material.SetColor(
            "_EmissionColor",
            color
        );
    }

    private Material GetCurrentMaterial()
    {
        switch (currentLane)
        {
            case LaneType.Outer:
                return outerMaterial;

            case LaneType.Middle:
                return middleMaterial;

            case LaneType.Inner:
                return innerMaterial;
        }

        return null;
    }
}