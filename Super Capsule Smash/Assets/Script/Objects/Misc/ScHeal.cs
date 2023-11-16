using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScHeal : ScObject
{
    [SerializeField] private int useAmount;
    [SerializeField] private ScDammage scDamage;
    private bool canUse = true;

    private void Start()
    {
        scDamage = this.transform.root.GetComponent<ScDammage>();
    }

    private void ResetHeal()
    {
        canUse = true;
    }

    private bool CanHeal(bool playerInput)
    {
        if (playerInput)
        {
            if (canUse && useAmount > 0)
            {
                useAmount--;
                canUse = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            ResetHeal();
            return false;
        }
    }

    public void HealPlayer()
    {
        scDamage.dammage -= 50;
    }

    public override void Use(bool isUsing)
    {
        if (CanHeal(isUsing))
        {

        }
    }

    private void Update()
    {
        IsBeingHeld();
    }
}
