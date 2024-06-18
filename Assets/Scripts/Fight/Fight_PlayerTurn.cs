using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家回合
public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("playerTurn");
        //回复行动力
        FightManager.Instance.FullfillPower();
        FightManager.Instance.RemoveSheild();
        
        UIManager.Instance.ShowTip("玩家回合", Color.green);

        
        // 抽卡
        //Debug.Log("抽牌");
        //Debug.Log("抽牌时抽牌堆数量：" + FightCardManager.Instance.cardList.Count);
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(FightManager.Instance.DrawCount);//抽四张牌
        
        //更新卡牌UI
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
    }

    public override void OnUpdate()
    {
        
    }
}