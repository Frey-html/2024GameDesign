using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//胜利
public class Fight_Win : FightUnit
{
    public override void Init()
    {
        Debug.Log("战斗胜利");
        
        //增加关卡序号，准备切换到下一关
        int levelId = FightManager.Instance.levelId + 1;
        FightManager.Instance.levelId = levelId;
        if(levelId > GameConfigManager.Instance.GetMaxLevelId())
        {
            UIManager.Instance.ShowTip("游戏胜利", Color.green, delegate ()
            {
                FightManager.Instance.StopAllCoroutines();
                //返回主界面
                //切换UI
                UIManager.Instance.CloseAllUI();
                UIManager.Instance.ShowUI<LoginUI>("LoginUI");
                //播放bgm
                AudioManager.Instance.PlayBGM("bgm1");
            });
        }else
        {
            UIManager.Instance.ShowTip("战斗胜利", Color.green, delegate ()
            {
                FightManager.Instance.StopAllCoroutines();
                Debug.Log("Call change type to Reward from Init() in FightWin");
                FightManager.Instance.ChangeType(FightType.Reward);
            });
        }
    }
}