using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PBMainAttribute
{
    public int damge; //伤害
    public int armor; //护甲
    public int hpMax;
    private int hp;

    public void Hurt(int point)
    {
        hp = Math.Max(0, hp - point);
    }

    public void Restore(int point)
    {
        hp = Math.Min(hpMax, hp + point);
    }

    public bool IsDead()
    {
        return hp == 0;
    }
}

[Serializable]
public class PBMoveConfig
{
    public int moveRange;
    public int attackRange;
}
