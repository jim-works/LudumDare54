using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Control))]
public class WanderMovementInput : MonoBehaviour
{
    public float WanderRadius = 2;
    public float WanderPauseDuration = 2f;
    //set whenever component is enabled
    private Vector2 origin;
    private Vector2 wanderDestination;
    private bool doneMoving = false;
    private IEnumerator wanderingCoroutine;
    private Control control;
    private const float MOVEMENT_EPSILON = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
    }

    void OnEnable()
    {
        origin = transform.position;
        wanderingCoroutine = SetDestination();
        StartCoroutine(wanderingCoroutine);
    }

    void OnDisable()
    {
        doneMoving = true;
        StopCoroutine(wanderingCoroutine);
    }

    void Update()
    {
        if (doneMoving) {
            control.MoveDirection = Vector2.zero;
        };
        Vector2 delta = wanderDestination - (Vector2)transform.position;
        if (delta.sqrMagnitude > MOVEMENT_EPSILON*MOVEMENT_EPSILON) {
            doneMoving = true;
        }
        control.MoveDirection =  delta;
    }

    private IEnumerator SetDestination() {
        while(true) {
            yield return new WaitForSeconds(WanderPauseDuration);
            Debug.Log("Destination Set!");
            doneMoving = false;
            wanderDestination = origin + Random.insideUnitCircle*WanderRadius;
        }
    }
}
