using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Control), typeof(Rigidbody2D))]
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
    public float KnockBackAccel = 1;
    public Transform Detecting;
    public ChaserMovementInput Chaser;
    public MonoBehaviour Wanderer;
    public GameObject ChaseUI;

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
    private Rigidbody2D rb;
    void Awake() {
        //setup
        control = GetComponent<Control>();
        Chaser.enabled = false;
        Wanderer.enabled = true;
        control.MoveSpeed = WanderMoveSpeed;
        _state = State.Wandering;
        if (Detecting == null) {
            Detecting = ObjectRegistry.Singleton.Player.transform;
            Chaser.Chasing = ObjectRegistry.Singleton.Player.transform;
        }
        rb = GetComponent<Rigidbody2D>();
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
                }
                break;
            case State.Chasing:
                if (d >= DetectionRadius*Alarm.DetectionRadiusMult+ChaseGiveUpExtraDistance) {
                    AIState = State.Wandering;
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
                ChaseUI.SetActive(active);
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("guard"))
        {
            rb.AddForce(KnockBackAccel * rb.mass * (col.gameObject.transform.position-transform.position).normalized, ForceMode2D.Impulse);
        }
    }
}
