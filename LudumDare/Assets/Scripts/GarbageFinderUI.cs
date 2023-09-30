using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageFinderUI : MonoBehaviour
{
    public GarbageCanManager GarbageCanManager;
    public RectTransform Arrow;

    void Update()
    {
        if (GarbageCanManager.SpecialGarbageCan == null)
        {
            return;
        }        
        Vector2 diff = GarbageCanManager.SpecialGarbageCan.transform.position - ObjectRegistry.Singleton.Player.transform.position;
        Arrow.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(diff.y,diff.x)*Mathf.Rad2Deg-90);
    }
}
