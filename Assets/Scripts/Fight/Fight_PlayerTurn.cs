using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家回合
public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        //Debug.Log("playerTime");
        UIManager.Instance.ShowTip("玩家回合", Color.green, delegate ()
        {
            Debug.Log("抽牌");
        });
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4);//抽四张牌
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
    }

    public override void OnUpdate()
    {
        
    }
}