using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObj
{
    public int wallDamage = 1;
    public int pointPerFood = 10;
    public int pointPerSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int food;

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
        //加载新关卡，这边由于就一个就填0
        SceneManager.LoadScene("SampleScene");
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("PlayerHited");
        food -= loss;
        CheckIfGameOver();
    }

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
        animator = GetComponent<Animator>();
        food = GameMgr.g_GameMgr.GetFoodPoint();

        base.Start();
    }

    //场景销毁时，进入，将需要保存的变量存入单例
    private void OnDisable()
    {
        GameMgr.g_GameMgr.SetFoodPoint(food);
    }

    private void CheckIfGameOver()
    {
        if(food <= 0)
        {
            GameMgr.g_GameMgr.GameOver();
        }
    }

    protected override void AttempMove<T>(int xDir, int yDir)
    {
        food--;
        base.AttempMove<T>(xDir, yDir);

        CheckIfGameOver();

        GameMgr.g_GameMgr.TurnOnOrOffPlayerTurn(false);
    }

    protected void TryOnMovePlayer()
    {
        int hori = (int)Input.GetAxisRaw("Horizontal");
        int vert = (int)Input.GetAxisRaw("Vertical");

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

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.g_GameMgr && !GameMgr.g_GameMgr.IsPlayerTurn())
        {
            return;
        }


        TryOnMovePlayer();
    }
}
