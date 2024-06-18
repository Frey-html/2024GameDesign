using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


//敌人行动
public enum ActionType
{
    None,
    Defend,//加防御
    Attack,//攻击
}

//敌人脚本
public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//敌人数据表信息

    public ActionType type;
    public GameObject hpItemObj;
    public GameObject actionObj;
    //ui相关
    public Transform attackTf;
    public Transform defendTf;
    public Text defendTxt;
    public Text hpTxt;
    public Image hpImg;

    //数值相关
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;
    
    //组件相关
    SkinnedMeshRenderer _meshRenderer;
    public Animator ani;

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }
    void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        ani = transform.GetComponent<Animator>();

        type = ActionType.None;
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();

        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");

        defendTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();

        //设置血条和行动力位置
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down * 0.2f);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);
        SetRandomAction();
        //Hp Attack Defend 初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);

        UpdateHp();
        UpdateDefend();
    }

    //随机一个行动
    public void SetRandomAction()
    {
        int ran = Random.Range(1,3);
        type = (ActionType)ran;
        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                break;
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                break;
        }
    }

    //更新血量信息
    public void UpdateHp()
    {
        hpTxt.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //更新防御信息
    public void UpdateDefend()
    {
        defendTxt.text = Defend.ToString();
    }

    //被攻击卡选中，显示红边
    public void OnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.red);
    }

    public void OnUnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.black);
    }

    //受伤
    public void Hit(int val)
    {
        //先扣护盾
        if(Defend >= val)
        {
            Defend -= val;
            //播放受伤动画
            ani.Play("hit", 0, 0);
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            CurHp -= val;
            if(CurHp <= 0)
            {
                CurHp = 0;
                //播放死亡动画
                ani.Play("die");

                //敌人从列表中移除
                EnemyManager.Instance.DeleteEnemy(this);
                DestroyEnemy();
            }
            else
            {
                //播放受伤动画
                ani.Play("hit",0,0);
            }
        }
        //刷新血量等ui
        UpdateDefend();
        UpdateHp();
    }

    //隐藏怪物头上的行动标志
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }

    //执行敌人行动
    public IEnumerator DoAction()
    {
        HideAction();
        Debug.Log(transform.gameObject.name + "is doing action " + type);
        
        //等待某一时刻后的执行行为
        yield return new WaitForSeconds(0.5f);

        switch(type)
        {
            case ActionType.None:
                break;

            case ActionType.Defend:
                //加防御
                Defend += 1;
                UpdateDefend();
                //可以对应播放对应的特效
                break;

            case ActionType.Attack:
                //播放对应的动画
                ani.Play("attack");
                //玩家扣血
                FightManager.Instance.GetPlayerHit(Attack);
                //摄像机抖动
                Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
                break;

            default:
                break;
        }
        //等待动画播放完成
        yield return new WaitForSeconds(1);
        //继续播放待机动画
        ani.Play("idle");
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject, 1);
        Destroy(actionObj);
        Destroy(hpItemObj);
    }
}
