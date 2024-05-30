using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏入口脚本
public class GameApp : MonoBehaviour
{
    //Start is called before the first frame update
    void Start()
    {
        //初始化音频管理器
        AudioManager.Instance.Init();

        //显示loginUI 创建的脚本名字记得跟预制体物体名字一致
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //播放bgm
        AudioManager.Instance.PlayBGM("bgm1");
    } 
}