using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeService
{
    public string timerName { get; private set; }
    public bool timerIsEnded { get; private set; }
    public bool timerIsActive { get; private set; }
    public bool timerRepeatable { get; private set; }
    public double timerStartingTime { get; private set; }
    public double timerActualTime { get; private set; }

    public TimeService(double time)
    {
        timerStartingTime = time;
        timerActualTime = timerStartingTime;
        timerIsActive = false;
        timerIsEnded = false;
        timerRepeatable = false;
    }
    public void RestartTimer()
    {
        timerActualTime = timerStartingTime;
    }
    public void StartTimer()
    {
        timerIsActive = true;
    }
    public void StopTimer()
    {
        timerIsActive = false;
    }
    public void RepeatableTimer()
    {
        timerRepeatable = true;
    }

    public void Timer()
    {
        if (timerIsActive)
        {
            timerActualTime -= Time.deltaTime * 1;
            if (timerActualTime <= 0)
            {
                timerIsEnded = true;
                if (timerRepeatable)
                {
                    RestartTimer();
                    StartTimer();
                }

            }
            else
            {
                timerIsEnded = false;
            }
        }
    }
}
