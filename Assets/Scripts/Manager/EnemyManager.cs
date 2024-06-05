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
            GameObject obj = Object.Instantiate(prefab, new Vector3(x, y, z), prefab.transform.rotation);
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

        //行动完后，更新所有敌人行为
        for(int i = 0; i < enemyList.Count;i++)
        {
            enemyList[i].SetRandomAction();
        }

        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }
}
