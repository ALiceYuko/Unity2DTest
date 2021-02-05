using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//sealed禁止继承，类似于java final
sealed public class PieceHero : Piece, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public float restartLevelDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //重新加载关卡
        if (collision.tag == "Exit")
        {
            Invoke("ReStart", restartLevelDelay);
            enabled = false;
        } else if (collision.tag == "Food")
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Soda")
        {
            collision.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    //场景进入时进行赋值，保留上一次的结果
    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    //场景销毁时，进入，将需要保存的变量存入单例
    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (GameMgr.g_GameMgr && !GameMgr.g_GameMgr.PlayerTurn)
        {
            return;
        }

        base.Update();
    }


    /// /////////////////////////////////////////////////////////////////////////////////////////////

    void CheckIfGameOver()
    {
        if(mainAttribute.IsDead())
        {
            GameMgr.g_GameMgr.GameOver();
            animator.SetTrigger("BoyDead");
        }
    }

    protected override void AttempMove<T>(int xDir, int yDir)
    {
        base.AttempMove<T>(xDir, yDir);

        CheckIfGameOver();

        GameMgr.g_GameMgr.PlayerTurn = false;
    }

    protected override void OnCanMove<T>(T component)
    {

    }

    private void ReStart()
    {
        GameMgr.g_GameMgr.ReStart();
    }

    public void LoseHp(int loss)
    {
        animator.SetTrigger("BoyHited");
        mainAttribute.Hurt(loss);
        CheckIfGameOver();
    }

    void TryOnMovePlayer()
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

    public  void OnPointerClick(PointerEventData eventData)
    {
        base.OnBasePointerClick(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        base.OnBasePointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        base.OnBasePointerExit(eventData);
    }
}
