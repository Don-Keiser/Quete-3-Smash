using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ScPlayerInfo
{
    private int playerNum;
    private int score;
    private int dammage;
    private bool isActiv;
    private TMP_Text txtScore;
    private TMP_Text txtDammage;

    public ScPlayerInfo(int playerNumber, int score, int dammage, TMP_Text txtScore, TMP_Text txtDammage, Color playerColor)
    {
        playerNum = playerNumber;
        this.score = score;
        this.dammage = dammage;
        isActiv = true;
        this.txtDammage = txtDammage;
        this.txtScore = txtScore;
        this.txtScore.color = playerColor;
        this.txtDammage.color = playerColor;
    }

    public void SetDammageText(string dammage)
    {
        txtDammage.text = "P"+playerNum+": "+ dammage;
    }

    public void SetActive(bool isStillOnMap)
    {
        isActiv = isStillOnMap;
    }

    public bool IsActiv()
    {
        return isActiv;
    }

    public int GetPlayerNumber()
    {
        return playerNum;
    }

    public void IncreaseScore()
    {
        score++;
        txtScore.text = score.ToString();
    }

    public void ResetDammage()
    {
        dammage = 0;
    }

}
