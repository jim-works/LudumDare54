using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public enum TeleporterLocation {
        Starting,
        Destination
    }
    public Vector3 Destination;
    public TeleporterLocation DestinationLocation;
    public const float TELEPORT_COOLDOWN = 0.5f;
    public static float LastTeleportTime;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Time.time - LastTeleportTime < TELEPORT_COOLDOWN) {
            return;
        }
        var player = col.GetComponent<Player>(); 
        if (player) {
            player.CurrentLocation = DestinationLocation;
            col.transform.position = Destination;
            LastTeleportTime = Time.time;
        }
    }
}
