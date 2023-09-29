using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float JumpHeight = 5;
    public float RunAcceleration = 1;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(new Vector2(0,JumpHeight), ForceMode2D.Impulse);
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal")*RunAcceleration,0));
    }
}
