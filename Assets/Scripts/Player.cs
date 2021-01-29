﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MovingObj
{
    public int wallDamage = 1;
    public int pointPerFood = 10;
    public int pointPerSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int food;
    private PlayerCtrl inputCtrl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //重新加载关卡
        if (collision.tag == "Exit")
        {
            Invoke("ReStart", restartLevelDelay);
            enabled = false;
        } else if (collision.tag == "Food")
        {
            food += pointPerFood;
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Soda")
        {
            food += pointPerSoda;
            collision.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    //场景进入时进行赋值，保留上一次的结果
    protected override void Start()
    {
        food = GameMgr.g_GameMgr.GetFoodPoint();

        base.Start();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputCtrl = new PlayerCtrl();
        //可以绑定一些回调函数，执行某些Input反馈
    }

    //场景销毁时，进入，将需要保存的变量存入单例
    private void OnDisable()
    {
        GameMgr.g_GameMgr.SetFoodPoint(food);
        inputCtrl.Disable();
    }

    private void OnEnable()
    {
        inputCtrl.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.g_GameMgr && !GameMgr.g_GameMgr.IsPlayerTurn())
        {
            return;
        }


        TryOnMovePlayer();

        TryOnPlayerAttack();
    }


    /// /////////////////////////////////////////////////////////////////////////////////////////////

    private void CheckIfGameOver()
    {
        if(food <= 0)
        {
            GameMgr.g_GameMgr.GameOver();
            animator.SetTrigger("PlayedDead");
        }
    }

    protected override void AttempMove<T>(int xDir, int yDir)
    {
        food--;
        base.AttempMove<T>(xDir, yDir);

        CheckIfGameOver();

        GameMgr.g_GameMgr.TurnOnOrOffPlayerTurn(false);
    }

    protected override void OnCanMove<T>(T component)
    {
        Wall hitwall = component as Wall;
        if (hitwall)
        {
            hitwall.DamageWall(wallDamage);
        }
    }

    private void ReStart()
    {
        GameMgr.g_GameMgr.ReStart();
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("PlayerHited");
        food -= loss;
        CheckIfGameOver();
    }

    protected void TryOnMovePlayer()
    {
        Vector2 move = inputCtrl.Player.Move.ReadValue<Vector2>();

        int hori = (int)move.x;
        int vert = (int)move.y;

        //目前没有斜着的动画，不允许斜着
        if (hori != 0)
        {
            vert = 0;
        } 

        if (hori != 0 || vert != 0)
        {
            AttempMove<Wall>(hori, vert);
        }
    }

    protected void TryOnPlayerAttack()
    {

    }

    
}
