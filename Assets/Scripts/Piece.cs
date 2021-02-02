using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OwnerTeamID=System.Int32;

//可控制逻辑棋子的基类，可以切换所有者（本游戏是个战棋）
//Unity这些Start接口本身属于外层业务，不要被继承多次，顶多一次
public class Piece : MovingObj
{
    public PBMainAttribute mainAttribute;
    public PBMoveConfig moveConfig;
    public OwnerTeamID teamID;

    protected Animator animator;
    protected PlayerCtrl inputCtrl;

    // Start is called before the first frame update
    //场景进入时进行赋值，保留上一次的结果
    protected virtual void Start()
    {
        
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        inputCtrl = new PlayerCtrl();
        base.InitComponent();
    }

    //场景销毁时，进入，将需要保存的变量存入单例
    protected virtual void OnDisable()
    {
        inputCtrl.Disable();
    }

    protected virtual void OnEnable()
    {
        inputCtrl.Enable();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }


    /// /////////////////////////////////////////////////////////////////////////////////////////////


    protected override void OnCanMove<T>(T component)
    {
        
    }
    
}
