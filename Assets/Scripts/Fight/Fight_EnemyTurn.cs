using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人回合
public class Fight_EnemyTurn : FightUnit
{
    public override void Init()
    {
        //删除所有卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();
        //显示敌人回合提示
        UIManager.Instance.ShowTip("敌人回合", Color.red, delegate ()
        {
            Debug.Log("执行敌人ai");
        });
    }

    public override void OnUpdate()
    {

    }
}