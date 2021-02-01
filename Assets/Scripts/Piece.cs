using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//可控制逻辑棋子的基类，可以切换所有者（本游戏是个战棋）
public class Piece : MovingObj
{
    public PieceBaseMainAttribute mainAttribute;
    public PieceBaseMoveConfig moveConfig;

    // Start is called before the first frame update
    //场景进入时进行赋值，保留上一次的结果
    protected override void Start()
    {
        base.Start();
    }

    protected void Awake()
    {
    }

    //场景销毁时，进入，将需要保存的变量存入单例
    protected void OnDisable()
    {
 
    }

    protected void OnEnable()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// /////////////////////////////////////////////////////////////////////////////////////////////


    protected override void OnCanMove<T>(T component)
    {
        
    }
    
}
