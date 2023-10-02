using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawner : MonoBehaviour
{
    public GameObject OrangeCatPrefab;
    public float OrangeCatWeight;
    public int OrangeCatStartingLevel = 3;
    public GameObject GrayCatPrefab;
    public float GrayCatWeight;
    public GameObject WhiteCatPreab;
    public float WhiteCatWeight;
    public int WhiteCatStartingLevel = 5;

    [Header("index 0 = alarm level 1, after max level switches to batch spawning")]
    public int[] SpawnsPerLevel;
    public int BatchSpawnsPerLevel = 10;

    private GuardSpawnpoint[] spawns;
    void Start()
    {
        spawns = GetComponentsInChildren<GuardSpawnpoint>();
    }

    public void OnAlarmIncreased(int level)
    {
        int spawnCount = level+1 < SpawnsPerLevel.Length ? SpawnsPerLevel[level+1] : BatchSpawnsPerLevel;
        List<GameObject> spawns = new List<GameObject>() {GrayCatPrefab};
        List<float> weights = new List<float>() {GrayCatWeight};
        float totalWeight = GrayCatWeight;
        if (level >= OrangeCatStartingLevel) {
            spawns.Add(OrangeCatPrefab);
            weights.Add(OrangeCatWeight);
            totalWeight += OrangeCatWeight;
        }
        if (level >= WhiteCatStartingLevel) {
            spawns.Add(WhiteCatPreab);
            weights.Add(WhiteCatWeight);
            totalWeight += WhiteCatWeight;
        }
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnGuard(spawns.ToArray(), weights.ToArray(), totalWeight);
        }
    }

    private void SpawnGuard(GameObject[] guards, float[] weights, float totalWeight)
    {
        GuardSpawnpoint spawnPoint = spawns[Random.Range(0,spawns.Length)];
        Vector2 spawnPosition = (Vector2)spawnPoint.transform.position + spawnPoint.Radius*Random.insideUnitCircle;
        GameObject go = Instantiate(guards[Util.ChooseFromWeightedArray(weights, totalWeight)], spawnPosition, Quaternion.identity);
        Debug.Log($"Spawned {go.name}");
    }
}
