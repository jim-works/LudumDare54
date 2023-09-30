using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRegistry : MonoBehaviour
{
    public static ObjectRegistry Singleton;
    public List<GameObject> Civilians = new();
    public List<GameObject> Guards = new();
    public Player Player;

    void Awake()
    {
        if (Singleton != null) {
            Debug.LogError("Singleton for ObjectRegistry already exists!");
            Destroy(this);
            return;
        }
        Singleton = this;
    }

    public GameObject GetClosestCivilian(Vector3 origin, float radius)
    {
        GameObject closest = null;
        float minDist = radius;
        foreach (var civ in Civilians) {
            float d = (origin-civ.transform.position).sqrMagnitude;
            if(d <= minDist) {
                closest = civ;
                minDist = d;
            }
        }
        return closest;
    }
    public GameObject GetClosestGuard(Vector3 origin, float radius)
    {
        GameObject closest = null;
        float minDist = radius;
        foreach (var g in Guards) {
            float d = (origin-g.transform.position).sqrMagnitude;
            if(d <= minDist) {
                closest = g;
                minDist = d;
            }
        }
        return closest;
    }
}