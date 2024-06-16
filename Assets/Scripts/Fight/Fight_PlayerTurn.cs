using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家回合
public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("playerTime");
        UIManager.Instance.ShowTip("玩家回合", Color.green, delegate ()
        {
            //回复行动力
            FightManager.Instance.CurrentPowerCount = FightManager.Instance.MaxPowerCount;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
        });
        
        // 抽卡
        Debug.Log("抽牌");
        Debug.Log("抽牌时抽牌堆数量：" + FightCardManager.Instance.cardList.Count);
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4);//抽四张牌
        
        //更新卡牌UI
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
    }

    public override void OnUpdate()
    {
        
    }
}