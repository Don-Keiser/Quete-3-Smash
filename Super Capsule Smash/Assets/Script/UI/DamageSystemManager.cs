using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DamageSystemManager : MonoBehaviour
{
    private int simpleAttack = 5;

    [SerializeField] private TMP_Text player1DamageText;
    [SerializeField] private TMP_Text player2DamageText;

    [SerializeField] private PlayerStats p1Stats;
    [SerializeField] private PlayerStats p2Stats;

    private void Update()
    {
        p1Stats.playerDamage = Mathf.Clamp(p1Stats.playerDamage, 0, 999);
        p2Stats.playerDamage = Mathf.Clamp(p2Stats.playerDamage, 0, 999);
        player1DamageText.text = "P1: " + p1Stats.playerDamage;
        player2DamageText.text = "P2: " + p2Stats.playerDamage;
    }

    public void AddDamage(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            p1Stats.playerDamage += simpleAttack;
        }
    }

    public void AddDamage2(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            p2Stats.playerDamage += simpleAttack;
        }
    }
}
