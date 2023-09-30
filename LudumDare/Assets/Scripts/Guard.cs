using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public enum State
    {
        Wandering,
        Chasing
    }
    public float DetectionRadius = 2;
    public float ChaseGiveUpDistance = 3;
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
            SetComponentActive(_state, false);
            SetComponentActive(value, true);
            _state = value;
        }
    }
    private State _state;

    void Awake() {
        //setup
        Chaser.enabled = false;
        Wanderer.enabled = true;
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
                if (d <= DetectionRadius) {
                    AIState = State.Chasing;
                    Debug.Log("chasing");
                }
                break;
            case State.Chasing:
                if (d >= ChaseGiveUpDistance) {
                    AIState = State.Wandering;
                    Debug.Log("wandering");
                }
                break;
        }
    }
    private void SetComponentActive(State state, bool active)
    {
        switch (state)
        {
            case State.Wandering:
                Wanderer.enabled = active;
                break;
            case State.Chasing:
                Chaser.enabled = active;
                break;
        }
    }
}
