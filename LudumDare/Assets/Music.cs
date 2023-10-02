using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip LowSus;
    public AudioClip HighSus;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        var clip = Alarm.Level >= 3 ? HighSus : LowSus;
        if (clip != source.clip)
        {
            source.clip = clip;
            source.Play();
        }
    }
}
