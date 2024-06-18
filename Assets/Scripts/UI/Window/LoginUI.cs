using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//开始界面，要继承UIBase
public class LoginUI : UIBase
{
    private void Awake()
    {
        //开始游戏
        Register("bg/startBtn").onClick = onStartGameBtn;
        Register("bg/quitBtn").onClick = onQuitGameBtn;
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData)
    {
        //关闭login界面
        Close();

        //战斗初始化
        Debug.Log("Call change type to Init from Init() in LoginUI");
        //重新初始化关卡与战斗数据（保证退出到login界面后重新开始游戏加载正常）
        FightManager.Instance.Init();
        FightManager.Instance.ChangeType(FightType.Init);
    }

    private void onQuitGameBtn(GameObject obj, PointerEventData pData)
     {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}