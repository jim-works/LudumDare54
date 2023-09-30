using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Following;
    public float FollowSpeed = 10f;

    void Update()
    {
        //todo: change this to be smooth
        var target = Following.position;//Vector3.Lerp(transform.position, Following.position, Time.deltaTime*FollowSpeed); <- looks glitchy and not framerate independent
        target.z = transform.position.z;
        transform.position = target;
    }
}
