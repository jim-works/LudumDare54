using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class VendingMachineItemDescriptionUI : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public float DisplaySeconds = 5;
    public void Display(VendingBuff buff)
    {
       Text.text = $"- {buff.name} -\n{buff.Description}";
       StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        float startTime = Time.time;
        while (Time.time - startTime < DisplaySeconds)
        {
            Color c = Text.color;
            c.a = Mathf.Lerp(255,0,(Time.time-startTime)/DisplaySeconds);
            Text.color = c;
            yield return null;
        }
    }
}