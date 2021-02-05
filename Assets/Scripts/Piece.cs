using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using OwnerTeamID=System.Int32;

//可控制逻辑棋子的基类，可以切换所有者（本游戏是个战棋）
//Unity这些Start接口本身属于外层业务，不要被继承多次，顶多一次
public class Piece : MovingObj
{
    public PBMainAttribute mainAttribute;
    public PBMoveConfig moveConfig;
    public eTeamType teamType;

    protected Animator animator;
    protected PlayerCtrl inputCtrl;
    protected Material materialSprite;

    private bool selected;
    public bool Selected
    {
        get { return selected; }
        set
        {
            Debug.Log("selected: " + value);
            if (selected == value)
            {
                return;
            }
            selected = value;


            //float outlineEnabled = value ? 1 : 0;
            //outlineMaterial.SetFloat(keyOutlineEnabled, outlineEnabled);
        }
    }

    /**
   轮廓线开关key
   */
    protected const string keyOutlineEnabled = "_OutlineEnabled";

    /**
    轮廓线颜色key
    */
    protected const string keyOutlineColor = "_SolidOutline";

    //轮廓线颜色
    private Color enemyHoverColor = new Color(1, 0, 0, 0.5f);
    private Color enemySelectedColor = new Color(1, 0.92f, 0.016f, 0.5f);
    private Color hoverColor = new Color(0, 1, 0, 0.5f);
    private Color selectedColor = new Color(1, 1, 1, 0.5f);
    private Dictionary<eTeamType, Color> dictHoverColor = new Dictionary<eTeamType, Color>();
    private Dictionary<eTeamType, Color> dictSelectColor = new Dictionary<eTeamType, Color>();

    /// ///////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    //场景进入时进行赋值，保留上一次的结果
    protected virtual void Start()
    {
        
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        inputCtrl = new PlayerCtrl();

        materialSprite = GetComponent<SpriteRenderer>().material;

        dictHoverColor.Add(eTeamType.TEAM_OWNER, hoverColor);
        dictHoverColor.Add(eTeamType.TEAM_ENEMY, enemyHoverColor);

        dictSelectColor.Add(eTeamType.TEAM_OWNER, selectedColor);
        dictSelectColor.Add(eTeamType.TEAM_ENEMY, enemySelectedColor);

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

    protected void OnBasePointerClick(PointerEventData eventData)
    {
        GameMgr.g_GameMgr.Selected = this.gameObject;
        GameMgr.g_GameMgr.ShowMoveRange();
    }

    protected void OnBasePointerEnter(PointerEventData eventData)
    {
        Color curColor = materialSprite.GetColor(keyOutlineColor);

        if(dictHoverColor.ContainsKey(teamType))
        {
            Color hoverColor = dictHoverColor[teamType];
            if(curColor != hoverColor)
            {
                materialSprite.SetColor(keyOutlineColor, hoverColor);
            }
        }
     
        materialSprite.SetFloat(keyOutlineEnabled, 1.0f);
    }

    protected void OnBasePointerExit(PointerEventData eventData)
    {
        if (selected)
        {
            Color curColor = materialSprite.GetColor(keyOutlineColor);

            if (dictSelectColor.ContainsKey(teamType))
            {
                Color selectColor = dictSelectColor[teamType];
                if (curColor != selectColor)
                {
                    materialSprite.SetColor(keyOutlineColor, selectColor);
                }
            }

        }else
        {
            materialSprite.SetFloat(keyOutlineEnabled, 0);
        }
    }

    /// /////////////////////////////////////////////////////////////////////////////////////////////


    protected override void OnCanMove<T>(T component)
    {
        
    }
}
