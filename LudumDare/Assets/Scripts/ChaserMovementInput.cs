using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Control))]
public class ChaserMovementInput : MonoBehaviour
{
    public Transform Chasing;
    public float UpdatePathInterval = 1.0f;
    [Tooltip("random variation on pathing calculation to not cause lag spikes from all updating at once. adds random from [-x/2,x/2]")]
    public float UpdatePathIntervalFuzz = 0.6f;
    private Control control;
    private NavMeshPath path;
    private float updatePathInterval;
    private float lastPathUpdateTime;
    private int waypointIndex;
    private const float MOVEMENT_EPSILON = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
        updatePathInterval = UpdatePathInterval + UpdatePathIntervalFuzz*(Random.value-0.5f);
    }

    void OnEnable()
    {
        lastPathUpdateTime = -updatePathInterval;
        TryUpdatePath();
    }

    // Update is called once per frame
    void Update()
    {
        TryUpdatePath();
        if (path == null)
        {
            control.MoveDirection = Vector2.zero;
            return;
        }
        if (waypointIndex >= path.corners.Length)
        {
            //just move toward target if no path
            control.MoveDirection = Chasing.position - transform.position;
            return;
        }
        Vector2 d = path.corners[waypointIndex] - transform.position;
        if (d.sqrMagnitude < MOVEMENT_EPSILON * MOVEMENT_EPSILON)
        {
            waypointIndex++;
            if (waypointIndex >= path.corners.Length)
            {
                control.MoveDirection = Chasing.position - transform.position;

            }
            return;
        }
        control.MoveDirection = d.normalized;
    }

    void TryUpdatePath()
    {
        if (Time.time - lastPathUpdateTime < updatePathInterval) {
            return;
        }
        waypointIndex = 0;
        path = new();
        NavMesh.SamplePosition(transform.position, out NavMeshHit hitA, 10f, NavMesh.AllAreas);
        NavMesh.SamplePosition(Chasing.position, out NavMeshHit hitB, 10f, NavMesh.AllAreas);
        lastPathUpdateTime = Time.time;
        if (!NavMesh.CalculatePath(hitA.position, hitB.position, NavMesh.AllAreas, path))
        {
            //just move toward target if no path
            path = null;
        }
    }
}
