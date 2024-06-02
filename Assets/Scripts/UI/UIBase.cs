using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//界面基类
// 基础UI类，继承自MonoBehaviour
public class UIBase : MonoBehaviour
{
    // 注册事件，返回UIEventTrigger对象
    public UIEventTrigger Register(string name)
    {
        // 查找当前UI对象的子对象
        Transform tf = transform.Find(name);
        // 获取或添加UIEventTrigger组件到找到的子对象
        return UIEventTrigger.Get(tf.gameObject);
    }

    // 显示UI，虚方法可以被子类重写
    public virtual void Show()
    {
        // 设置当前游戏对象为激活状态
        gameObject.SetActive(true);
    }

    // 隐藏UI，虚方法可以被子类重写
    public virtual void Hide()
    {
        // 设置当前游戏对象为非激活状态
        gameObject.SetActive(false);
    }

    // 关闭UI，虚方法可以被子类重写
    public virtual void Close()
    {
        // 调用UIManager的实例，关闭当前游戏对象的UI
        UIManager.Instance.CloseUI(gameObject.name);
    }
}
