using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AlertUI : MonoBehaviour
{
    public Image IconImage;
    public TextMeshProUGUI SusText;
    private Image backgroundImage;
    public Sprite[] Icons;
    public Color[] BackgroundColors;
    private int currIdx;
    // Start is called before the first frame update
    void Start()
    {
        backgroundImage = GetComponent<Image>();
        currIdx = 0;
        SetValues(currIdx);
    }

    // Update is called once per frame
    void Update()
    {
        int newIdx = (int)Alarm.Level;
        if (newIdx != currIdx)
        {
            currIdx = newIdx;
            SetValues(currIdx);
        }
    }

    private void SetValues(int idx)
    {
        IconImage.sprite = Icons[Math.Min(Icons.Length-1,idx)];
        backgroundImage.color = BackgroundColors[Math.Min(Icons.Length-1,idx)];
        SusText.text = $"Sus Level {idx}";
    }
}
