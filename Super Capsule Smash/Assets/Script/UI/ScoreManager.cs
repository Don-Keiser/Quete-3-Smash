using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerStats p1Stats;
    [SerializeField] private PlayerStats p2Stats;

    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;

    private void Update()
    {
        p1Stats.playerScore = Mathf.Clamp(p1Stats.playerScore, 0, 999);
        p2Stats.playerScore = Mathf.Clamp(p2Stats.playerScore, 0, 999);

        player1ScoreText.text = p1Stats.playerScore.ToString();
        player2ScoreText.text = p2Stats.playerScore.ToString();
    }

    public void AddP1Score(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            p1Stats.playerScore += 1;
        }    
    }

    public void AddP2Score(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            p2Stats.playerScore += 1;
        }
    }
}
