using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum PickPocketResult
{
    Success,
    Cancelled,
    OutOfTime,
}

public class PickpocketUI : MonoBehaviour
{
    private enum State
    {
        Deciding,
        Pickpocketing,
        Done
    }
    private State state;
    private Inventory target;
    public GameObject PickpocketText;
    public GameObject SwapItemUI;
    public ItemUI SwapItemImage;
    public Slider DecideTimeSlider;
    private float decideTime;
    private float timeElapsed;
    
    void OnEnable() {
        target = null;
    }
    public void DisplayFor(Inventory target, float decideTime)
    {
        if (target == this.target) return; //already displaying for this guy, no need to go again
        state = State.Deciding;
        this.decideTime = decideTime;
        this.target = target;
        timeElapsed = 0;
        PickpocketText.SetActive(true);
        SwapItemUI.SetActive(false);
        SwapItemImage.Displaying = target;
    }

    void Update()
    {
        switch (state)
        {
            case State.Deciding:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    state = State.Pickpocketing;
                    PickpocketText.SetActive(false);
                    SwapItemUI.SetActive(true);
                }
                break;
            case State.Pickpocketing:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = State.Done;
                    ObjectRegistry.Singleton.Player.OnPickpocket(target, PickPocketResult.Success);
                    SwapItemUI.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    state = State.Done;
                    ObjectRegistry.Singleton.Player.OnPickpocket(target, PickPocketResult.Cancelled);
                    SwapItemUI.SetActive(false);
                }
                else if (timeElapsed > decideTime)
                {
                    state = State.Done;
                    ObjectRegistry.Singleton.Player.OnPickpocket(target, PickPocketResult.OutOfTime);
                    SwapItemUI.SetActive(false);
                }
                else
                {
                    timeElapsed += Time.deltaTime;
                    DecideTimeSlider.value = timeElapsed/decideTime;
                }
                break;
            case State.Done:
                break;
        }
    }
}
