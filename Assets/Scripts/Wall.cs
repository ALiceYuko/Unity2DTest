using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wall : MonoBehaviour
{
    public Sprite HitedSprite;
    public int hp = 5;

    private SpriteRenderer render;

    // Start is called before the first frame update
    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        render.sprite = HitedSprite;
        hp -= loss;
        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
