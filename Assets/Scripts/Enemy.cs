using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObj
{
    public int playerDamage = 20;

    private Animator animator;
    private Transform target; //记录玩家位置
    private bool bSkipMove; //为了和间隔帧移动

    protected override void OnCanMove<T>(T component)
    {
        Player player = component as Player;
        if (player)
        {
            player.LoseFood(playerDamage);
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    protected override void AttempMove<T>(int xDir, int yDir)
    {
        if (bSkipMove)
        {
            bSkipMove = false;
            return;
        }

        base.AttempMove<T>(xDir, yDir);
        bSkipMove = true;
    }

    //简单规则移动敌人
    public void TryMoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        //每次就移动一个方向
        if(Mathf.Abs(transform.position.x - target.position.x) < float.Epsilon)
        {
            yDir = transform.position.y > target.position.y ? -1:1;
        }else
        {
            xDir = transform.position.x > target.position.x ? -1 : 1;
        }

        AttempMove<Player>(xDir, yDir);

        //GameMgr.g_GameMgr.TurnOnOrOffPlayerTurn(true);
    }
}
