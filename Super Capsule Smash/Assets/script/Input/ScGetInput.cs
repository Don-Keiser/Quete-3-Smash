using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour
{
    private ScPlayerMove movementScript;
    private ScAttack attackScript;
    private ScTempoShield shieldscript;
    private Vector2 leftJoystickDir;
    private bool canGetInput;

    private void Start()
    {
        movementScript = GetComponent<ScPlayerMove>();
        attackScript = GetComponent<ScAttack>();
        shieldscript = GetComponent<ScTempoShield>();
        canGetInput = true;
    }


    public void GetLeftJoyStickValue(InputAction.CallbackContext ctxt)
    {
        if (movementScript != null && canGetInput)
        {
            leftJoystickDir = ctxt.ReadValue<Vector2>();
            movementScript.LeftJoystick(ctxt.ReadValue<Vector2>());
        }

        shieldscript.SetshieldDirection(ctxt.ReadValue<Vector2>());
    }

    public void GetSouthButon(InputAction.CallbackContext ctxt)
    {
        if (movementScript != null && canGetInput)
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

    public void GetRighttrigger(InputAction.CallbackContext ctxt)
    {
        if (attackScript != null && canGetInput)
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

    public void GetLeftTrigger(InputAction.CallbackContext ctxt)
    {
        if (shieldscript != null && canGetInput)
        {
            if (ctxt.started)
            {
                shieldscript.SetShieldActiv(true);
            }

            if (ctxt.canceled)
            {
                shieldscript.SetShieldActiv(false);
            }
        }
    }

    public void CanGetInput(bool activ)
    {
        canGetInput = activ;
    }
}
