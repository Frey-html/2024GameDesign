using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();
        Dictionary<string, string> LevelData = GameConfigManager.Instance.GetLevelById(id);
        
        string[] enemyIds = LevelData["EnemyIds"].Split('=');
        string[] enemyPos = LevelData["Pos"].Split('=');
        
        for(int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);            

            
            GameObject prefab = Resources.Load<GameObject>(enemyData["Model"]);
            prefab.transform.position = new Vector3(x, y, z);
            CalculateRotation(prefab, Camera.main);
            GameObject obj = Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
            Enemy enemy = obj.AddComponent<Enemy>();
            enemyList.Add(enemy);
            enemy.Init(enemyData);
        }
    }

    //敌人死亡后移除对应敌人实体
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        //后续做清场判定
         if(enemyList.Count == 0)
        {   
            Debug.Log("Call change type to Win from DleteEnemy() in EnemyManager");
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }

      //执行活着的怪物的行为
    public IEnumerator DoAllEnemyAction()
    {   
        Debug.Log("All enemy is prepared to do something...");
        Debug.Log("Enemy count:" + enemyList.Count.ToString());


        for(int i = 0;i < enemyList.Count;i++)
        {
            Debug.Log(enemyList[i].transform.gameObject.name + "is prepared to do something");
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        if(enemyList.Count > 0){
            //行动完后，更新所有敌人行为
            for(int i = 0; i < enemyList.Count;i++)
            {
                enemyList[i].SetRandomAction();
            }
            //切换到玩家回合
            Debug.Log("Call change type to Player from DoAllEnemyAction() in EnemyManager");
            FightManager.Instance.ChangeType(FightType.Player);
        }
    }

    public void CalculateRotation(GameObject prefab, Camera camera)
    {
        // 获取Prefab和Camera的位置
        Vector3 prefabPosition = prefab.transform.position;
        Vector3 cameraPosition = camera.transform.position;

        // 计算Prefab到Camera的方向向量
        Vector3 direction = cameraPosition - prefabPosition;

        // 设置Y轴的方向向量为0，以确保只在XZ平面旋转
        direction.y = 0;

        // 确保方向向量不为零，以避免异常
        if (direction != Vector3.zero)
        {
            // 计算旋转四元数
            Quaternion rotation = Quaternion.LookRotation(direction);
            //旋转
            prefab.transform.rotation = rotation;
        }
    }
}


