using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//防御卡（增加护盾）
public class DefendCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData) 
    {
        if (TryUse() == true)
        {
            //使用效果数值
            int val = int.Parse(data["Arg0"]);
            //播放使用后的音效
            AudioManager.Instance.PlayEffect("Effect/healspell");
            //增加护盾数值
            FightManager.Instance.DefenseCount += val;
            //刷新护盾文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
            Vector3 pos = Camera.main.transform.position;
            pos.y = 0;
            PlayEffect(pos);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}