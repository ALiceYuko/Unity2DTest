using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr g_GameMgr = null;

    public BoardMgr boardMgr;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
