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
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
        updatePathInterval = UpdatePathInterval + UpdatePathIntervalFuzz*(Random.value-0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        TryUpdatePath();
        if (path == null)
        {
            //just move toward target if no path
            control.MoveDirection = Chasing.position - transform.position;
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

    void TryUpdatePath()
    {
        if (Time.time - lastPathUpdateTime < updatePathInterval) {
            return;
        }
        path = new();
        NavMesh.SamplePosition(transform.position, out NavMeshHit hitA, 10f, NavMesh.AllAreas);
        NavMesh.SamplePosition(Chasing.position, out NavMeshHit hitB, 10f, NavMesh.AllAreas);
        if (!NavMesh.CalculatePath(hitA.position, hitB.position, NavMesh.AllAreas, path))
        {
            Debug.LogWarning("Couldn't calculate path");
            //just move toward target if no path
            path = null;
        }
    }
}
