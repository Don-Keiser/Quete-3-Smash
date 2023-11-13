using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<TMP_Text> playersScore = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> playersDammage = new List<TMP_Text>();

    private Dictionary<ScDammage, ScPlayerInfo> playersDic = new Dictionary<ScDammage, ScPlayerInfo>();

    int activPlayerOnMap;

    public void AddPlayer(ScDammage playerInfo)
    {
        if (!playersDic.ContainsKey(playerInfo))
        {
            playersDic.Add(playerInfo, new ScPlayerInfo(0,0, playersScore[playersDic.Count], playersDammage[playersDic.Count]));
            activPlayerOnMap++;
        }
    }

    public void UpdateDammageValue(ScDammage playerToUpdate, int dammageValue)
    {
        if (playersDic.ContainsKey(playerToUpdate))
        {
            playersDic[playerToUpdate].SetDammageText( dammageValue.ToString()) ;
        }
    }

    public void PlayerOut(ScDammage playerOut)
    {
        ScPlayerInfo lastActiv;
        int activPlayerCount = 0;
        playersDic[playerOut].SetActive(false) ;
        foreach  ( KeyValuePair<ScDammage, ScPlayerInfo> player in playersDic)
        {
            if (player.Value.IsActiv())
            {
                activPlayerCount ++;
                lastActiv = player.Value ;
            }

        }
        if (activPlayerCount == 1)
        {//need to update the lastActiv score
            Debug.Log("quelqu'un à gagné let's Go");
        }
    }

}
