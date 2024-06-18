using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

//战斗枚举
public enum FightType
{
    None,
    Init,
    Player,//玩家回合
    Enemy,//敌人回合
    Win,
    Loss,
    Reward
}

//战斗管理器
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fightUnit;//战斗单元

    [Header("PlayerProperty")]
    public int MaxHp;//最大血量
    public int CurrentHp;//当前血量
    public int MaxPowerCount;//最大能量
    public int CurrentPowerCount;
    public int DefenseCount;//防御值
    public int DrawCount; //每回合抽卡数
    public int levelId{ get; set; } = 10001; //当前关卡ID
    public FightType currentType = FightType.None;

    //初始化
    public void Init(){
        if(levelId == 10001)
        {
            MaxHp = 50;
            CurrentHp = 50;
            MaxPowerCount = 3;
            CurrentPowerCount = 3;
            DefenseCount = 10;
            DrawCount = 3;
        }else
        {
            CurrentPowerCount = MaxPowerCount;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    //切换回合（战斗类型）
    public void ChangeType(FightType type)
    {   
        currentType = type;
        switch(type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                fightUnit = new Fight_Win();
                break;
            case FightType.Loss:
                fightUnit = new Fight_Loss();
                break;
            case FightType.Reward:
                fightUnit = new Fight_Reward();
                break;
            default:
                Debug.Log("No action in curent fight unit");
                break;
        }
        fightUnit.Init();//初始化
    }

    //玩家受伤逻辑
    public void GetPlayerHit(int hit)
    {
        //扣除护盾
        if(DefenseCount >= hit)
        {
            DefenseCount -= hit;
        }
        else
        {
            hit -= DefenseCount;
            DefenseCount = 0;
            CurrentHp -= hit;
            if(CurrentHp <= 0) 
            { 
                CurrentHp = 0;
                //切换到游戏失败状态
                ChangeType(FightType.Loss);
            }
        }

        //更新界面
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
    }

    private void Update()
    {
        if(fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }

    public void GetHealthReward()
    {
        MaxHp += 20;
        CurrentHp += 20;
    }

    public void GetEnergyReward()
    {
        MaxPowerCount += 1;
    }

    public void GetDrawCardReward()
    {
        DrawCount += 1;
    }

    public void FullfillPower()
    {
        CurrentPowerCount = MaxPowerCount;
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
    }

    public void RemoveSheild()
    {
        DefenseCount = 0;
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
    }
}