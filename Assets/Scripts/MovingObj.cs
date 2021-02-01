using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

可移动物体的基类 

*/
public abstract class MovingObj : MonoBehaviour
{
    public float moveTime = 0.1f;
    public LayerMask blockingMaskLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D ribd;
    private float inverseMoveTime;

    // Start is called before the first frame update
    protected virtual  void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        ribd = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime; //类似单位间隔的移动速度

    }

    //C#的迭代器，外部会持续调用。这个相当于是个状态机，持续按帧移动单位距离
    protected IEnumerator SmootMovement( Vector3 end)
    {
        //比较的是物体位置，但实际尝试移动的是刚体，因为不一定能动啊
        float sqrRemainDis = (transform.position - end).sqrMagnitude;

        while (sqrRemainDis > float.Epsilon)
        {
            Vector3 newPos = Vector3.MoveTowards(ribd.position, end, inverseMoveTime * Time.deltaTime);
            ribd.MovePosition(new Vector2(newPos.x, newPos.y));
            sqrRemainDis = (transform.position - end).sqrMagnitude;

            yield return null; //迭代器执行完一次，咱们这个就是为了位移，不需要返回什么实际值
        }
    }

    //out代表这个参数是个引用
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        //似乎这个物理调用有可能碰到自身的碰撞盒子，下面三句相当于做了次检测
        boxCollider.enabled = false; 
        hit = Physics2D.Linecast(start, end, blockingMaskLayer);
        boxCollider.enabled = true;

        if(hit.transform == null) //没碰到任何物体
        {
            StartCoroutine(SmootMovement(end));
            return true;
        }

        return false;
    }


    //由于继承MovingObj的对象包括怪物和Player,两者处理碰撞时的需求是不一致的所以需要继承重写。
    //当然这份代码的前提是二者的普通移动是一样的，并且共用一些参数和接口
    protected abstract void OnCanMove<T>(T component)
     where T : Component;

    //尝试移动
    protected virtual void AttempMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool bMovedNoHit = Move(xDir, yDir,out hit);
        if (bMovedNoHit)
        {
            return;
        }

        //遇到了碰撞物体，需要执行OnCanMove
        T hitComponent = hit.transform.GetComponent<T>();
        if(!bMovedNoHit && hitComponent != null)
        {
            OnCanMove(hitComponent);
        }
    }
}
