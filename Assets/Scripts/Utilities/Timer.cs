using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer
{
    float maxTimer = 0.0f;
    float currentTimer = 0.0f;
    public float CurrentTimer { get { return currentTimer; } }
    Action completeAction = null;

    bool hasExecuted = false;


    public Timer(float a_maxTime, Action a_action)
    {
        maxTimer = a_maxTime;
        currentTimer = maxTimer;
        completeAction = a_action;
    }

    public void Tick(float a_timeSinceLastTick)
    {
        if (currentTimer > 0.0f)
            currentTimer -= a_timeSinceLastTick;

        else if (currentTimer <= 0.0f && !hasExecuted)
        {
            completeAction.Invoke();
            hasExecuted = true;
        }
    }

    public void Reset()
    {
        currentTimer = maxTimer;
        hasExecuted = false;
    }

    public void Reset(float a_time)
    {
        maxTimer = a_time;
        Reset();
    }

    public void Reset(Action a_action)
    {
        completeAction = a_action;
        Reset();
    }

    public void Reset(float a_time, Action a_action)
    {
        maxTimer = a_time;
        completeAction = a_action;
        Reset();
    }
}
