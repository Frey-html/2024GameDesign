using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

// 事件监听器类，实现了IPointerClickHandler接口，用于监听鼠标点击事件
public class UIEventTrigger : MonoBehaviour, IPointerClickHandler
{
    // 事件委托，当对象被点击时触发
    public Action<GameObject, PointerEventData> onClick;

    // 静态方法，用于获取或添加UIEventTrigger组件到指定的GameObject
    public static UIEventTrigger Get(GameObject obj)
    {
        // 尝试获取GameObject上的UIEventTrigger组件
        UIEventTrigger trigger = obj.GetComponent<UIEventTrigger>();
        // 如果组件不存在，添加一个新的UIEventTrigger组件
        if (trigger == null)
        {
            trigger = obj.AddComponent<UIEventTrigger>();
        }
        // 返回UIEventTrigger组件
        return trigger;
    }

    // 实现IPointerClickHandler接口的方法，当对象被点击时调用
    public void OnPointerClick(PointerEventData eventData)
    {
        // 如果onClick事件不为空，触发事件并传递当前对象和事件数据
        if (onClick != null)
        {
            onClick(gameObject, eventData);
        }
    }
}
