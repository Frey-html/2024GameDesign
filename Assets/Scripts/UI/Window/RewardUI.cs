using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//开始界面，要继承UIBase
public class RewardUI : UIBase
{
    private void Awake()
    {
        Register("bg/choice1Btn").onClick = onSelect1Btn;
        Register("bg/choice2Btn").onClick = onSelect2Btn;
        Register("bg/choice3Btn").onClick = onSelect3Btn;
        //切换bgm
        AudioManager.Instance.PlayBGM("bgm1");
    }

    private void onSelect1Btn(GameObject obj, PointerEventData pData)
    {
        FightManager.Instance.GetHealthReward();
        Close();
        //进入下一关
        Debug.Log("Call change type to Init from RewardUI button");
        FightManager.Instance.ChangeType(FightType.Init);
    }
    private void onSelect2Btn(GameObject obj, PointerEventData pData)
    {
        FightManager.Instance.GetEnergyReward();
        Close();
        Debug.Log("Call change type to Init from RewardUI button");
        FightManager.Instance.ChangeType(FightType.Init);
    }
    private void onSelect3Btn(GameObject obj, PointerEventData pData)
    {
        FightManager.Instance.GetDrawCardReward();
        Close();
        Debug.Log("Call change type to Init from RewardUI button");
        FightManager.Instance.ChangeType(FightType.Init);
    }
}