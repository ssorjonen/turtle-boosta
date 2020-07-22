using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    public string stageName;
    public string stagePath;
    public bool stageUnlocked;
    public float playerBestTime;
    public float developerTime;
    public int song;

    public StageInfo(string _stageName, string _stagePath, float _developerTime, int _song)
    {
        this.stageName = _stageName;
        this.stagePath = _stagePath;
        this.developerTime = _developerTime;
        stageUnlocked = false;
        playerBestTime = 0f;
        song = _song;
        
    }

    public StageInfo(string _stageName, string _stagePath, float _developerTime, int _song, bool _stageUnlocked)
    {
        this.stageName = _stageName;
        this.stagePath = _stagePath;
        this.stageUnlocked = _stageUnlocked;
        this.developerTime = _developerTime;
        song = _song;
    }

}
