using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Control : MonoBehaviour
{
    public float MoveSpeed = 10;
    public float MoveAcceleration = 50;
    public Vector2 MoveDirection;
    private Rigidbody2D rb;

    const float MOVE_EPSILON = 0.01f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        DoMovement();
    }

    private void DoMovement() {
        //TODO: improve this
        float moveMag = MoveDirection.magnitude;
        if(moveMag <= MOVE_EPSILON) {
            return;
        }
        Vector2 deltaMovement = (moveMag > 1f) ? MoveDirection.normalized*MoveSpeed : MoveDirection*MoveSpeed;
        Vector2 dv = (deltaMovement-rb.velocity).normalized;

        rb.AddForce(dv*MoveAcceleration);
    }
}
