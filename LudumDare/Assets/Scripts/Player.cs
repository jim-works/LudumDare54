using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory), typeof(KeyboardMovementInput), typeof(Control))]
public class Player : MonoBehaviour
{
    public enum State {
        Default,
        DiggingThroughGarbage
    }
    public float InteractRadius = 3;
    public float PickpocketAlarmIncrement = 0.25f;
    public PickpocketUI PickpocketUI;
    public GameObject DepositUI;
    public GameObject GarbageDigUI;

    public GarbageCanManager GarbageCanManager;
    public State MyState {get; set;}

    public Inventory Inventory;
    public FinderUI FinderUI;
    public Teleporter.TeleporterLocation CurrentLocation;
    private KeyboardMovementInput input;
    private Control control;
    void Start()
    {
        Inventory = GetComponent<Inventory>();
        input = GetComponent<KeyboardMovementInput>();
        control = GetComponent<Control>();
    }
    // Update is called once per frame
    void Update()
    {
        if (DoItemDepositing()) {
            PickpocketUI.gameObject.SetActive(false);
            GarbageDigUI.SetActive(false);
        }
        else if (DoGarbageDig()) {
            PickpocketUI.gameObject.SetActive(false);
        } else {
            DoPickpocket();
        }
        UpdateFinderUI();
        switch (MyState) {
            case State.Default:
                input.enabled = true;
                break;
            case State.DiggingThroughGarbage:
                input.enabled = false;
                control.MoveDirection = Vector2.zero;
                break;
        }
    }
    //returns if in range
    private bool DoPickpocket()
    {
        GameObject civ = ObjectRegistry.Singleton.GetClosestCivilian(transform.position, InteractRadius);
        PickpocketUI.gameObject.SetActive(civ != null);
        if (civ != null) {
            PickpocketUI.DisplayFor(civ.GetComponent<Inventory>(), 10);
            return true;
        }
        return false;
    }
    private bool DoGarbageDig()
    {
        bool inRange = Vector3.Distance(GarbageCanManager.SpecialGarbageCan.transform.position, transform.position) <= InteractRadius;
        GarbageDigUI.SetActive(inRange);
        return inRange;
    }
    private bool DoItemDepositing()
    {
        if (Inventory.Item == null) {
            DepositUI.SetActive(false);
            return false;
        }
        ItemReceiver recv = ObjectRegistry.Singleton.GetClosestItemReciever(transform.position, InteractRadius);
        DepositUI.SetActive(recv != null);
        return recv != null;
    }
    public void OnPickpocket(Inventory target, PickPocketResult result) {
        if (result == PickPocketResult.Success) {
            Inventory.SwapInventory(target);
            Alarm.IncreaseAlarm(PickpocketAlarmIncrement);
        }
    }
    void UpdateFinderUI()
    {
        switch (FinderUI.GetState()) {
            case FinderUI.State.Garbage:
                if (LevelRequirements.Singleton.RequiredDrug.Item == null || Inventory.Item == LevelRequirements.Singleton.RequiredDrug.Item) {
                    FinderUI.SetState(CurrentLocation == Teleporter.TeleporterLocation.Starting ? FinderUI.State.StartingTeleporter : FinderUI.State.MafiaGuy);
                }
                break;
            case FinderUI.State.StartingTeleporter:
                if (CurrentLocation == Teleporter.TeleporterLocation.Destination) {
                    FinderUI.SetState(FinderUI.State.MafiaGuy);
                }
                break;
            case FinderUI.State.MafiaGuy:
                if (LevelRequirements.Singleton.Satisfied()) {
                    FinderUI.SetState(FinderUI.State.Exit);
                }
                break;
        }
        //reset if requirements are not met
        if (LevelRequirements.Singleton.RequiredDrug.Item != null && LevelRequirements.Singleton.RequiredDrug.Item != Inventory.Item) {
            FinderUI.SetState(FinderUI.State.Garbage);
        } else if (LevelRequirements.Singleton.RequiredMoney > 0) {
            FinderUI.SetState(CurrentLocation == Teleporter.TeleporterLocation.Starting ? FinderUI.State.StartingTeleporter : FinderUI.State.MafiaGuy);
        }

    }
}
