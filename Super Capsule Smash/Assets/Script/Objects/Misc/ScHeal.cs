using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScHeal : ScObject
{
    [SerializeField] private int healValue;

    public override void Use(bool isUsing)
    {
        dammageScript.Heal(healValue);
        DeleteOnNewRound();
        attackScript.ThrowObject();
    }

    private void Update()
    {
        IsBeingHeld();
    }
}
