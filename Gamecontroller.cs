using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;

public class Gamecontroller : MonoBehaviour
{
    public int whoTurn; // 0 = X , 1 = o
    public int turnCount;
    public GameObject[] turnIcons;
    public int[] markedSpaces; // 0 = ºóÄ­, 1 = X , 2 = O

    public Sprite[] playIcons; 
    public Button[] tictactoeSpaces;


    public TMP_Text winnerText;
    public GameObject[] winningLine;
    public GameObject winnerPanel;

    public int xPlayerScore;
    public int oPlayerScore;
    public TMP_Text oPlayerScoreText;
    public TMP_Text xPlayerScoreText;

    public Button startGameButton;

    void Start()
    {
       

        InitialSetup();
    }

    void InitialSetup()
    {
        startGameButton.gameObject.SetActive(true);

        SetTurnIcons(false, false);

        winnerText.gameObject.SetActive(false);
        winnerPanel.SetActive(false);


        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayerScoreText.text = "0";
        oPlayerScoreText.text = "0";

        ResetBoard();
        DeactivateWinningLines();
    }

    public void StartGame()
    {
        startGameButton.gameObject.SetActive(false);
        GameSetup();
    }
  

    void GameSetup()
    {
        whoTurn = Random.Range(0, 2);
        turnCount = 0;

        SetTurnIcons(whoTurn == 0, whoTurn == 1);

        ResetBoard();

    }

    void SetTurnIcons(bool xTurn, bool oTurn)
    {
        turnIcons[0].SetActive(xTurn);
        turnIcons[1].SetActive(oTurn);
    }



    void ResetBoard()
    {
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    void DeactivateWinningLines()
    {
        for (int i = 0; i < winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }
    }

    public void TicTacToeButton(int Whichnumber) 
    {
        tictactoeSpaces[Whichnumber].image.sprite = playIcons[whoTurn];
        tictactoeSpaces[Whichnumber].interactable = false;

        markedSpaces[Whichnumber] = whoTurn + 1;

        turnCount++;

        if (turnCount > 4)
        {
            WinnerCheck();
        }

        SwitchTurn();

    }

    void SwitchTurn()
    {
        if (whoTurn == 0)
        {
            whoTurn = 1;
            SetTurnIcons(false, true);
        }
        else
        {
            whoTurn = 0;
            SetTurnIcons(true, false);
        }
    }

    void WinnerCheck() 
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];
        int s8 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        
        var solution = new int[] { s1, s2,s3,s4,s5,s6,s7,s8 };

        for (int i = 0; i < solution.Length; i++) 
        {
            if (solution[i] == 3 || solution[i] == 6) // 3ÀÌ¸é X½Â , 6ÀÌ¸é O ½Â
            {
                WinnerDisplay(i);
                return;
            }

        }

    }

    void WinnerDisplay(int indexIn) 
    {
        winnerText.gameObject.SetActive(true);
        winnerPanel.gameObject.SetActive(true);

        if(whoTurn == 0) 
        {
            xPlayerScore++;
            xPlayerScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Player X Wins!";
        
        }    
        else if(whoTurn == 1) 
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Player O Wins!";
        
        }

        winningLine[indexIn].SetActive(true);

        for (int i = 0;i < tictactoeSpaces.Length; i++) // ¹öÆ° ºñÈ°¼ºÈ­
        {
            tictactoeSpaces[i].interactable = false;

        }

    }

    public void Rematch() 
    {
        GameSetup();
        DeactivateWinningLines();
        winnerPanel.SetActive(false);
    }

    public void Restart()
    {
        Rematch();

        InitialSetup();
    }

}
