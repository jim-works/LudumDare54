using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class WalkAnimator : MonoBehaviour
{
    private const float DIR_CHANGE_EPSILON = 0.2f;
    public Sprite[] Down;
    public Sprite[] Up;
    public float DistancePerFrame = 0.5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int frame = 0;
    private float distanceSinceLastFrame;
    private Sprite[] currDir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currDir = Down;
    }

    void Update()
    {
        distanceSinceLastFrame += rb.velocity.magnitude*Time.deltaTime;
        UpdateFrame(GetDirection());
    }

    private Sprite[] GetDirection() {
        if (Mathf.Abs(rb.velocity.y) >= DIR_CHANGE_EPSILON) {
            currDir = rb.velocity.y <= 0 ? Down : Up;
        }
        return currDir;
    }
    private void UpdateFrame(Sprite[] frames) {
        if (distanceSinceLastFrame >= DistancePerFrame) {
            distanceSinceLastFrame = 0;
            frame += 1;
        }
        frame %= frames.Length;
        sr.sprite = frames[frame];
    }
}
