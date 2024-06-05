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
            //卡堆已经没有卡 将弃牌堆卡置入抽牌堆
            if(FightCardManager.Instance.HasCard() == false)
            {   
                //直接交换抽排队与弃牌堆的引用
                List<string> temp;
                temp = FightCardManager.Instance.cardList;
                FightCardManager.Instance.cardList = FightCardManager.Instance.usedCardList;
                FightCardManager.Instance.usedCardList = temp;
                //更新卡堆UI
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
            }
        });
        
        // 抽卡
        Debug.Log("抽牌");
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4);//抽四张牌
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

        //更新卡牌数
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
    }

    public override void OnUpdate()
    {
        
    }
}