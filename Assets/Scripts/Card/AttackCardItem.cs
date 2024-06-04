using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//攻击卡
public class AttackCardItem : CardItem, IPointerDownHandler
{
    public override void OnBeginDrag(PointerEventData eventdata){}

    public override void OnDrag(PointerEventData eventdata){}

    public override void OnEndDrag(PointerEventData eventdata){}

    //按下
    public void OnPointerDown(PointerEventData eventdata)
    {
        //播放声音
        AudioManager.Instance.PlayEffect("Cards/draw");
        //隐藏鼠标
        Cursor.visible = false;
        //关闭所有协同程序
        StopAllCoroutines();
        //启动鼠标操作协同程序
        StartCoroutine(OnMouseDownRight(eventdata));
    }

    IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        while (true)
        {
            //如果再次按下鼠标右键 跳出循环
            if(Input.GetMouseButton(1))
            {
                break;
            }

            Vector2 pos;
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                pData.position,
                pData.pressEventCamera,
                out pos
                ))
            {
                //进行射线检查是否碰到怪物
                CheckRayToEnemy();
            }

            yield return null;
        }

        //跳出循环后，显示鼠标
        Cursor.visible = true;
    }
    Enemy hitEnemy;//射线检测到的敌人脚本

    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            hitEnemy = hit.transform.GetComponent<Enemy>();
            Debug.Log("Ray hit enemy:" + hitEnemy.name);
            hitEnemy.OnSelect();//选中

            //如果按下鼠标左键，使用攻击卡
            if(Input.GetMouseButtonDown(0))
            {
                //关闭所有协同程序
                StopAllCoroutines();
                //鼠标显示
                Cursor.visible = true;
                if(TryUse() == true)
                {
                    //播放特效
                    PlayEffect(hitEnemy.transform.position);
                    //打击音效
                    AudioManager.Instance.PlayEffect("Effect/sword");
                    //敌人受伤
                    int val = int.Parse(data["Arg0"]);
                    hitEnemy.Hit(val);
                }
                //取消选中
                hitEnemy.OnUnSelect();
                //设置敌人脚本为null
                hitEnemy = null;
            }
        }
        else
        {
            //未射到怪物
            if(hitEnemy != null)
            {
                hitEnemy.OnUnSelect();
                hitEnemy = null;
            }
        }
    }
}
