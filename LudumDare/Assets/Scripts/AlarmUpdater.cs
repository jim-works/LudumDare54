using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmUpdater : MonoBehaviour
{
    public float IncrementPerSecond = 0.1f;
    public UnityEvent<int> OnAlarmIncreased;
    void Start()
    {
        Alarm.ResetAlarm();
    }

    void Update()
    {
        float oldAlarm = Alarm.Level;
        Alarm.IncreaseAlarm(IncrementPerSecond * Time.deltaTime);
        if ((int)Alarm.Level - (int)oldAlarm != 0) {
            OnAlarmIncreased.Invoke((int)Alarm.Level);
        }
    }
}
