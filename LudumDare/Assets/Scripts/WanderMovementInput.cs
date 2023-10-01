using System.Collections;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Control))]
public class WanderMovementInput : MonoBehaviour
{
    public float WanderRadius = 2;
    //smaller area to wander around if destination lands outside navmesh
    public const float SAFE_WANDER_RADIUS = 0.5f;
    public float WanderPauseDuration = 2f;
    //set whenever component is enabled
    private Vector2 origin;
    private Vector2 wanderDestination;
    private bool doneMoving = false;
    private IEnumerator wanderingCoroutine;
    private Control control;
    private const float MOVEMENT_EPSILON = 0.25f;
    private NavMeshPath path;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
    }

    void OnEnable()
    {
        doneMoving = true;
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
            return;
        };
        Vector2 delta = wanderDestination - (Vector2)transform.position;
        if (delta.sqrMagnitude < MOVEMENT_EPSILON*MOVEMENT_EPSILON) {
            doneMoving = true;
        }
        if (path == null)
        {
            //walk in safe area if no path
            control.MoveDirection = delta;
        }
        else
        {
            Vector3 d = path.corners[1] - transform.position;
            if (d.sqrMagnitude < 0.001f && path.corners.Length > 2)
            {
                d = path.corners[2] - transform.position;
            }
            control.MoveDirection = d.normalized;
        }
    }

    private IEnumerator SetDestination() {
        while(true) {
            yield return new WaitForSeconds(WanderPauseDuration);
            wanderDestination = origin + Random.insideUnitCircle*WanderRadius;
            UpdatePath(wanderDestination);
            doneMoving = false;
        }
    }

    void UpdatePath(Vector2 destination)
    {
        path = new();
        NavMesh.SamplePosition(transform.position, out NavMeshHit hitA, 10f, NavMesh.AllAreas);
        NavMesh.SamplePosition(destination, out NavMeshHit hitB, 10f, NavMesh.AllAreas);
        if (!hitA.hit || !hitB.hit || !NavMesh.CalculatePath(hitA.position, hitB.position, NavMesh.AllAreas, path))
        {
            //do safe small radius wandering if no path
            path = null;
            wanderDestination = origin + Random.insideUnitCircle*SAFE_WANDER_RADIUS;
        }
    }
}
