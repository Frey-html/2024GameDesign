using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//胜利
public class Fight_Reward : FightUnit
{
    public override void Init()
    {
        Debug.Log("奖励回合");
        //切换抽卡UI
        UIManager.Instance.CloseAllUI();
        UIManager.Instance.ShowUI<RewardUI>("RewardUI");
    }
}