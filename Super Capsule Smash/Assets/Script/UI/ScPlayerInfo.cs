using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ScPlayerInfo
{
    private int score;
    private int dammage;
    private bool isActiv;
    private TMP_Text txtScore;
    private TMP_Text txtDammage;

    public ScPlayerInfo(int score, int dammage, TMP_Text txtScore, TMP_Text txtDammage)
    {
        this.score = score;
        this.dammage = dammage;
        isActiv = true;
        this.txtDammage = txtDammage;
        this.txtScore = txtScore;
    }

    public void SetDammageText(string dammage)
    {
        txtDammage.text = dammage;
    }

    public void SetActive(bool isStillOnMap)
    {
        isActiv = isStillOnMap;
    }

    public bool IsActiv()
    {
        return isActiv;
    }

    public void IncreaseScore()
    {
        score++;
        txtScore.text = score.ToString();
    }

}
