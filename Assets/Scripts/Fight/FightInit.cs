using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

//卡牌战斗初始化
public class FightInit : FightUnit
{
    public int levelId{ get; set; } = 10001;
    public override void Init()
    {
        //初始化战斗数值
        FightManager.Instance.Init();
        //切换bgm
        AudioManager.Instance.PlayBGM("battle");
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