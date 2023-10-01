using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestroyer : MonoBehaviour
{
    public float TTL = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TriggerDeath());
    }

    private IEnumerator TriggerDeath()
    {
        yield return new WaitForSeconds(TTL);
        Destroy(gameObject);
    }
}
