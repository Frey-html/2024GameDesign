using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//胜利
public class Fight_Win : FightUnit
{
    public override void Init()
    {
        Debug.Log("战斗胜利");
        UIManager.Instance.ShowTip("战斗胜利", Color.green, delegate ()
        {
            FightManager.Instance.StopAllCoroutines();
        });
    }
}