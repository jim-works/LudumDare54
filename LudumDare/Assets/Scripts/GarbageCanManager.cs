using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCanManager : MonoBehaviour
{
    public GameObject SpecialGarbageCanPrefab;
    public GameObject BasicGarbageCanPrefab;
    public GameObject SpecialGarbageCan {get; private set;}

    void Awake()
    {
        SelectSpeicalGarbageCan();
    }

    public void SelectSpeicalGarbageCan()
    {
        if (SpecialGarbageCan != null) {
            GameObject basic = Instantiate(BasicGarbageCanPrefab);
            basic.transform.position = SpecialGarbageCan.transform.position;
            basic.transform.parent = transform;
            Destroy(SpecialGarbageCan);
            SpecialGarbageCan = null;
            Debug.Log("Reset trashcan");
        }
        Transform[] transforms = GetComponentsInChildren<Transform>();
        int promotionIdx = Random.Range(0, transforms.Length);
        GameObject special = Instantiate(SpecialGarbageCanPrefab);
        special.transform.position = transforms[promotionIdx].position;
        special.transform.parent = transform;
        SpecialGarbageCan = special;
        Debug.Log($"Promoted {promotionIdx} at position {special.transform.position}");
        Destroy(transforms[promotionIdx].gameObject);
    }
}
