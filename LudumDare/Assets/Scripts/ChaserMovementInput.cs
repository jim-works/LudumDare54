using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Control))]
public class ChaserMovementInput : MonoBehaviour
{
    public Transform Chasing;
    private Control control;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
    }

    // Update is called once per frame
    void Update()
    {
        control.MoveDirection = Chasing.position-transform.position;
    }
}
