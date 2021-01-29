using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr g_GameMgr = null;

    public float turnDelay = 0.1f;
    public BoardMgr boardMgr;
    public int playerFoodPoint = 100;

    //目前是个回合制游戏，Player和Enemy交替移动
    [HideInInspector] public bool playerTurn = true;

    private int level = 3;
    private List<Enemy> enemies;
    private bool enemiesMoving;

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
        enemies = new List<Enemy>();
        boardMgr = GetComponent<BoardMgr>();
    }

    public void InitGame()
    {
        print("liufei in InitGame");

        enemies.Clear();
        if (boardMgr)
        {
            boardMgr.SetupScene(level);
            level++;
            playerTurn = true;
        }
    }

    void Update()
    {
        if (playerTurn || enemiesMoving)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    public void ReStart()
    {
        print("liufei in ReStart");

        enemies.Clear();
        //加载新关卡
        SceneManager.LoadScene("SampleScene");
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        foreach( Enemy turn in enemies)
        {
            print("liufei in Move Enemy");

            //一次动一个，排队慢慢动
            if(turn)
            {
                turn.TryMoveEnemy();
            }
            else
            {
                playerTurn = true;
                enemiesMoving = false;
                yield break;
            }
            
            yield return new WaitForSeconds(turn.moveTime);
        }

        //所有敌人挪完之后才是Player
        playerTurn = true;
        enemiesMoving = false;
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
