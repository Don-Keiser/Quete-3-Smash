using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour
{
    private ScPlayerMove movementScript;

    private void Start()
    {
        movementScript = GetComponent<ScPlayerMove>();
    }


    public void GetLeftJoyStickValue(InputAction.CallbackContext ctxt)
    {
       movementScript.MoveLeftAndRIght(ctxt.ReadValue<Vector2>().x);
    }
}
