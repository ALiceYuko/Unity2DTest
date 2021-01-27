using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr g_GameMgr = null;

    public BoardMgr boardMgr;
    public int playerFoodPoint = 100;

    //目前是个回合制游戏，Player和Enemy交替移动
    [HideInInspector] public bool playerTurn = true;

    private int level = 3;

    // Start is called before the first frame update
    void Awake()
    {
        if(g_GameMgr == null)
        {
            g_GameMgr = this;
        }
        else if(g_GameMgr != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        boardMgr = GetComponent<BoardMgr>();
        InitGame();
    }

    void InitGame()
    {
        if (boardMgr)
        {
            boardMgr.SetupScene(level);
        }
    }

    public void GameOver()
    {
        enabled = false;
    }

    public void TurnOnOrOffPlayerTurn(bool bOn)
    {
        playerTurn = bOn;
    }

    public bool IsPlayerTurn()
    {
        return playerTurn;
    }

    public int GetFoodPoint()
    {
        return playerFoodPoint;
    }

    public void SetFoodPoint(int point)
    {
        playerFoodPoint = point;
    }
}
