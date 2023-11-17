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
    private Vector2 rightJoystickDir;
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

        if (shieldscript != null && rightJoystickDir != Vector2.zero)
            shieldscript.SetshieldDirection(ctxt.ReadValue<Vector2>());
    }
    public void GetRightJoyStickValue(InputAction.CallbackContext ctxt)
    {
        if (movementScript != null && canGetInput)
        {
            rightJoystickDir = ctxt.ReadValue<Vector2>();
            movementScript.RightJoystick(ctxt.ReadValue<Vector2>());
        }

        if (shieldscript != null)
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

    public void GetLeftShoulder(InputAction.CallbackContext ctxt)
    {
        if (attackScript != null && canGetInput)
        {
            if (ctxt.started)
            {
                attackScript.DropTheObject();
            }
        }
    }

    public void CanGetInput(bool activ)
    {
        canGetInput = activ;

        if (canGetInput)
        {
            movementScript.LeftJoystick(Vector2.zero);
            shieldscript.SetshieldDirection(Vector2.zero);
        }//reset all the inputValues
    }
}
