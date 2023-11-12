using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour
{
    private ScPlayerMove movementScript;
    private ScAttack attackScript;
    private Vector2 leftJoystickDir;

    private void Start()
    {
        movementScript = GetComponent<ScPlayerMove>();
        attackScript = GetComponent<ScAttack>();
    }


    public void GetLeftJoyStickValue(InputAction.CallbackContext ctxt)
    {
        leftJoystickDir = ctxt.ReadValue<Vector2>();
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

    public void GetRighttrigger(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started) 
        {
            attackScript.AttackInstruction(true, leftJoystickDir);
        }

        if (ctxt.canceled)
        {
            attackScript.AttackInstruction(false, leftJoystickDir);
        }
    }
}
