using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Control), typeof(WalkAnimator))]
public class LuggageCart : MonoBehaviour
{
    public enum State {
        Empty,
        Boarded
    }
    public Vector3 PassengerOffset;
    public State MyState {get; private set;}
    private Control control;
    private WalkAnimator anim;
    private GameObject passenger;
    private float prevDistancePerFrame;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
        anim = GetComponent<WalkAnimator>();
        MyState = State.Empty;
        control.enabled = false;
    }

    void OnEnable()
    {
        ObjectRegistry.Singleton.LuggageCarts.Add(this);
    }
    void OnDisable()
    {
        ObjectRegistry.Singleton.LuggageCarts.Add(this);
    }

    void Update()
    {
        if (passenger != null) {
            //passenger should be displayed on top of cart if moving up, behind cart if moving down
            passenger.transform.localPosition = PassengerOffset + new Vector3(0,0,anim.currDir == anim.Down ? 0.01f : -0.01f);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemovePassenger();
        }
    }

    public void BoardPassenger(GameObject p)
    {
        if (passenger != null) return;
        passenger = p;
        passenger.GetComponent<Control>().enabled = false;
        MyState = State.Boarded;
        control.enabled = true;
        p.transform.SetParent(transform);
        prevDistancePerFrame = passenger.GetComponent<WalkAnimator>().DistancePerFrame;
    }
    public void RemovePassenger()
    {
        if (passenger == null) {
            return;
        }
        MyState = State.Empty;
        passenger.GetComponent<Control>().enabled = true;
        control.enabled = false;
        passenger.transform.SetParent(null);
        passenger.GetComponent<WalkAnimator>().DistancePerFrame = prevDistancePerFrame;
        passenger = null;
    }
}
