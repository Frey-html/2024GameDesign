using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//失败
public class Fight_Loss : FightUnit
{
    public override void Init()
    {
        Debug.Log("游戏失败");
    }

    public override void OnUpdate()
    {
        
    }
}