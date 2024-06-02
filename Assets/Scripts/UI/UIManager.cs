using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

//ui管理器
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTf;//画布的变换组件

    private List<UIBase> uiList;


    //关闭界面
    public virtual void Awake()
    {
        Instance = this;
        //找世界中的画布
        canvasTf = GameObject.Find("Canvas").transform;
        //初始化集合
        uiList = new List<UIBase>();
    }

    public UIBase ShowUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if (ui == null)
        {
            //集合中没有 需要从Resources/UI文件夹中更新
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName) , canvasTf) as GameObject;
            //改名字
            obj.name = uiName;
            //添加需要的脚本
            ui = obj.AddComponent<T>();
            //添加到集合进行储存
            uiList.Add(ui);
        }
        else
        {
            //显示
            ui.Show();
        }
        return ui;
    }

    //隐藏
    public void HideUI(string uiName)
    {
        UIBase ui= Find(uiName);
        if(ui!=null)
        {
            ui.Hide();
        }
    }

    //关闭所有界面
    public void CloseAllUI()
    {
        for(int i=uiList.Count-1;i>=0;i--)
        {
            Destroy(uiList[i].gameObject);
        }

        uiList.Clear();//清空集合
    }


    //关闭某个界面
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui!=null)
        {
            uiList.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    //从集合中找到名字对应的界面脚本
     public UIBase Find(string uiName)
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name == uiName) return uiList[i];
        }
        return null;
    }

    //获得界面的脚本
    public T GetUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if(ui != null){
            return ui.GetComponent<T>();
        }
        return null;
    }

    //创建敌人头部的行动图标物体
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"),canvasTf) as GameObject;
        obj.transform.SetAsLastSibling();//设置在父级的最后一位
        return obj;
    }

    //创建敌人底部的行血量物体
    public GameObject CreateHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"),canvasTf) as GameObject;
        obj.transform.SetAsLastSibling();//设置在父级的最后一位
        return obj;
    }

    // 显示提示信息的方法
    public void ShowTip(string msg, Color color, System.Action callback = null)
    {
        // 实例化一个提示预制体，并将其设置为画布的子对象
        GameObject obj = Instantiate(Resources.Load("UI/Tips"), canvasTf) as GameObject;
        
        // 找到预制体中的文本组件
        Text text = obj.transform.Find("bg/Text").GetComponent<Text>();
        
        // 设置文本颜色
        text.color = color;
        
        // 设置提示信息内容
        text.text = msg;
        
        // 定义第一个缩放动画，目标缩放为1，持续时间0.4秒
        Tween scale1 = obj.transform.Find("bg").DOScale(1, 0.4f);
        
        // 定义第二个缩放动画，目标缩放为0，持续时间0.4秒
        Tween scale2 = obj.transform.Find("bg").DOScale(0, 0.4f);

        // 创建一个动画序列
        DG.Tweening.Sequence seq = DOTween.Sequence();
        
        // 将第一个缩放动画添加到序列
        seq.Append(scale1);
        
        // 在第一个动画结束后，添加一个0.5秒的延迟
        seq.AppendInterval(0.5f);
        
        // 在延迟结束后，添加第二个缩放动画
        seq.Append(scale2);
        
        // 在第二个动画结束后，执行回调函数（如果有）
        seq.AppendCallback(delegate ()
        {
            if (callback != null)
            {
                callback();
            }
        });
        // 在2秒后销毁提示对象
        MonoBehaviour.Destroy(obj, 2);
    }

}
