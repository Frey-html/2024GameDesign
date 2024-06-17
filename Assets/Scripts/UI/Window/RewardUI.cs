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
    }
    private void onSelect2Btn(GameObject obj, PointerEventData pData)
    {
    }
    private void onSelect3Btn(GameObject obj, PointerEventData pData)
    {
    }

}