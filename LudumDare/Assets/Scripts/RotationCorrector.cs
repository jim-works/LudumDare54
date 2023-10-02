using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotationCorrector : MonoBehaviour
{
    public float TargetZRot;
    public float CorrectionStrength = 0.01f;
    public float DerivativeTerm = 0.01f;
    private Rigidbody2D rb;
    
    //pd control based on https://www.robotsforroboticists.com/pid-control/
    private float oldError = 0;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle = transform.rotation.eulerAngles.z;
        float error = TargetZRot-(angle > 180 ? angle-360 : angle);
        oldError = error-oldError;
        rb.AddTorque(CorrectionStrength*error+DerivativeTerm*oldError, ForceMode2D.Force);
        if (Mathf.Abs(rb.angularVelocity) > 1080) {
            rb.angularVelocity = Mathf.Sign(rb.angularVelocity)*1080;
        }
    }
}
