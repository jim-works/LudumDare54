using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Control))]
public class KeyboardMovementInput : MonoBehaviour
{
    private Control control;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
    }

    // Update is called once per frame
    void Update()
    {
        control.MoveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
