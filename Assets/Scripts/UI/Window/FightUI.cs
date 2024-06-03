using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//战斗界面
public class FightUI : UIBase
{
    private Text cardCountTxt;//卡牌数量
    private Text noCardCountTxt;//弃牌堆数量
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt;//防御数值
    private List<CardItem> cardItemList;//存储卡牌实体集合

    private void Awake()
    {
        cardItemList = new List<CardItem>();
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        noCardCountTxt = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/hpTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }

    //更新血量提示
    public void UpdateHp()
    {
        hpTxt.text = FightManager.Instance.CurrentHp + "/" + FightManager.Instance.MaxHp;
        hpImg.fillAmount = (float)FightManager.Instance.CurrentHp / (float)FightManager.Instance.MaxHp;
    }

    //更新能量
    public void UpdatePower()
    {
        powerTxt.text = FightManager.Instance.CurrentPowerCount + "/" + FightManager.Instance.MaxPowerCount;
    }

    //防御更新
    public void UpdateDefense()
    {
        fyTxt.text = FightManager.Instance.DefenseCount.ToString();
    }

    //更新卡牌数量
    public void UpdateCardCount()
    {
        cardCountTxt.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    //更新弃牌堆数量
    public void UpdateUsedCardCount() 
    {
        noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }

    //创建卡牌实体
    public void CreateCardItem(int count)
    {
        if(count > FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;
        }
        for(int i = 0; i < count;i++){
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new UnityEngine.Vector2(-1000, -700);
            //var item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);
        }
    }

    //更新卡牌位置
    // 更新卡牌项的位置
    public void UpdateCardItemPos()
    {
        // 计算每张卡牌之间的水平偏移量，使它们在水平方向上均匀分布
        float offset = 800.0f / cardItemList.Count;
        // 计算起始位置，使卡牌项在水平方向上居中显示，并且位于屏幕底部
        UnityEngine.Vector2 startPos = new UnityEngine.Vector2(
            -cardItemList.Count / 2.0f * offset + offset * 0.5f, // X 坐标
            -700); // Y 坐标
        // 遍历卡牌项列表，逐个更新它们的位置
        for(int i = 0; i < cardItemList.Count; i++)
        {
            // 使用 DOTween 库中的 DOAnchorPos 方法来动画地更新卡牌项的锚点位置
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 1.0f);
            // 更新起始位置的 X 坐标，以便下一个卡牌项可以正确地排列在其右侧
            startPos.x = startPos.x + offset;
        }
    }
}
