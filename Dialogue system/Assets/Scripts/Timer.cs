using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float _durationTime, _startTime;

	public Timer(float time)
    {
        _durationTime = time;
        _startTime = Time.time;
    }

    public bool Done()
    {
        return (Time.time >= _startTime + _durationTime);
    }

    public float GetSpentTime()
    {
        return Time.time - _startTime;
    }
}
