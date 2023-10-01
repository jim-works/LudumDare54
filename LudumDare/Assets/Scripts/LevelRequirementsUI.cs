using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelRequirementsUI : MonoBehaviour
{
    public GameObject RequiredDrugUI;
    public TextMeshProUGUI NeedText;
    void Update()
    {
        RequiredDrugUI.SetActive(LevelRequirements.Singleton.RequiredDrug.Item != null);
        if (LevelRequirements.Singleton.RequiredDrug.Item == null && LevelRequirements.Singleton.RequiredMoney <= 0)
        {
            //all requirements satisfied, show how much extra is banked
            NeedText.text = $"BONUS: ${-LevelRequirements.Singleton.RequiredMoney}";
        }
        else
        {
            //at least one requirement not satisfied, show what's left\
            NeedText.text = $"NEED: {(LevelRequirements.Singleton.RequiredDrug.Item != null ? "↑" : "")}{(LevelRequirements.Singleton.RequiredMoney > 0 ? "+$" + LevelRequirements.Singleton.RequiredMoney.ToString() : "")}";
        }

    }
}
