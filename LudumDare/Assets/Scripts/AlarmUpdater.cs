using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmUpdater : MonoBehaviour
{
    public float IncrementPerSecond = 0.1f;
    void Start()
    {
        Alarm.ResetAlarm();
    }

    void Update()
    {
        float oldAlarm = Alarm.Level;
        Alarm.IncreaseAlarm(IncrementPerSecond * Time.deltaTime);
        if ((int)Alarm.Level - (int)oldAlarm != 0) {
            Debug.Log($"Alarm increased to {Alarm.Level}");
        }
    }
}
