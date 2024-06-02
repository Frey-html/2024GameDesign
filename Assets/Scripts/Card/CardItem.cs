using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem:MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Dictionary<string, string> data; //卡牌信息

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    //记录当前元素在父级中的索引
    private int index;
    // 当鼠标进入该 UI 元素时触发
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 将该 UI 元素放大到 1.5 倍，并在 0.25 秒内完成放大效果
        transform.DOScale(1.3f, 0.25f);
        // 记录当前 UI 元素在父级中的索引位置
        index = transform.GetSiblingIndex();
        // 将该 UI 元素移动到父级中的最后一个位置，使其显示在最前面
        transform.SetAsLastSibling();
        // 获取名为 "bg" 的子元素的 Image 组件，并设置其材质的颜色为黄色
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        // 设置 "bg" 子元素的材质的线条宽度为 10
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    // 当鼠标离开该 UI 元素时触发
    public void OnPointerExit(PointerEventData eventData)
    {
        // 将该 UI 元素缩小回原来的大小，并在 0.25 秒内完成缩小效果
        transform.DOScale(1, 0.25f);
        // 将该 UI 元素移动回之前记录的位置
        transform.SetSiblingIndex(index);
        // 获取名为 "bg" 的子元素的 Image 组件，并设置其材质的颜色为黑色
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        // 设置 "bg" 子元素的材质的线条宽度为 1
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

    Vector2 initPos;//拖拽开始时记录卡牌的位置
    //开始拖拽
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        //播放声音
        AudioManager.Instance.PlayEffect("Cards/draw");
    }

    //拖拽中
    public virtual void OnDrag(PointerEventData eventData)
    {
        // 存储转换后的局部坐标
        Vector2 pos;
        // 将屏幕坐标转换为父容器的局部坐标
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(), // 父容器的 RectTransform
            eventData.position, // 当前指针的位置（屏幕坐标）
            eventData.pressEventCamera, // 按下事件时的相机
            out pos // 输出参数，将转换后的局部坐标存储在 pos 中
            ))
        {
            // 如果转换成功，将当前对象的锚点位置设置为转换后的局部坐标
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }


    //结束拖拽
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }

    private void Start()
    {
        //ID name       Script Type         Des BgIcon          Icon Expand         Arg0 Effects
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = data["Des"];
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
        
        //设置bg背景image的外边框材质
        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
    }
}
