using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor.PackageManager;
using UnityEngine;

//卡牌战斗初始化
public class FightInit : FightUnit
{
    public override void Init()
    {
        //初始化战斗数值
        FightManager.Instance.Init();
        //切换bgm
        AudioManager.Instance.PlayBGM("battle");
        //获得当前关卡序号
        int levelId = FightManager.Instance.levelId;
        
        //敌人生成
        EnemyManager.Instance.LoadRes(levelId.ToString());//读取对应关卡敌人信息
        FightCardManager.Instance.Init();
        //显示战斗界面
        UIManager.Instance.ShowUI<FightUI>("FightUI");
        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}