using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Alarm
{
    public static float Level = 0f;
    public static float DetectionRadiusMult => Mathf.Max(0,Level-1f);
}
