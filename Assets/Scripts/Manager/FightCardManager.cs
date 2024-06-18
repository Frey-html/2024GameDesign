using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//战斗卡牌管理器
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//卡堆集合

    public List<string> usedCardList;//弃牌堆

    //初始化
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();  

        //定义临时集合
        List<string> tempList= new List<string>();  
        //将玩家的卡牌存储到临时集合
        tempList.AddRange(RoleManager.Instance.cardList);

        while(tempList.Count > 0)
        {
            //随即下标
            int tempIndex = Random.Range(0,tempList.Count);

            //添加到卡堆
            cardList.Add(tempList[tempIndex]);

            //临时集合删除
            tempList.RemoveAt(tempIndex);
        }

        //Debug.Log(cardList.Count);
    }

    //是否有卡
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //抽卡
    public string DrawCard()
    {
        // 检查列表是否为空，如果是则洗牌
        if (cardList.Count == 0)
        {
            Shuffle();
        }
        int randomIndex = Random.Range(0, cardList.Count);
        //Debug.Log("从牌堆中抽到卡牌序号：" + randomIndex);
        string id = cardList[randomIndex];
        cardList.RemoveAt(randomIndex);
        return id;
    }

    public void Shuffle()
    {
        List<string> temp;
        temp = cardList;
        cardList = usedCardList;
        usedCardList = temp;
    }
}


