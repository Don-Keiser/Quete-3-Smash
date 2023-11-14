using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] List<TMP_Text> playersScore = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> playersDammage = new List<TMP_Text>();
    [SerializeField] Animator playerWonTextAnim = new Animator();
    [SerializeField] TMP_Text playerWonTxt;

    private Dictionary<ScDammage, ScPlayerInfo> playersDic = new Dictionary<ScDammage, ScPlayerInfo>();

    public UnityEvent<int> newRound;
    public UnityEvent roundOver;

    private bool weGotAWinner; //this prevent the code to do anything if the last player decide to kill himself after winning

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }

    }
    private void Start()
    {
        weGotAWinner = false;
        if (newRound == null)
            newRound = new UnityEvent<int>();

        if (roundOver == null)
            roundOver = new UnityEvent();
    }

    public void AddPlayer(ScDammage playerInfo, Transform playerTrans)
    {
        if (!playersDic.ContainsKey(playerInfo))
        {
            playersDic.Add(playerInfo, new ScPlayerInfo(playersDic.Count+1, 0,0, playersScore[playersDic.Count], playersDammage[playersDic.Count], playerTrans.GetComponent<ScPlayerMove>().myColor));
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
        if (!weGotAWinner)
        {
            ScDammage lastActiv = null;
            int activPlayerCount = 0;
            playersDic[playerOut].SetActive(false);
            foreach (KeyValuePair<ScDammage, ScPlayerInfo> player in playersDic)
            {
                if (player.Value.IsActiv())
                {
                    activPlayerCount++;
                    lastActiv = player.Key;
                }
            }
            if (activPlayerCount == 1)
            {
                playerWonTxt.text = playersDic[lastActiv].GetPlayerNumber().ToString();
                playersDic[lastActiv].IncreaseScore();
                playerWonTextAnim.SetBool("playerWon", true);
                Invoke("StartANewRound", 1);
                roundOver.Invoke();
            }//need to update the lastActiv score
        }
    }

    private void StartANewRound()
    {
        newRound.Invoke(2);
        Invoke("ResetWinnerAnim", 2);
        foreach (KeyValuePair<ScDammage, ScPlayerInfo> player in playersDic)
        {
            playersDic[player.Key].SetActive(true);
        }
        weGotAWinner = false;
    }

    private void ResetWinnerAnim()
    {
        playerWonTextAnim.SetBool("playerWon", false);
    }
}
