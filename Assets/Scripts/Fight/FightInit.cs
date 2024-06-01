using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//卡牌战斗初始化
public class FightInit : FightUnit
{
    public override void Init()
    {
        //切换bgm
        AudioManager.Instance.PlayBGM("battle");
        //显示战斗界面
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        //敌人生成
        EnemyManager.Instance.LoadRes("10003");//此处读取关卡3的敌人信息
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}