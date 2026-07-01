using System;

[Serializable]
public struct BeatTarget
{
    public int BeatIndex;
    public LaneType Lane;

    public BeatTarget(int beatIndex, LaneType lane)
    {
        BeatIndex = beatIndex;
        Lane = lane;
    }
}