using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CivilianNamer : MonoBehaviour
{
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Text.text= gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
