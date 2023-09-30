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
    public float PickpocketRadius = 3;
    public float GarbageDigRadius = 3;
    public int Cash;
    public PickpocketUI PickpocketUI;
    public GameObject GarbageDigUI;

    public GarbageCanManager GarbageCanManager;
    public State MyState {get; set;}

    private Inventory inv;
    private KeyboardMovementInput input;
    private Control control;
    void Start()
    {
        inv = GetComponent<Inventory>();
        input = GetComponent<KeyboardMovementInput>();
        control = GetComponent<Control>();
    }
    // Update is called once per frame
    void Update()
    {
        if (DoGarbageDig()) {
            PickpocketUI.gameObject.SetActive(false);
        } else {
            DoPickpocket();
        }
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
        GameObject civ = ObjectRegistry.Singleton.GetClosestCivilian(transform.position, PickpocketRadius);
        PickpocketUI.gameObject.SetActive(civ != null);
        if (civ != null) {
            PickpocketUI.DisplayFor(civ.GetComponent<Inventory>(), 10);
            return true;
        }
        return false;
    }
    private bool DoGarbageDig()
    {
        bool inRange = Vector3.Distance(GarbageCanManager.SpecialGarbageCan.transform.position, transform.position) <= GarbageDigRadius;
        GarbageDigUI.SetActive(inRange);
        return inRange;
    }
    public void OnPickpocket(Inventory target, PickPocketResult result) {
        if (result == PickPocketResult.Success) {
            inv.SwapInventory(target);
        }
    }
}
