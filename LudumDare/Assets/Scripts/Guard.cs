using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Control))]
public class Guard : MonoBehaviour
{
    public enum State
    {
        Wandering,
        Chasing
    }
    public float DetectionRadius = 2;
    public float ChaseGiveUpExtraDistance = 2;
    public float WanderMoveSpeed = 2;
    public float ChaseMoveSpeed = 5;
    public Transform Detecting;
    public MonoBehaviour Chaser;
    public MonoBehaviour Wanderer;

    public State AIState
    {
        get
        {
            return _state;
        }
        set
        {
            if (_state == value) return;
            SwitchState(_state, false);
            SwitchState(value, true);
            _state = value;
        }
    }
    private State _state;
    private Control control;
    void Awake() {
        //setup
        control = GetComponent<Control>();
        Chaser.enabled = false;
        Wanderer.enabled = true;
        control.MoveSpeed = WanderMoveSpeed;
        _state = State.Wandering;
    }

    void Update()
    {
        UpdateBehavior();
    }

    private void UpdateBehavior()
    {
        float d = Vector3.Distance(Detecting.position, transform.position);
        switch (AIState) {
            case State.Wandering:
                if (d <= DetectionRadius*Alarm.DetectionRadiusMult) {
                    AIState = State.Chasing;
                    Debug.Log("chasing");
                }
                break;
            case State.Chasing:
                if (d >= DetectionRadius*Alarm.DetectionRadiusMult+ChaseGiveUpExtraDistance) {
                    AIState = State.Wandering;
                    Debug.Log("wandering");
                }
                break;
        }
    }
    private void SwitchState(State state, bool active)
    {
        switch (state)
        {
            case State.Wandering:
                Wanderer.enabled = active;
                if (active) {
                    control.MoveSpeed = WanderMoveSpeed;
                }
                break;
            case State.Chasing:
                Chaser.enabled = active;
                if (active) {
                    control.MoveSpeed = ChaseMoveSpeed;
                }
                break;
        }
    }

    void OnEnable()
    {
        ObjectRegistry.Singleton.Guards.Add(gameObject);
    }

    void OnDisable()
    {
        ObjectRegistry.Singleton.Guards.Remove(gameObject);
    }
}
