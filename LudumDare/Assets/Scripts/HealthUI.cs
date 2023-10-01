using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject HeartPrefab;
    public Vector2 SpawnDelta = new (-64,0);

    private readonly List<GameObject> hearts = new();

    public void UpdateHealth(int health)
    {
        Debug.Log("heart");
        foreach (var g in hearts)
        {
            Destroy(g);
        }
        hearts.Clear();
        for (int i = 0; i < health; i++)
        {
            var go = Instantiate(HeartPrefab);
            hearts.Add(go);
            go.transform.SetParent(transform);
            go.transform.localPosition = SpawnDelta*i;
        }
    }
}
