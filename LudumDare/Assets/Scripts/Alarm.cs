using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Alarm
{
    public static float Level {get; private set;}
    public static float DetectionRadiusMult => Mathf.Max(0,Level-1f);

    public static void IncreaseAlarm(float amount)
    {
        Level += amount;
    }
    public static void DecreaseAlarm(float amount)
    {
        Level -= amount;
    }
    public static void ResetAlarm()
    {
        Level = 0;
    }
}
