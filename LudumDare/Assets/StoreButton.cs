using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour
{
    public int Count;
    public int Cost;
    public TextMeshProUGUI Text;

    // Update is called once per frame
    void Update()
    {
        Text.text = $"{Count} - ${Cost}";
        GetComponent<Button>().interactable = Cost <= DataStore.BankedMoney;
    }
}
