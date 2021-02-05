using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr g_GameMgr = null;

    public float turnDelay = 0.1f;
    public BoardMgr boardMgr;

    //目前是个回合制游戏，Player和Enemy交替移动
    [HideInInspector] private bool playerTurn = true;

    public bool PlayerTurn
    {
        get;
        set;
    }

    private int level = 3;
    /**
     选中的棋子
    */
    private GameObject selected;

    public GameObject Selected
    {
        get { return selected; }
        set
        {
            if (selected != null)
            {
                selected.GetComponent<Piece>().Selected = false;
            }
            selected = value;

            if (value != null)
            {
                value.GetComponent<Piece>().Selected = true;
            }

        }
    }

    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////


    // Start is called before the first frame update
    void Awake()
    {
        if (g_GameMgr == null)
        {
            g_GameMgr = this;
        }
        else if (g_GameMgr != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        boardMgr = GetComponent<BoardMgr>();
    }

    public void InitGame()
    {
        print("liufei in InitGame");

        if (boardMgr)
        {
            boardMgr.SetupScene(level);
            level++;
            playerTurn = true;
        }
    }

    void Update()
    {
        if (playerTurn)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    public void ReStart()
    {
        print("liufei in ReStart");

        //加载新关卡
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator MoveEnemies()
    {
        yield return null;
    }

    public void GameOver()
    {
        enabled = false;
    }

    public void ShowMoveRange()
    {

    }
}

