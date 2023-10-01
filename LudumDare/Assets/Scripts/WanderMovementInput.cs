using System.Collections;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Control))]
public class WanderMovementInput : MonoBehaviour
{
    private float SAFE_WANDER_RADIUS = 0.5f;
    public float WanderPauseDuration = 2f;
    //set whenever component is enabled
    private Vector2 origin;
    private Vector2 wanderDestination;
    private bool doneMoving = false;
    private IEnumerator wanderingCoroutine;
    private Control control;
    private const float MOVEMENT_EPSILON = 0.25f;
    private NavMeshPath path;
    private int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
        StartCoroutine(SetDestination());
        doneMoving = true;
        origin = transform.position;
    }

    void Update()
    {
        if (doneMoving)
        {
            control.MoveDirection = Vector2.zero;
            return;
        };
        Vector2 delta = wanderDestination - (Vector2)transform.position;
        if (delta.sqrMagnitude < MOVEMENT_EPSILON * MOVEMENT_EPSILON)
        {
            doneMoving = true;
            return;
        }
        if (path == null)
        {
            //walk in safe area if no path
            control.MoveDirection = delta;
            return;
        }
        Vector2 d = path.corners[waypointIndex] - transform.position;
        if (d.sqrMagnitude < MOVEMENT_EPSILON * MOVEMENT_EPSILON)
        {
            waypointIndex++;
            if (waypointIndex >= path.corners.Length)
            {
                doneMoving = true;

            }
            return;
        }
        control.MoveDirection = d.normalized;
    }

    private IEnumerator SetDestination()
    {
        while (true)
        {
            wanderDestination = WanderWaypointManager.Singleton.GetWanderDestination();
            Debug.Log($"wandering to {wanderDestination}");
            UpdatePath(wanderDestination);
            while (!doneMoving)
            {
                yield return new WaitForSeconds(WanderPauseDuration);
            }

        }
    }

    void UpdatePath(Vector2 destination)
    {
        path = new();
        NavMesh.SamplePosition(transform.position, out NavMeshHit hitA, 100f, NavMesh.AllAreas);
        NavMesh.SamplePosition(destination, out NavMeshHit hitB, 100f, NavMesh.AllAreas);
        if (!hitA.hit || !hitB.hit || !NavMesh.CalculatePath(hitA.position, hitB.position, NavMesh.AllAreas, path))
        {
            //do safe small radius wandering if no path
            path = null;
            Debug.LogWarning("coudn't find path to destination");
            wanderDestination = origin + Random.insideUnitCircle * SAFE_WANDER_RADIUS;
        }
        waypointIndex = 0;
        doneMoving = false;
    }
}
