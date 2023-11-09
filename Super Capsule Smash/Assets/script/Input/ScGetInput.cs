using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
       movementScript.LeftJoystick(ctxt.ReadValue<Vector2>());

    }

    public void GetSouthButon(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started)
        {
            movementScript.JumpInstruction(true);
        }
        if (ctxt.canceled) 
        {
            movementScript.JumpInstruction(false);
        }
    }
}
