using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }
    void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();

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
}
