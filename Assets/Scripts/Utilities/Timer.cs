using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer
{
    float maxTimer = 0.0f;
    float currentTimer = 0.0f;
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
}
