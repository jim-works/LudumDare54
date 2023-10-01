using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WalkAnimator : MonoBehaviour
{
    private const float DIR_CHANGE_EPSILON = 0.2f;
    public Sprite[] Down;
    public Sprite[] Up;
    public float DistancePerFrame = 0.5f;
    private SpriteRenderer sr;
    private int frame = 0;
    private float distanceSinceLastFrame;
    public Sprite[] currDir;
    private Vector3 prevPos;
    private Vector3 v;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currDir = Down;
    }

    void FixedUpdate()
    {
        v = (transform.position - prevPos) / Time.deltaTime;
        prevPos = transform.position;
        distanceSinceLastFrame += v.magnitude;
        UpdateFrame(GetDirection());
    }

    private Sprite[] GetDirection() {
        
        if (Mathf.Abs(v.y) >= DIR_CHANGE_EPSILON) {
            currDir = v.y <= 0 ? Down : Up;
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
