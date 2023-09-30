using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour
{
    void OnEnable()
    {
        ObjectRegistry.Singleton.Civilians.Add(gameObject);
    }

    void OnDisable()
    {
        ObjectRegistry.Singleton.Civilians.Remove(gameObject);
    }
}
