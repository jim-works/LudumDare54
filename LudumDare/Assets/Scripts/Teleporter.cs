using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 Destination;
    public const float TELEPORT_COOLDOWN = 0.5f;
    public static float LastTeleportTime;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Time.time - LastTeleportTime < TELEPORT_COOLDOWN) {
            return;
        }
        if (col.GetComponent<Player>()) {
            col.transform.position = Destination;
            LastTeleportTime = Time.time;
        }
    }
}
